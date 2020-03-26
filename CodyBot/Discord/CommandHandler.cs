﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodyBot.Discord
{
	public class CommandHandler
	{
		private readonly char prefix = '!';
		private readonly DiscordSocketClient client;
		private readonly CommandService commands;

		public CommandHandler(DiscordSocketClient client, CommandService commands)
		{
			this.commands = commands;
			this.client = client;
		}

		public async Task InstallCommandsAsync()
		{
			// Hook the MessageReceived event into our command handler
			client.MessageReceived += HandleCommandAsync;

			// If you do not use Dependency Injection, pass null.
			// See Dependency Injection guide for more information.
			await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
											services: null);
		}

		private async Task HandleCommandAsync(SocketMessage messageParam)
		{
			// Don't process the command if it was a system message
			var message = messageParam as SocketUserMessage;
			if (message == null) return;

			// Create a number to track where the prefix ends and the command begins
			int argPos = 0;

			// Determine if the message is a command based on the prefix and make sure no bots trigger commands
			if (!(message.HasCharPrefix(prefix, ref argPos) ||
				message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
				message.Author.IsBot)
				return;

			// Create a WebSocket-based command context based on the message
			var context = new SocketCommandContext(client, message);

			// Execute the command with the command context we just
			// created, along with the service provider for precondition checks.

			// Keep in mind that result does not indicate a return value
			// rather an object stating if the command executed successfully.
			var result = await commands.ExecuteAsync(
				context: context,
				argPos: argPos,
				services: null);
		}
	}
}