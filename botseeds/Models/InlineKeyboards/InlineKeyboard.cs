using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;
using botseeds.Models;

namespace botseeds.Models.InlineKeyboards
{
    public class InlineKeyboard
    {
        public InlineKeyboardMarkup PhotoKeyboard()
        {
            var PhotoKeyboard = new InlineKeyboardMarkup(new[]
                {

                new VarietiesPhoto().Bubblicious(true),

                   new[] // second row
                   {
                       new InlineKeyboardButton(){ Text = "Go Back", CallbackData = "GoToAutoPhoto" }
                   }
               });

            return PhotoKeyboard;
        }

        public InlineKeyboardMarkup AutoKeyboard()
        {
            var AutoKeyboard = new InlineKeyboardMarkup(new[]
                {
                   new VarietiesAuto().Magnum(true),

                   new VarietiesAuto().MalanaBomb(true),

                   new VarietiesAuto().MambaNegra(true),

                   new VarietiesAuto().CBDSkunkHaze(true),

                   new VarietiesAuto().SweetTooth(true),

                   new VarietiesAuto().SwissDreamCBD(true),

                   new VarietiesAuto().BlueCheese(true),

                   new VarietiesAuto().Mikromachine(true),

                   new VarietiesAuto().Zkittlez(true),

                   new VarietiesAuto().NewYorkCity(true),

                   new VarietiesAuto().RedPoison(true),

                   new VarietiesAuto().SuperSkunk(true),

                   new VarietiesAuto().CaliforniaOrange(true),

                   new VarietiesAuto().Purple(true),

                   new VarietiesAuto().DarkPurple(true),

                   new VarietiesAuto().LemonSkunk(true),

                   new VarietiesAuto().CBDWhiteWidow(true),

                   new VarietiesAuto().Kush(false),

                   new VarietiesAuto().ExodusCheeseCBD(true),

                   new VarietiesAuto().BlueberrySkunk(true),

                   new VarietiesAuto().CreamMandarin(true),

                   new VarietiesAuto().Lowryder2(true),

                   new VarietiesAuto().Cheese(true),

                   new VarietiesAuto().DarkDevil(true),

                   new VarietiesAuto().OGKush(true),

                   new VarietiesAuto().SourDieselHaze(true),

                   new VarietiesAuto().Vertigo(true),

                   new VarietiesAuto().Skunk1(true),

                   new VarietiesAuto().Pineapple(true),

                   new VarietiesAuto().SuperLemonHaze(true),

                   new VarietiesAuto().BigSkunk(true),

                   new VarietiesAuto().WhiteWidowXXL(true),

                   new VarietiesAuto().AmneziaXXL(false),

                   new VarietiesAuto().BigBudXXL(true),

                   new VarietiesAuto().GirlScoutCookiesXXL(true),

                   new VarietiesAuto().ElAlquimista(true),

                   new VarietiesAuto().DeliciousCandy(false),

                   new VarietiesAuto().NorthernLightBlue(true),


                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Go Back", CallbackData = "GoToAutoPhoto" }
                   }
               });

            return AutoKeyboard;
        }

        public InlineKeyboardMarkup ChooseVerietiesKeyboard()
        {
            var ChooseVerietiesKeyboard = new InlineKeyboardMarkup(new[]
                   {
                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Автики", CallbackData = "Auto" }
                   },

                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Фотики", CallbackData = "Photo" }
                   }
               });

            return ChooseVerietiesKeyboard;
        }

        public InlineKeyboardMarkup GoBackToAutoPhoto()
        {
            var GoBackToAutoPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[]
                   {
                       new InlineKeyboardButton(){ Text = "Go Back", CallbackData = "GoToAutoPhoto" }
                   }
               });

            return GoBackToAutoPhoto;
        }

        public InlineKeyboardMarkup CartOrBackPhoto()
        {
            var CartOrBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Добавить в корзину", CallbackData = "AddToCart" }
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Go Back", CallbackData = "BackPhoto" }
                   }
               });

            return CartOrBackPhoto;
        }

        public InlineKeyboardMarkup OnlyBackPhoto()
        {
            var OnlyBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Go Back", CallbackData = "BackPhoto" }
                   }
               });

            return OnlyBackPhoto;
        }

        public InlineKeyboardMarkup CartOrBackAuto()
        {
            var CartOrBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Добавить в корзину", CallbackData = "AddToCart" }
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Go Back", CallbackData = "BackAuto" }
                   }
               });

            return CartOrBackPhoto;
        }

        public InlineKeyboardMarkup OnlyBackAuto()
        {
            var OnlyBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Go Back", CallbackData = "BackAuto" }
                   }
               });

            return OnlyBackPhoto;
        }

        public InlineKeyboardMarkup ContinueShopping()
        {
            var CheckCart = new InlineKeyboardMarkup(new[]
                {
                    new[] 
                   {
                       new InlineKeyboardButton() { Text = "Продолжить покупки", CallbackData = "Auto" }
                   }
               });

            return CheckCart;
        }

        public InlineKeyboardMarkup ClearCartOrCreateOrder()
        {
            var CheckCart = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Очистить корзину", CallbackData = "ClearCart"}
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Оформить заказ", CallbackData = "CreateOrder"}
                   }
               });
            return CheckCart;
        }

        public InlineKeyboardMarkup CheckPay()
        {
            var CheckCart = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Оплатить с банкоской карты", Url = "https://money.yandex.ru/to/410012613142360"} // твоя ссылка
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Оплатить с кошелька Яндекс Денег", Url = "https://money.yandex.ru/to/410012613142360"} // твоя ссылка
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Проверить оплату", CallbackData = "CheckPay"}
                   }
               });
            return CheckCart;
        }

        public InlineKeyboardMarkup QuantitySeeds()
        {
            var CartOrBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "1", CallbackData = "seed1" }
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "2", CallbackData = "seed2" }
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "3", CallbackData = "seed3" }
                   }
               });

            return CartOrBackPhoto;
        }

        public InlineKeyboardMarkup Delivery()
        {
            var CartOrBackPhoto = new InlineKeyboardMarkup(new[]
                {
                   new[] 
                   {
                       new InlineKeyboardButton(){ Text = "Россия", CallbackData = "Russia" }
                   },
                   new[] 
                   {
                       new InlineKeyboardButton() { Text = "Другая страна", CallbackData = "AnotherCounry" }
                   }
               });

            return CartOrBackPhoto;
        }
    }
}
