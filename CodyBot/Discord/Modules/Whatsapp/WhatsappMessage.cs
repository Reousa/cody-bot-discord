using System;
using System.Collections.Generic;
using System.Text;

namespace CodyBot.Discord.Modules.Whatsapp
{
	public struct WhatsappMessage
	{
		public string Message;
		public string Date;
		public string Author;

		public WhatsappMessage(string message, string date, string author)
		{
			Message = message;
			Date = date;
			Author = author;
		}
	}
}
