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
        // ~come -> Joins your currnet room
        [Command("come")]
        [Summary("Join author's current voice channel.")]
        public async Task ComeAsync( [Summary("Join author's current voice channel.")] IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("User must be in a voice channel");
            }

            return;
        }

    }
}
