using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

		public static List<DB> Reader() 
		{
			string RawJson = File.ReadAllText(@"C:/Users/karee/source/repos/cody-bot-discord/CodyBot/DB.Json");
			return JsonConvert.DeserializeObject<List<DB>>(RawJson);
		}
		public static void Writer(ulong Id,char Prefix) 
		{

			List<DB> _data = Reader();
			for (int i = 0; i < _data.Count; i++)
				if (_data[i].GuildId == Id) {
					_data.RemoveAt(i);
					break;
				}
			_data.Add(new DB()
			{
				GuildId=Id,
				Prefix=Prefix
			});
			string json = JsonConvert.SerializeObject(_data.ToArray());
			//write string to file
			System.IO.File.WriteAllText(@"C:/Users/karee/source/repos/cody-bot-discord/CodyBot/DB.Json", json);
		}
		public CommandHandler(DiscordSocketClient client, CommandService commands)
		{
			this.commands = commands;
			this.client = client;
		}
		public char PrefixGetter(ulong GuildID)
		{
			List<DB> allista = Reader();
			foreach(DB server in allista) { 
				if(server.GuildId==GuildID)
					return server.Prefix;
			}
			return prefix;
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

			Console.WriteLine("asdsadsadsadsadsadsadsadsadsad");
			// Create a WebSocket-based command context based on the message
			var context = new SocketCommandContext(client, message);
			// Determine if the message is a command based on the prefix and make sure no bots trigger commands
			if (!(message.HasCharPrefix(PrefixGetter(context.Guild.Id), ref argPos) ||
				message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
				message.Author.IsBot)
				return;
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
