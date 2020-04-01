using Discord.Commands;
using System;
using System.Collections.Generic;

namespace CodyBot.Discord.Modules
{
    public class WhatsappModule : ModuleBase<SocketCommandContext>
    {
        public static WhatsappConv ParseMessages(string completeText)
        {
            //parse each message, add it to the array, return the array
            String[] spearator = { "[", "] " ,": "};
            List<String> splitText = new List<string>(completeText.Split(spearator, StringSplitOptions.None)); 
            int nSize = 3;
            var list = new List<List<String>>();
            //Console.WriteLine(completeText);
            //for (int i = 0; i < splitText.Count; i += 1)
            //{
            //    Console.WriteLine(splitText[i]);
            //}
            for (int i = 1; i < splitText.Count; i += nSize)
            {
                list.Add(splitText.GetRange(i, Math.Min(nSize, splitText.Count - i)));
            }
            List<WhatsappMessage> whatsappMessages = new List<WhatsappMessage>();
            foreach (List<String> sublist in list)
            {
                if (sublist.Count == 3)
                {
                    WhatsappMessage whatsappMessage = new WhatsappMessage(sublist[0], sublist[1], sublist[2]);
                    whatsappMessages.Add(whatsappMessage);
                    //Console.WriteLine(sublist[0]+""+ sublist[1] + "" + sublist[2]);

                }

            }
            WhatsappConv conv = new WhatsappConv(whatsappMessages);
            return conv;
        }
        //static void Main(string[] args)
        //{
        //    string op = ParseMessages("[4 / 1, 12:50 AM] Karim: Sar7aaaaaan[4 / 1, 12:50 AM] Karim: Hi[4 / 1, 12:50 AM] Karim: Eb3atly 7aha").ToString();
        //    Console.WriteLine(op);
        //}
    }
}
/*
[4 / 1, 12:50 AM] Karim: Sar7aaaaaan
[4 / 1, 12:50 AM] Karim: Hi
[4 / 1, 12:50 AM] Karim: Eb3atly 7aha
[4 / 1, 12:50 AM] Karim: 7aga*
[4 / 1, 12:51 AM] Karim Sar7an: asd
[4 / 1, 12:51 AM] Karim Sar7an: dsa
[4 / 1, 12:51 AM] Karim Sar7an: sad
[4 / 1, 12:51 AM] Karim Sar7an: dsa
[4 / 1, 12:51 AM] Karim Sar7an: a
[4 / 1, 12:51 AM] Karim: Shmshm
*/;