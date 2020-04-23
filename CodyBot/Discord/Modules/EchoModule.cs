using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodyBot.Discord.Modules
{
	// Create a module with no prefix
	public class EchoModule : ModuleBase<SocketCommandContext>
	{
		// ~say hello world -> hello world
		[Command("say")]
		[Summary("Echoes a message back to the user.")]
		public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
		{
			return ReplyAsync(echo);
		}
		// ~say hello world -> hello world
		[Command("prefix")]
		[Summary("Echoes a message back to the user.")]
		public Task PrefixAsync([Remainder] [Summary("The text to echo")] char echo)
		{
			Console.WriteLine("ana henaaa");
			ulong guildid = Context.Guild.Id;
			CommandHandler.Writer(guildid, echo);
			return ReplyAsync("now prefix is "+echo+" done");
		}
	}
}
