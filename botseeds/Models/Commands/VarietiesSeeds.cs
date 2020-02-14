using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using botseeds.Models.InlineKeyboards;
using System.Threading.Tasks;

namespace botseeds.Models.Commands
{
    public class VarietiesSeeds : Command
    {
        public override string Name => "Сорта семок";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "\U0001F303 Выбери тип семок \U0001F303", replyMarkup: new InlineKeyboard().ChooseVerietiesKeyboard());
        }
    }
}