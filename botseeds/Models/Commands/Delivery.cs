using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace botseeds.Models.Commands
{
    public class Delivery : Command
    {
        public override string Name => "U0001F680 Доставка";

        public override async Task Execute(Message message, TelegramBotClient client, Update update)
        {
            await client.SendTextMessageAsync(message.From.Id, "U0001F449 Доставка производится 1 классом Почтой России. Быстро и дешево. Например, в Москву - 2-3 дня \n U0001F449 Доставка в любую точку России - 200 рублей \n U0001F449 Доставка в другую страну - 500 рублей \n U0001F449 Есть опыт отправки в другие страны(Узбекистан, Таджикистан, Грузия, Армения, Беларусь, Украина, Корея), ни одного случая задержки на таможне. \n U0001F449 Упаковки по России при желании клиента шифруются(чисто для душевного спокойствия некоторых граждан).Упаковка в деталь + 100 рублей к цене доставки \n U0001F449 Для доставки необходимы ваши ФИО и Индекс \n U0001F449 Трек номер предоставляю(вы узнаете, когда посылка придет на вашу почту) \n U0001F449 Если что - то непонятно, пишите мне в личку - @realganjahands"); 
        }
    }
}