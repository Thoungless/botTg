using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace botseeds.Models.Commands
{
    public class Pay : Command
    {
        public override string Name => "U0001F4B0 Оплата";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            await client.SendTextMessageAsync(message.From.Id, "U0001F449 Все семки - 250 за штуку   U000203C КАЖДЫЙ ПЯТЫЙ В ПОДАРОК \n U0001F449 Предоплата заказа 100 процентов(про порядочность можно узнать в моем чате) - @ganjahandstalk \n U0001F449 Оплата возможна несколькими способами - Qiwi, ЯндексДеньги и карта Сбербанк \n U0001F449 Если есть вопросы, пишем мне в личку - я очень приветлив и открыт новым людям(хотя всяко бывает)  @realganjahands")
        }
    }
}