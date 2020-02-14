using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace botseeds.Models.Commands
{
    public class HowToOrder : Command
    {
        public override string Name => "U0001F4A3 Как заказать?";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            await client.SendTextMessageAsync(message.From.Id, "Можете просто посмотреть видео (Ссылка на видео)"); // твоя ссылка на видео

        }
    }
}