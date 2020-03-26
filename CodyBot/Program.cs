using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using CodyBot.Discord;

namespace CodyBot
{
	class Program
	{
		private string token = "";

		static void Main(string[] args) =>
			new Program().MainAsync().GetAwaiter().GetResult();

		private string ReadToken()
		{
			if (string.IsNullOrEmpty(token))
			{
				Console.WriteLine("Please enter your discord bot token:");
				return Console.ReadLine();
			}
			else
				return Environment.GetEnvironmentVariable("token");
		}

		public async Task MainAsync()
		{
			var client = new DiscordSocketClient();
			await client.LoginAsync(TokenType.Bot, ReadToken());
			await client.StartAsync();

			await Task.Delay(-1);
		}
	}
}
