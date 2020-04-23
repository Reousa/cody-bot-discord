using System;
using System.Collections.Generic;
using System.Text;

namespace CodyBot.Discord.Modules.Whatsapp
{
	public class WhatsappConversation
	{
		public string[] Authors { get; }
		public WhatsappMessage[] Messages { get; }

		public WhatsappConversation(WhatsappMessage[] messages)
		{
			var allAuthors = new HashSet<string>();
			foreach(var message in messages)
			{
				allAuthors.Add(message.Author);
			}
			Authors = new string[allAuthors.Count];
			allAuthors.CopyTo(Authors);
			Messages = messages;
		}
	}
}
