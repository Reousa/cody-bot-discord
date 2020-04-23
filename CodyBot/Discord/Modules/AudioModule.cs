using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using RunMode = Discord.Commands.RunMode;

namespace CodyBot.Discord.Modules
{
    public class AudioModule : ModuleBase<SocketCommandContext>
    {

        public int MaxMarmatLoops = 6;

        /// <summary>
        /// !come -> Joins your current room
        /// !come room2  -> Joins room2
        /// </summary>
        [Command("come")]
        [Alias("join", "t3ala")]
        [Summary("Join author's current voice channel.")]
        public async Task ComeAsync(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("Author must be in a voice channel, or a voice channel must be passed as an argument.");
            }
            else
                await channel.ConnectAsync();
            return;
        }

        /// <summary>
        /// !marmt -> Author gets moved to all the empty voice channels and then gets moved back to his orignal channel
        /// !marmt @kareemsarhan -> metioned user gets moved to all the empty voice channels and then gets moved back to his orignal channel
        /// !marmt @kareemsarhan 3 -> metioned user gets moved to all the empty voice channels for 3 times and then gets moved back to his orignal channel
        /// </summary>
        [Command("move", RunMode = RunMode.Async)]
        [Alias("marmt", "bhdl", "marmat", "mrmt", "bahdl", "bahdel", "mrmat")]
        [Summary("metioned user gets moved to all the empty voice channels and then gets moved back to his orignal channel.")]
        public async Task moveAsync(IGuildUser user = null, int count = 1)
        {
            Console.WriteLine($"User {Context.User.Username} requested to marmat {user.Nickname ?? user.Username} {count} times.");

            // Get the user
            user = user ?? Context.User as IGuildUser;
            // Get the audio channel
            IVoiceChannel originalChannel = user.VoiceChannel;
            if (originalChannel == null)
            {
                await Context.Channel.SendMessageAsync("The user that you want to marmat must be in a voice channel");
                return;
            }

            count = count > MaxMarmatLoops ? MaxMarmatLoops : count;

            IGuild guild = Context.Guild;
            var guildChannels = await guild.GetVoiceChannelsAsync();

            for (int i = count; i > 0; i--)
            {

                await Context.Channel.SendMessageAsync($"Marmating {user.Nickname} {i} more times.");
                Console.WriteLine($"Marmating {user.Nickname} {i} more times.");

                foreach (IVoiceChannel channel in guildChannels)
                {
                    if (channel != originalChannel)
                        await user.ModifyAsync(x =>
                        {
                            x.Channel = Optional.Create(channel);
                        });
                }
            }

            await user.ModifyAsync(x =>
            {
                x.Channel = Optional.Create(originalChannel);
            });
            return;
        }

        /// <summary>
        /// !play https://www.youtube.com/watch?v=qpZrFl9v9oM -> Joins your current room & play the audio from this youtube video.
        /// </summary>
        [Command("play")]
        [Alias("P", "p")]
        [Summary("Joins your current room & play the audio from this youtube video.")]
        public async Task PlayAsync(IVoiceChannel channel = null)
        {
        }
    }
}
