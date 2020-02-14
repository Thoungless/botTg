using botseeds.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using botseeds.Models.InlineKeyboards;

namespace botseeds.Controllers
{
    public enum State { nothing, index, adress, fio};
    public class MessageController : ApiController
    {
        static string nameOrder;
        static DateTime dt = new DateTime();
        static State state;

        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {


            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();

            if (update.Type == UpdateType.CallbackQuery)
            {
                var callback = update.CallbackQuery;

                if (callback.Data.ToString() == "AddToCart")
                {
                    try
                    {
                        await client.SendTextMessageAsync(callback.From.Id, ("Выбери количество семок"), replyMarkup: new InlineKeyboard().QuantitySeeds());
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                    }
                }
                else if(callback.Data.ToString() == "seed1")
                {
                    await new database().AddToCartUser(callback.From.Id, nameOrder);
                    await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                    await client.SendTextMessageAsync(callback.From.Id, ("Товар " + nameOrder + " добавен в корзину."), replyMarkup: new InlineKeyboard().ContinueShopping());
                    nameOrder = "";
                }
                else if (callback.Data.ToString() == "seed2")
                {
                    await new database().AddToCartUser(callback.From.Id, nameOrder);
                    await new database().AddToCartUserMany(callback.From.Id, nameOrder);
                    await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                    await client.SendTextMessageAsync(callback.From.Id, ("Товар " + nameOrder + " добавен в корзину."), replyMarkup: new InlineKeyboard().ContinueShopping());
                    nameOrder = "";
                }
                else if (callback.Data.ToString() == "seed3")
                {
                    
                    await new database().AddToCartUser(callback.From.Id, nameOrder);
                    await new database().AddToCartUserMany(callback.From.Id, nameOrder);
                    await new database().AddToCartUserMany(callback.From.Id, nameOrder);
                    await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                    await client.SendTextMessageAsync(callback.From.Id, ("Товар " + nameOrder + " добавен в корзину."), replyMarkup: new InlineKeyboard().ContinueShopping());
                    nameOrder = "";
                }
                else if (callback.Data.ToString() == "Russia")
                {
                    await new database().SaveUserCountry(callback.From.Id, "Russia");
                    await client.SendTextMessageAsync(callback.From.Id, "Введите ваше ФИО");
                    state = State.fio;
                }
                else if (callback.Data.ToString() == "AnotherCounry")
                {
                    await new database().SaveUserCountry(callback.From.Id, "Another");
                    await client.SendTextMessageAsync(callback.From.Id, "Введите ваш полный адрес");
                    state = State.adress;
                }
                else if (callback.Data.ToString() == "ClearCart")
                {
                    try
                    {
                        await new database().ClearCart(callback.From.Id);
                        await client.SendTextMessageAsync(callback.From.Id, "\U0001F30F Ваша корзина очищена \U0001F30F");
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                    }
                }
                else if (callback.Data.ToString() == "CreateOrder")
                {
                    try
                    {
                        var listOrder = await new database().OpenCart(callback.From.Id);
                        if (listOrder.Count >= 4)
                        {
                            await client.SendTextMessageAsync(callback.From.Id, "Выберите куда доставлять", replyMarkup: new InlineKeyboard().Delivery());
                        }
                        else
                        {
                            await client.SendTextMessageAsync(callback.From.Id, "Минимальный заказ 4 семечки.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                    }
                }
                else if (callback.Data.ToString() == "CheckPay")
                {
                    try
                    {
                        var country = await new database().GetUserCountry(callback.From.Id);

                        if (country == "Russia")
                        {
                            var paymenth = await new HttpRequests().GetHistory(dt);
                            var listOrder = await new database().OpenCart(callback.From.Id);
                            var timedate = await new database().OpenDateTime(callback.From.Id);
                            var sum = await new database().GetUserSum(callback.From.Id);
                            if (paymenth.operations.Count != 0)
                            {
                                foreach (var i in paymenth.operations)
                                {
                                    if (i.status == "success" && i.direction == "in" && i.amount == sum && (i.type == "incoming-transfer") || (i.type == "deposition"))
                                    {
                                        if (i.datetime.Date.Day >= timedate.Date.Month && i.datetime.Date.Month >= timedate.Date.Day && i.datetime.AddHours(3).Hour >= timedate.Hour && i.datetime.Minute > timedate.Minute)
                                        {
                                            await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                                            await client.SendTextMessageAsync(callback.From.Id, "Ваш заказ оформлен, скоро мы вам пришлем трек номер");
                                            string fio = await new database().GetUserFio(callback.From.Id);
                                            string index = await new database().GetUserIndex(callback.From.Id);
                                            string order = "";
                                            foreach (string ordr in listOrder)
                                            {
                                                order += ordr + ", ";
                                            }
                                            order = order.TrimEnd(',', ' ');
                                            await client.SendTextMessageAsync("549948572", ("Поступил новый заказ от @" + callback.From.Username + " его Id в телеграме " + callback.From.Id + " его ФИО - " + fio + " его Индекс - " + index + " его заказ " + order));
                                            await new database().RemoveDateTime(callback.From.Id);
                                            await new database().ClearCart(callback.From.Id);
                                            break;
                                        }
                                    }
                                    if (i == paymenth.operations[(paymenth.operations.Count) - 1])
                                    {
                                        await client.SendTextMessageAsync(callback.From.Id, ("Оплата ещё не поступила, попробуйте через пару минут"));
                                    }
                                }
                            }
                        }
                        if(country == "Another")
                        {
                            var paymenth = await new HttpRequests().GetHistory(dt);
                            var listOrder = await new database().OpenCart(callback.From.Id);
                            var timedate = await new database().OpenDateTime(callback.From.Id);
                            int sum = await new database().GetUserSum(callback.From.Id);
                            if (paymenth.operations.Count != 0)
                            {
                                foreach (var i in paymenth.operations)
                                {
                                    if (i.status == "success" && i.direction == "in" && i.amount == sum && (i.type == "incoming-transfer") || (i.type == "deposition"))
                                    {
                                        if (i.datetime.Date.Day >= timedate.Date.Month && i.datetime.Date.Month >= timedate.Date.Day && i.datetime.AddHours(3).Hour >= timedate.Hour && i.datetime.Minute > timedate.Minute)
                                        {
                                            await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                                            await client.SendTextMessageAsync(callback.From.Id, "Ваш заказ оформлен, скоро мы вам пришлем трек номер");
                                            string fio = await new database().GetUserFio(callback.From.Id);
                                            string index = await new database().GetUserIndex(callback.From.Id);
                                            string adress = await new database().GetUserAdress(callback.From.Id);
                                            string order = "";
                                            foreach(string ordr in listOrder)
                                            {
                                                order += ordr + ", ";
                                            }
                                            order = order.TrimEnd(',', ' ');
                                            await client.SendTextMessageAsync("549948572", ("Поступил новый заказ от @" + callback.From.Username + " его Id в телеграме " + callback.From.Id + " его ФИО - " + fio + " его Индекс - " + index + " его адрес - " + adress + " его заказ " + order));
                                            await new database().RemoveDateTime(callback.From.Id);
                                            await new database().ClearCart(callback.From.Id);
                                            break;
                                        }
                                    }
                                    if (i == paymenth.operations[(paymenth.operations.Count) - 1])
                                    {
                                        await client.SendTextMessageAsync(callback.From.Id, ("Оплата ещё не поступила, попробуйте через пару минут"));
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Exception d = ex;
                    }
                    return Ok();
                }
                else
                {
                    nameOrder = await HandleAsync(callback, client);
                    return Ok();
                }
                return Ok();
            }

            if (update.Type == UpdateType.Message)
            {
                if (state == State.adress)
                {
                    if (message.Text != null)
                    {
                        await new database().SaveUserAdress(message.From.Id, message.Text);
                        state = State.fio;
                        await client.SendTextMessageAsync(message.From.Id, "Введите ваше ФИО");
                    }
                    return Ok();
                }
                if (state == State.fio)
                {
                    if (message.Text != null)
                    {
                        await new database().SaveUserFio(message.From.Id, message.Text);
                        state = State.index;
                        await client.SendTextMessageAsync(message.From.Id, "Введите ваш Индекс");
                    }
                    return Ok();
                }
                if (state == State.index)
                {
                    int sum = 0;
                    if (message.Text != null)
                    {
                        await new database().SaveUserIndex(message.From.Id, message.Text);
                        state = State.nothing;
                        var listOrder = await new database().OpenCart(message.From.Id);
                        var country = await new database().GetUserCountry(message.From.Id);
                        if (country == "Russia")
                        {
                            sum = 200;
                            sum += (listOrder.Count * 250);
                            if (listOrder.Count > 4)
                            {
                                if (listOrder.Count > 8)
                                {
                                    if (listOrder.Count > 12)
                                        sum -= 750;
                                    else
                                        sum -= 500;
                                }
                                else
                                    sum -= 250;
                            }
                        }
                        if (country == "Another")
                        {
                            sum = 500;
                            sum += (listOrder.Count * 250);
                            if (listOrder.Count > 4)
                            {
                                if (listOrder.Count > 8)
                                {
                                    if (listOrder.Count > 12)
                                        sum -= 750;
                                    else
                                        sum -= 500;
                                }
                                else
                                    sum -= 250;
                            }
                        }
                        if (listOrder.Count != 0)
                        {
                            dt = DateTime.Now;
                            await new database().AddDateTimeUser(message.From.Id, dt);
                            await new database().SaveUserSum(message.From.Id, sum);
                            await client.SendTextMessageAsync(message.From.Id, ("Переведите " + sum + " рублей на колешек яндекс денег 410012613142360"), replyMarkup: new InlineKeyboard().CheckPay());
                        }
                    }
                    return Ok();
                }
            }

            try
            {
                foreach (var command in commands)
                {
                    if (message.Text == null)
                        break;
                    if (command.Contains(message.Text))
                    {
                       await command.Execute(message, client, update);
                        break;
                    }
                }
                return Ok();
            }
            catch(Exception ex)
            {
                Exception d = ex;
                return Ok();
            }
        }       
        public async Task<string> HandleAsync(CallbackQuery callback, TelegramBotClient client)
        {
            string orderName = "";
            switch (callback.Data)
            {
                case ("Auto"):
                    try
                    {
                        await client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().AutoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Photo"):
                    try
                    {
                        await client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().PhotoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("GoToAutoPhoto"):
                    try
                    {
                        await client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().ChooseVerietiesKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                #region PhotoVarieties
                case ("Bubblicious"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/27v3Hw4", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 15-20% \n \U0001F449 КБД -около 1% \n \U0001F449 Высота - около 100 см \n \U0001F449 Время вызревания - около 80 дней", replyMarkup: new InlineKeyboard().CartOrBackPhoto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BubbliciousNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/27v3Hw4", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 15-20% \n \U0001F449 КБД -около 1% \n \U0001F449 Высота - около 100 см \n \U0001F449 Время вызревания - около 80 дней", replyMarkup: new InlineKeyboard().OnlyBackPhoto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                #endregion

                #region AutoVarieties
                case ("Magnum"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/W6NXzkj", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 10-15% \n \U0001F449 КБД -около 0 \n \U0001F449 Высота - 70-90см \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MagnumNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/W6NXzkj", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 10-15% \n \U0001F449 КБД -около 0 \n \U0001F449 Высота - 70-90см \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MalanaBomb"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/dpLYZmy", caption: "\U0001F449 САТИВА - 30 процентов \n \U0001F449 Индика - 70 процентов \n \U0001F449 ТГК - 13-15% \n \U0001F449 КБД -около 0.1 \n \U0001F449 Высота - 70-80см \n \U0001F449 Время вызревания - около 65 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MalanaBombNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/dpLYZmy", caption: "\U0001F449 САТИВА - 30 процентов \n \U0001F449 Индика - 70 процентов \n \U0001F449 ТГК - 13-15% \n \U0001F449 КБД -около 0.1 \n \U0001F449 Высота - 70-80см \n \U0001F449 Время вызревания - около 65 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MambaNegra"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/GWVypWC", caption: "\U0001F449 САТИВА - 30 процентов \n \U0001F449 Индика - 70 процентов \n \U0001F449 ТГК - 16-19% \n \U0001F449 КБД -около 0.1 \n \U0001F449 Высота - от 1 метра \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MambaNegraNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/GWVypWC", caption: "\U0001F449 САТИВА - 30 процентов \n \U0001F449 Индика - 70 процентов \n \U0001F449 ТГК - 16-19% \n \U0001F449 КБД -около 0.1 \n \U0001F449 Высота -  от 1 метра \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CBDSkunkHaze"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/31pHgZw", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 5-12% \n \U0001F449 КБД - 5-12% \n \U0001F449 Высота -  от 1 метра \n \U0001F449 Время вызревания - около 80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CBDSkunkHazeNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/31pHgZw", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 5-12% \n \U0001F449 КБД - 5-12% \n \U0001F449 Высота -  от 1 метра \n \U0001F449 Время вызревания - около 80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SweetTooth"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/m5H2Kbw", caption: "\U0001F449 САТИВА - 40 процентов \n \U0001F449 Индика - 60 процентов \n \U0001F449 ТГК - 16-19% \n \U0001F449 КБД - 0.5-0.6% \n \U0001F449 Высота - от 90см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SweetToothNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/m5H2Kbw", caption: "\U0001F449 САТИВА - 40 процентов \n \U0001F449 Индика - 60 процентов \n \U0001F449 ТГК - 16-19% \n \U0001F449 КБД - 0.5-0.6% \n \U0001F449 Высота - от 90см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SwissDreamCBD"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/qWCv7CP", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SwissDreamCBDNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/qWCv7CP", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BlueCheese"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Fw5hb8n", caption: "\U0001F449 САТИВА - 20 процентов \n \U0001F449 Индика - 80 процентов \n \U0001F449 ТГК - 20% \n \U0001F449 КБД - 1-2% \n \U0001F449 Высота - около 80 см \n \U0001F449 Время вызревания - около 70-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BlueCheeseNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Fw5hb8n", caption: "\U0001F449 САТИВА - 20 процентов \n \U0001F449 Индика - 80 процентов \n \U0001F449 ТГК - 20% \n \U0001F449 КБД - 1-2% \n \U0001F449 Высота - около 80 см \n \U0001F449 Время вызревания - около 70-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Mikromachine"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6W8HjZf", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 17% \n \U0001F449 КБД - 0.4-0.5% \n \U0001F449 Высота - около 60-80 см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("MikromachineNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6W8HjZf", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 17% \n \U0001F449 КБД - 0.4-0.5% \n \U0001F449 Высота - около 60-80 см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Zkittlez"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/LCyFS0W", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 13-15% \n \U0001F449 КБД -0.2% \n \U0001F449 Высота - около 80 см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("ZkittlezNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/LCyFS0W", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 13-15% \n \U0001F449 КБД -0.2% \n \U0001F449 Высота - около 80 см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("NewYorkCity"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/S6HT206", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 17% \n \U0001F449 КБД -0.2% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("NewYorkCityNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/S6HT206", caption: "\U0001F449 САТИВА - 60 процентов \n \U0001F449 Индика - 40 процентов \n \U0001F449 ТГК - 17% \n \U0001F449 КБД -0.2% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 70-75 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("RedPoison"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/M76ZDvp", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 14-16% \n \U0001F449 КБД - около 0.8% \n \U0001F449 Высота - от 60 см \n \U0001F449 Время вызревания - около 63-66 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("RedPoisonNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/M76ZDvp", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 14-16% \n \U0001F449 КБД - около 0.8% \n \U0001F449 Высота - от 60 см \n \U0001F449 Время вызревания - около 63-66 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SuperSkunk"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/0CPm4ZN", caption: "\U0001F449 САТИВА - 35 процентов \n \U0001F449 Индика - 65 процентов \n \U0001F449 ТГК - 19% \n \U0001F449 КБД - 0.1% \n \U0001F449 Высота - около 80см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SuperSkunkNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/0CPm4ZN", caption: "\U0001F449 САТИВА - 35 процентов \n \U0001F449 Индика - 65 процентов \n \U0001F449 ТГК - 19% \n \U0001F449 КБД - 0.1% \n \U0001F449 Высота - около 80см \n \U0001F449 Время вызревания - около 70 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CaliforniaOrange"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6bFGc0S", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 15% \n \U0001F449 КБД - около 0 \n \U0001F449 Высота - высокий \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CaliforniaOrangeNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6bFGc0S", caption: "\U0001F449 САТИВА - 50 процентов \n \U0001F449 Индика - 50 процентов \n \U0001F449 ТГК - 15% \n \U0001F449 КБД - около 0 \n \U0001F449 Высота - высокий \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                    // добавить описание -----------------------------------------------------------------------------------------
                case ("Purple"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/hBJXbBc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("PurpleNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/hBJXbBc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DarkPurple"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/hBJXbBc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DarkPurpleNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/hBJXbBc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("LemonSkunk"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/8D8kq0h", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("LemonSkunkNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/8D8kq0h", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CBDWhiteWidow"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Qn1j35Q", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CBDWhiteWidowNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Qn1j35Q", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Kush"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/G5PRQtD", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("KushNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/G5PRQtD", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("ExodusCheeseCBD"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/cyYQQvc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("ExodusCheeseCBDNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/cyYQQvc", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BlueberrySkunk"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/0FNCW96", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BlueberrySkunkNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/0FNCW96", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CreamMandarin"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/rfj5cVS", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CreamMandarinNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/rfj5cVS", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Lowryder2"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/QnChMX6", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Lowryder2NF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/QnChMX6", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Cheese"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/3RkQgTh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("CheeseNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/3RkQgTh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DarkDevil"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/PhmpGm3", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DarkDevilNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/PhmpGm3", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("OGKush"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/z2XjLSm", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("OGKushNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/z2XjLSm", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SourDieselHaze"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/mXdkxk0", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SourDieselHazeNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/mXdkxk0", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Vertigo"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6HDbbs8", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("VertigoNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/6HDbbs8", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Skunk1"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/NjftjJw", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Skunk1NF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/NjftjJw", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("Pineapple"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/K00bBpm", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("PineappleNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/K00bBpm", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SuperLemonHaze"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/XXWDMdh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("SuperLemonHazeNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/XXWDMdh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BigSkunk"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/jJd94vQ", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BigSkunkNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/jJd94vQ", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("WhiteWidowXXL"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/7Qxhy7f", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("WhiteWidowXXLNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/7Qxhy7f", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("AmneziaXXL"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Pm2ZPtD", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("AmneziaXXLNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/Pm2ZPtD", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BigBudXXL"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/ryNqpMr", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("BigBudXXLNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/ryNqpMr", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("GirlScoutCookiesXXL"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/5kHDk3G", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("GirlScoutCookiesXXLNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/5kHDk3G", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("ElAlquimista"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/yRTsHWt", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("ElAlquimistaNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/yRTsHWt", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DeliciousCandy"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/x8mg1rh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("DeliciousCandyNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/x8mg1rh", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("NorthernLightBlue"):
                    try
                    {
                        orderName = callback.Data.ToString();
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/xzWyNhW", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().CartOrBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                case ("NorthernLightBlueNF"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendPhotoAsync(callback.From.Id, photo: "https://ibb.co/xzWyNhW", caption: "\U0001F449 САТИВА - 70 процентов \n \U0001F449 Индика - 30 процентов \n \U0001F449 ТГК - 0-1% \n \U0001F449 КБД - 7% \n \U0001F449 Высота - выше 100см \n \U0001F449 Время вызревания - около 75-80 дней", replyMarkup: new InlineKeyboard().OnlyBackAuto());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }

                #endregion

                #region AfterClickPhotoVarieties
                case ("BackPhoto"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendTextMessageAsync(callback.From.Id, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().PhotoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                case ("OnlyBackPhoto"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendTextMessageAsync(callback.From.Id, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().PhotoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                #endregion

                #region AfterClickAUtoVarieties
                case ("BackAuto"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendTextMessageAsync(callback.From.Id, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().AutoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                case ("OnlyBackAuto"):
                    try
                    {
                        await client.DeleteMessageAsync(callback.From.Id, callback.Message.MessageId);
                        await client.SendTextMessageAsync(callback.From.Id, "\U0001F30F Выбери сорт \U0001F30F", replyMarkup: new InlineKeyboard().AutoKeyboard());
                        callback = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Exception d = ex;
                        break;
                    }
                #endregion

            }
            return orderName;
        }
    }        
}
