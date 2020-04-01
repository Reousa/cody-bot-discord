using System.Collections.Generic;

namespace CodyBot
{
    public class Structs
    {
        public ulong GuildId
        {
            get;
            set;
        }
        public char Prefix
        {
            get;
            set;
        }
    }
    public class WhatsappMessage
    {
        public WhatsappMessage(string Date, string SenderName, string MessageContent)
        {
            this.Date = Date;
            this.SenderName = SenderName;
            this.MessageContent = MessageContent;
        }        
        public WhatsappMessage()
        {
   
        }

        public string Date
        {
            get;
            set;
        }
        public string SenderName
        {
            get;
            set;
        }
        public string MessageContent
        {
            get;
            set;
        }
        public override string ToString()
        {
            return this.SenderName +  "   " + this.MessageContent + "   " + "Sent at: "+ this.Date+ "\n"  ;
        }
    }
    public class WhatsappConv
    {
        public WhatsappConv(List<WhatsappMessage> whatsappMessages)
        {
            this.Messages = whatsappMessages;
        }
        public WhatsappConv()
        {
        }
        public List<WhatsappMessage> Messages
        {
            get;
            set;
        }
        public override string ToString()
        {
            string output="";
            this.Messages.ForEach(Message => { output+= Message.ToString(); });
            return output;
        }
    }
}

