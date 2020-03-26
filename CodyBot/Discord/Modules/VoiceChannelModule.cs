using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RunMode = Discord.Commands.RunMode;

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

        //-------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------

        // ~marmt -> Auther gets moved to all the empty voice channels and then gets moved back to his orignal channel
        // ~marmt @kareemsarhan  -> metioned user gets moved to all the empty voice channels and then gets moved back to his orignal channel
        [Command("move")]
        [Alias("marmt", "bhdl")]
        [Summary("moves the user around.")]
        public async Task moveAsync([Summary("moves the user around.")] IGuildUser user = null , int count=10)
        {
            // Get the user
            user = user as IGuildUser ?? Context.User as IGuildUser;
            // Get the audio channel
            IVoiceChannel orignalchannel = user.VoiceChannel;
            if (orignalchannel == null)
            {
                await Context.Channel.SendMessageAsync("The user that you want to move must be in a voice channel");
            }
            IGuild guild = Context.Guild;
            IReadOnlyCollection<IVoiceChannel> guildchannels = await guild.GetVoiceChannelsAsync();
            //TODO: check this the problem in this link is happining here , cant get the solution to work 
            //https://discord.foxbot.me/docs/faq/commands/general.html?tabs=cmdattrib
            //its not an error it just slowes down the execution
            for (int i = 0; count > i; i++)
            {
                foreach (IVoiceChannel channel in guildchannels)
                {
                    //TODO: mesh 3arf a al voice channel current members 3shan a check hya empty wla l2
                    if (channel != orignalchannel)
                        await user.ModifyAsync(x =>
                        {
                            x.Channel = Optional.Create(channel);
                        });
                }
            }
            //TODO: efhm how to convert from lamda exp to normal
            await user.ModifyAsync(x =>
            {
                x.Channel = Optional.Create(orignalchannel);
            });
            return;
        }
    }
}
