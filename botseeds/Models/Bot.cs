using botseeds.Models.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;

namespace botseeds.Models
{
    public class Bot
    {
        private static TelegramBotClient client;

        private static List<Command> commandList;
        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }
        public static async Task<TelegramBotClient> Get()
        {
            try
            {
                if (client != null)
                {
                    return client;
                }

                commandList = new List<Command>();
                commandList.Add(new Start());
                commandList.Add(new VarietiesSeeds());
                commandList.Add(new Cart());

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                client = new TelegramBotClient(AppSettings.Key);

                var hook = string.Format(AppSettings.Url, "api/message/update");          

                await client.SetWebhookAsync(hook);

                return client;
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                return client;
            }
        }
    }
}