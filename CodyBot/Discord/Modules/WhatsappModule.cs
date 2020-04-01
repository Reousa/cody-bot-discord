using CodyBot.Discord.Modules.Whatsapp;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CodyBot.Discord.Modules
{
    public class WhatsappModule : ModuleBase<SocketCommandContext>
    {
        [Command("convsim")]
        [Alias("conv", "sim", "simconv")]
        [Summary("Simulates a whatsapp conv.")]
        public async Task WhatsappConvSimAsync([Remainder] [Summary("Simulates a whatsapp conv.")] string messages)
        {
			var embed = await ConvertWhatsappConvToEmbed( ParseWhatsappConversation(messages));
			await ReplyAsync(embed: embed);
		}

		private async Task<Embed> ConvertWhatsappConvToEmbed(WhatsappConversation conv)
		{
			var messages = conv.Messages;
			var eb = new EmbedBuilder
			{
				Title = "Whatsapp Conversation",
			};
			for (int i = 0; i < messages.Length; i++)
			{
				var msg = messages[i];
				eb.WithColor(Color.Blue)
					.AddField($"**{msg.Date}: {msg.Author}**", $"{msg.Message}");
			}
			var embed = eb.Build();
			return embed;
		}

		private WhatsappConversation ParseWhatsappConversation(string conversation )
		{
			var sr = new StringReader(conversation);
			var messages = new List<WhatsappMessage>();

			while(true)
			{
				var line = sr.ReadLine();
				if (!string.IsNullOrEmpty(line))
					messages.Add(ParseWhatsappMessage(line));
				else
					break;
			}
			return new WhatsappConversation(messages.ToArray());
		}

		private WhatsappMessage ParseWhatsappMessage(string line)
		{
			var first = line.Split("]");
			var date = first[0].Remove(0, 1);
			var second = first[1].Split(": ");
			var author = second[0];
			var message = second[1];

			return new WhatsappMessage(message, date, author);
		}
    }
}