using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodyBot.Discord.Modules
{
    // Create a module with no prefix
    public class VoiceChannelModule : ModuleBase<SocketCommandContext>
    {
        // ~come -> Joins your current room
        // ~come room2  -> Joins room2
        [Command("come")]
        [Alias("join", "t3ala")]
        [Summary("Join author's current voice channel.")]
        public async Task ComeAsync( [Summary("Joins author's current voice channel or the specified channel .")] IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("Author must be in a voice channel, or a voice channel must be passed as an argument.");
            }
            return;
        }
    }
}
