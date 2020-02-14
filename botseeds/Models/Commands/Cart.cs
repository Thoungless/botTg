using botseeds.Models.InlineKeyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Yandex.Money.Api.Sdk.Authorization;
using Yandex.Money.Api.Sdk.Net;

namespace botseeds.Models.Commands
{
    public class Cart : Command
    {
        public override string Name => "Корзина";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            try
            {
                var listOrder = await new database().OpenCart(message.From.Id);
                if (listOrder.Count == 0)
                {
                    await client.SendTextMessageAsync(message.From.Id, "\U0001F30F Ваша корзина пуста \U0001F30F");
                }
                else
                {
                    foreach (var lo in listOrder)
                    {
                        await client.SendTextMessageAsync(message.From.Id, lo);
                    }
                    await client.SendTextMessageAsync(message.From.Id, "\U0001F30F Товары в вашей корзине \U0001F30F", replyMarkup: new InlineKeyboard().ClearCartOrCreateOrder());
                }
            }
            catch(Exception ex)
            {
                Exception d = ex;
            }
        }
    }
}