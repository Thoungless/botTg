using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace botseeds.Models.Commands
{
    public class Start : Command
    {
        public override string Name => "start";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            try
            {
                var chatId = message.Chat.Id;
                var nickName = message.From.FirstName;
                await new database().SaveUser(chatId, nickName);

                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard = new[]
                    {
                    new[]
                    {
                    new KeyboardButton("Сорта семок")
                    },

                    new[]
                    {
                    new KeyboardButton("U0001F4A3 Как заказать?")
                    },

                    new[]
                    {
                    new KeyboardButton("U0001F680 Доставка"),
                    new KeyboardButton("U0001F4B0 Оплата")
                    },

                    new[]
                    {
                    new KeyboardButton("Корзина")
                    }
                }
                };
                keyboard.ResizeKeyboard = true;
                await client.SendTextMessageAsync(chatId, "\U0001F308 Привет, Бро! Добро пожаловать в наш магазин \U0001F308", ParseMode.Html, false, false, 0, keyboard);
            }
            catch(Exception ex)
            {
                Exception d = ex;
            }
        }
    }
}