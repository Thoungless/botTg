using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;

namespace botseeds.Models
{
    public class VarietiesAuto
    {
        public InlineKeyboardButton[] Magnum(bool availability)
        {
            if (availability)
            {
                var Magnum = new[]
                   {
                      new InlineKeyboardButton() { Text = "Magnum", CallbackData = "Magnum" }
                   };

                return Magnum;
            }
            else
            {
                var Magnum = new[]
                   {
                      new InlineKeyboardButton() { Text = "Magnum (Нет в наличии)", CallbackData = "MagnumNF" },
                   };

                return Magnum;
            }
        }

        public InlineKeyboardButton[] MalanaBomb(bool availability)
        {
            if (availability)
            {
                var MalanaBomb = new[]
                   {
                      new InlineKeyboardButton() { Text = "Malana Bomb", CallbackData = "MalanaBomb" }
                   };

                return MalanaBomb;
            }
            else
            {
                var MalanaBomb = new[]
                   {
                      new InlineKeyboardButton() { Text = "Malana Bomb (Нет в наличии)", CallbackData = "MalanaBombNF" },
                   };

                return MalanaBomb;
            }
        }

        public InlineKeyboardButton[] MambaNegra(bool availability)
        {
            if (availability)
            {
                var MambaNegra = new[]
                   {
                      new InlineKeyboardButton() { Text = "Mamba Negra", CallbackData = "MambaNegra" }
                   };

                return MambaNegra;
            }
            else
            {
                var MambaNegra = new[]
                   {
                      new InlineKeyboardButton() { Text = "Mamba Negra (Нет в наличии)", CallbackData = "MambaNegraNF" },
                   };

                return MambaNegra;
            }
        }

        public InlineKeyboardButton[] CBDSkunkHaze(bool availability)
        {
            if (availability)
            {
                var CBDSkunkHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "CBD Skunk Haze", CallbackData = "CBDSkunkHaze" }
                   };

                return CBDSkunkHaze;
            }
            else
            {
                var CBDSkunkHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "CBD Skunk Haze (Нет в наличии)", CallbackData = "CBDSkunkHazeNF" },
                   };

                return CBDSkunkHaze;
            }
        }

        public InlineKeyboardButton[] SweetTooth(bool availability)
        {
            if (availability)
            {
                var SweetTooth = new[]
                   {
                      new InlineKeyboardButton() { Text = "Sweet Tooth", CallbackData = "SweetTooth" }
                   };

                return SweetTooth;
            }
            else
            {
                var SweetTooth = new[]
                   {
                      new InlineKeyboardButton() { Text = "Sweet Tooth (Нет в наличии)", CallbackData = "SweetToothNF" },
                   };

                return SweetTooth;
            }
        }

        public InlineKeyboardButton[] SwissDreamCBD(bool availability)
        {
            if (availability)
            {
                var SwissDreamCBD = new[]
                   {
                      new InlineKeyboardButton() { Text = "Swiss Dream CBD", CallbackData = "SwissDreamCBD" }
                   };

                return SwissDreamCBD;
            }
            else
            {
                var SwissDreamCBD = new[]
                   {
                      new InlineKeyboardButton() { Text = "Swiss Dream CBD (Нет в наличии)", CallbackData = "SwissDreamCBDNF" },
                   };

                return SwissDreamCBD;
            }
        }

        public InlineKeyboardButton[] BlueCheese(bool availability)
        {
            if (availability)
            {
                var BlueCheese = new[]
                   {
                      new InlineKeyboardButton() { Text = "Blue Cheese", CallbackData = "BlueCheese" }
                   };

                return BlueCheese;
            }
            else
            {
                var BlueCheese = new[]
                   {
                      new InlineKeyboardButton() { Text = "Blue Cheese (Нет в наличии)", CallbackData = "BlueCheeseNF" },
                   };

                return BlueCheese;
            }
        }

        public InlineKeyboardButton[] Mikromachine(bool availability)
        {
            if (availability)
            {
                var Mikromachine = new[]
                   {
                      new InlineKeyboardButton() { Text = "Mikromachine", CallbackData = "Mikromachine" }
                   };

                return Mikromachine;
            }
            else
            {
                var Mikromachine = new[]
                   {
                      new InlineKeyboardButton() { Text = "Mikromachine (Нет в наличии)", CallbackData = "MikromachineNF" },
                   };

                return Mikromachine;
            }
        }

        public InlineKeyboardButton[] Zkittlez(bool availability)
        {
            if (availability)
            {
                var Zkittlez = new[]
                   {
                      new InlineKeyboardButton() { Text = "Zkittlez", CallbackData = "Zkittlez" }
                   };

                return Zkittlez;
            }
            else
            {
                var Zkittlez = new[]
                   {
                      new InlineKeyboardButton() { Text = "Zkittlez (Нет в наличии)", CallbackData = "ZkittlezNF" },
                   };

                return Zkittlez;
            }
        }

        public InlineKeyboardButton[] NewYorkCity(bool availability)
        {
            if (availability)
            {
                var NewYorkCity = new[]
                   {
                      new InlineKeyboardButton() { Text = "New York City", CallbackData = "NewYorkCity" }
                   };

                return NewYorkCity;
            }
            else
            {
                var NewYorkCity = new[]
                   {
                      new InlineKeyboardButton() { Text = "New York City (Нет в наличии)", CallbackData = "NewYorkCityNF" },
                   };

                return NewYorkCity;
            }
        }

        public InlineKeyboardButton[] RedPoison(bool availability)
        {
            if (availability)
            {
                var RedPoison = new[]
                   {
                      new InlineKeyboardButton() { Text = "Red Poison", CallbackData = "RedPoison" }
                   };

                return RedPoison;
            }
            else
            {
                var RedPoison = new[]
                   {
                      new InlineKeyboardButton() { Text = "Red Poison (Нет в наличии)", CallbackData = "RedPoisonNF" },
                   };

                return RedPoison;
            }
        }

        public InlineKeyboardButton[] SuperSkunk(bool availability)
        {
            if (availability)
            {
                var SuperSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Super Skunk", CallbackData = "SuperSkunk" }
                   };

                return SuperSkunk;
            }
            else
            {
                var SuperSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Super Skunk (Нет в наличии)", CallbackData = "SuperSkunkNF" },
                   };

                return SuperSkunk;
            }
        }

        public InlineKeyboardButton[] CaliforniaOrange(bool availability)
        {
            if (availability)
            {
                var CaliforniaOrange = new[]
                   {
                      new InlineKeyboardButton() { Text = "California Orange", CallbackData = "CaliforniaOrange" }
                   };

                return CaliforniaOrange;
            }
            else
            {
                var CaliforniaOrange = new[]
                   {
                      new InlineKeyboardButton() { Text = "California Orange (Нет в наличии)", CallbackData = "CaliforniaOrangeNF" },
                   };

                return CaliforniaOrange;
            }
        }

        public InlineKeyboardButton[] Purple(bool availability)
        {
            if (availability)
            {
                var Purple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Purple", CallbackData = "Purple" }
                   };

                return Purple;
            }
            else
            {
                var Purple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Purple (Нет в наличии)", CallbackData = "PurpleNF" },
                   };

                return Purple;
            }
        }

        public InlineKeyboardButton[] DarkPurple(bool availability)
        {
            if (availability)
            {
                var DarkPurple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Dark Purple", CallbackData = "DarkPurple" }
                   };

                return DarkPurple;
            }
            else
            {
                var DarkPurple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Dark Purple (Нет в наличии)", CallbackData = "DarkPurpleNF" },
                   };

                return DarkPurple;
            }
        }

        public InlineKeyboardButton[] LemonSkunk(bool availability)
        {
            if (availability)
            {
                var LemonSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Lemon Skunk", CallbackData = "LemonSkunk" }
                   };

                return LemonSkunk;
            }
            else
            {
                var LemonSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Lemon Skunk (Нет в наличии)", CallbackData = "LemonSkunkNF" },
                   };

                return LemonSkunk;
            }
        }

        public InlineKeyboardButton[] CBDWhiteWidow(bool availability)
        {
            if (availability)
            {
                var CBDWhiteWidow = new[]
                   {
                      new InlineKeyboardButton() { Text = "CBD White Widow", CallbackData = "CBDWhiteWidow" }
                   };

                return CBDWhiteWidow;
            }
            else
            {
                var CBDWhiteWidow = new[]
                   {
                      new InlineKeyboardButton() { Text = "CBD White Widow (Нет в наличии)", CallbackData = "CBDWhiteWidowNF" },
                   };

                return CBDWhiteWidow;
            }
        }

        public InlineKeyboardButton[] Kush(bool availability)
        {
            if (availability)
            {
                var Kush = new[]
                   {
                      new InlineKeyboardButton() { Text = "Kush", CallbackData = "Kush" }
                   };

                return Kush;
            }
            else
            {
                var Kush = new[]
                   {
                      new InlineKeyboardButton() { Text = "Kush (Нет в наличии)", CallbackData = "KushNF" },
                   };

                return Kush;
            }
        }

        public InlineKeyboardButton[] ExodusCheeseCBD(bool availability)
        {
            if (availability)
            {
                var ExodusCheeseCBD = new[]
                   {
                      new InlineKeyboardButton() { Text = "Exodus Cheese CBD", CallbackData = "ExodusCheeseCBD" }
                   };

                return ExodusCheeseCBD;
            }
            else
            {
                var ExodusCheeseCBD = new[]
                   {
                      new InlineKeyboardButton() { Text = "Exodus Cheese CBD (Нет в наличии)", CallbackData = "ExodusCheeseCBDNF" },
                   };

                return ExodusCheeseCBD;
            }
        }

        public InlineKeyboardButton[] BlueberrySkunk(bool availability)
        {
            if (availability)
            {
                var BlueberrySkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Blueberry Skunk", CallbackData = "BlueberrySkunk" }
                   };

                return BlueberrySkunk;
            }
            else
            {
                var BlueberrySkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Blueberry Skunk (Нет в наличии)", CallbackData = "BlueberrySkunkNF" },
                   };

                return BlueberrySkunk;
            }
        }

        public InlineKeyboardButton[] CreamMandarin(bool availability)
        {
            if (availability)
            {
                var CreamMandarin = new[]
                   {
                      new InlineKeyboardButton() { Text = "Cream Mandarin", CallbackData = "CreamMandarin" }
                   };

                return CreamMandarin;
            }
            else
            {
                var CreamMandarin = new[]
                   {
                      new InlineKeyboardButton() { Text = "Cream Mandarin (Нет в наличии)", CallbackData = "CreamMandarinNF" },
                   };

                return CreamMandarin;
            }
        }

        public InlineKeyboardButton[] Lowryder2(bool availability)
        {
            if (availability)
            {
                var Lowryder2 = new[]
                   {
                      new InlineKeyboardButton() { Text = "Lowryder2", CallbackData = "Lowryder2" }
                   };

                return Lowryder2;
            }
            else
            {
                var Lowryder2 = new[]
                   {
                      new InlineKeyboardButton() { Text = "Lowryder2 (Нет в наличии)", CallbackData = "Lowryder2NF" },
                   };

                return Lowryder2;
            }
        }

        public InlineKeyboardButton[] Cheese(bool availability)
        {
            if (availability)
            {
                var Cheese = new[]
                   {
                      new InlineKeyboardButton() { Text = "Cheese", CallbackData = "Cheese" }
                   };

                return Cheese;
            }
            else
            {
                var Cheese = new[]
                   {
                      new InlineKeyboardButton() { Text = "Cheese (Нет в наличии)", CallbackData = "CheeseNF" },
                   };

                return Cheese;
            }
        }

        public InlineKeyboardButton[] DarkDevil(bool availability)
        {
            if (availability)
            {
                var DarkDevil = new[]
                   {
                      new InlineKeyboardButton() { Text = "Dark Devil", CallbackData = "DarkDevil" }
                   };

                return DarkDevil;
            }
            else
            {
                var DarkDevil = new[]
                   {
                      new InlineKeyboardButton() { Text = "Dark Devil (Нет в наличии)", CallbackData = "DarkDevilNF" },
                   };

                return DarkDevil;
            }
        }

        public InlineKeyboardButton[] OGKush(bool availability)
        {
            if (availability)
            {
                var OGKush = new[]
                   {
                      new InlineKeyboardButton() { Text = "OG Kush", CallbackData = "OGKush" }
                   };

                return OGKush;
            }
            else
            {
                var OGKush = new[]
                   {
                      new InlineKeyboardButton() { Text = "OG Kush (Нет в наличии)", CallbackData = "OGKushNF" },
                   };

                return OGKush;
            }
        }

        public InlineKeyboardButton[] SourDieselHaze(bool availability)
        {
            if (availability)
            {
                var SourDieselHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "Sour Diesel Haze", CallbackData = "SourDieselHaze" }
                   };

                return SourDieselHaze;
            }
            else
            {
                var SourDieselHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "Sour Diesel Haze (Нет в наличии)", CallbackData = "SourDieselHazeNF" },
                   };

                return SourDieselHaze;
            }
        }

        public InlineKeyboardButton[] Vertigo(bool availability)
        {
            if (availability)
            {
                var Vertigo = new[]
                   {
                      new InlineKeyboardButton() { Text = "Vertigo", CallbackData = "Vertigo" }
                   };

                return Vertigo;
            }
            else
            {
                var Vertigo = new[]
                   {
                      new InlineKeyboardButton() { Text = "Vertigo (Нет в наличии)", CallbackData = "VertigoNF" },
                   };

                return Vertigo;
            }
        }

        public InlineKeyboardButton[] Skunk1(bool availability)
        {
            if (availability)
            {
                var Skunk1 = new[]
                   {
                      new InlineKeyboardButton() { Text = "Skunk#1", CallbackData = "Skunk1" }
                   };

                return Skunk1;
            }
            else
            {
                var Skunk1 = new[]
                   {
                      new InlineKeyboardButton() { Text = "Skunk#1 (Нет в наличии)", CallbackData = "Skunk1NF" },
                   };

                return Skunk1;
            }
        }

        public InlineKeyboardButton[] Pineapple(bool availability)
        {
            if (availability)
            {
                var Pineapple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Pineapple", CallbackData = "Pineapple" }
                   };

                return Pineapple;
            }
            else
            {
                var Pineapple = new[]
                   {
                      new InlineKeyboardButton() { Text = "Pineapple (Нет в наличии)", CallbackData = "PineappleNF" },
                   };

                return Pineapple;
            }
        }

        public InlineKeyboardButton[] SuperLemonHaze(bool availability)
        {
            if (availability)
            {
                var SuperLemonHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "Super Lemon Haze", CallbackData = "SuperLemonHaze" }
                   };

                return SuperLemonHaze;
            }
            else
            {
                var SuperLemonHaze = new[]
                   {
                      new InlineKeyboardButton() { Text = "Super Lemon Haze (Нет в наличии)", CallbackData = "SuperLemonHazeNF" },
                   };

                return SuperLemonHaze;
            }
        }

        public InlineKeyboardButton[] BigSkunk(bool availability)
        {
            if (availability)
            {
                var BigSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Big Skunk", CallbackData = "BigSkunk" }
                   };

                return BigSkunk;
            }
            else
            {
                var BigSkunk = new[]
                   {
                      new InlineKeyboardButton() { Text = "Big Skunk (Нет в наличии)", CallbackData = "BigSkunkNF" },
                   };

                return BigSkunk;
            }
        }

        public InlineKeyboardButton[] WhiteWidowXXL(bool availability)
        {
            if (availability)
            {
                var WhiteWidowXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "White Widow XXL", CallbackData = "WhiteWidowXXL" }
                   };

                return WhiteWidowXXL;
            }
            else
            {
                var WhiteWidowXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "White Widow XXL (Нет в наличии)", CallbackData = "WhiteWidowXXLNF" },
                   };

                return WhiteWidowXXL;
            }
        }

        public InlineKeyboardButton[] AmneziaXXL(bool availability)
        {
            if (availability)
            {
                var AmneziaXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "Amnezia XXL", CallbackData = "AmneziaXXL" }
                   };

                return AmneziaXXL;
            }
            else
            {
                var AmneziaXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "Amnezia XXL (Нет в наличии)", CallbackData = "AmneziaXXLNF" },
                   };

                return AmneziaXXL;
            }
        }

        public InlineKeyboardButton[] BigBudXXL(bool availability)
        {
            if (availability)
            {
                var BigBudXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "Big Bud XXL", CallbackData = "BigBudXXL" }
                   };

                return BigBudXXL;
            }
            else
            {
                var BigBudXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "Big Bud XXL (Нет в наличии)", CallbackData = "BigBudXXLNF" },
                   };

                return BigBudXXL;
            }
        }

        public InlineKeyboardButton[] GirlScoutCookiesXXL(bool availability)
        {
            if (availability)
            {
                var GirlScoutCookiesXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "GirlScout Cookies XXL", CallbackData = "GirlScoutCookiesXXL" }
                   };

                return GirlScoutCookiesXXL;
            }
            else
            {
                var GirlScoutCookiesXXL = new[]
                   {
                      new InlineKeyboardButton() { Text = "GirlScout Cookies XXL (Нет в наличии)", CallbackData = "GirlScoutCookiesXXLNF" },
                   };

                return GirlScoutCookiesXXL;
            }
        }

        public InlineKeyboardButton[] ElAlquimista(bool availability)
        {
            if (availability)
            {
                var ElAlquimista = new[]
                   {
                      new InlineKeyboardButton() { Text = "El Alquimista", CallbackData = "ElAlquimista" }
                   };

                return ElAlquimista;
            }
            else
            {
                var ElAlquimista = new[]
                   {
                      new InlineKeyboardButton() { Text = "El Alquimista (Нет в наличии)", CallbackData = "ElAlquimistaNF" },
                   };

                return ElAlquimista;
            }
        }

        public InlineKeyboardButton[] DeliciousCandy(bool availability)
        {
            if (availability)
            {
                var DeliciousCandy = new[]
                   {
                      new InlineKeyboardButton() { Text = "Delicious Candy", CallbackData = "DeliciousCandy" }
                   };

                return DeliciousCandy;
            }
            else
            {
                var DeliciousCandy = new[]
                   {
                      new InlineKeyboardButton() { Text = "Delicious Candy (Нет в наличии)", CallbackData = "DeliciousCandyNF" },
                   };

                return DeliciousCandy;
            }
        }

        public InlineKeyboardButton[] NorthernLightBlue(bool availability)
        {
            if (availability)
            {
                var NorthernLightBlue = new[]
                   {
                      new InlineKeyboardButton() { Text = "Northern Light Blue", CallbackData = "NorthernLightBlue" }
                   };

                return NorthernLightBlue;
            }
            else
            {
                var NorthernLightBlue = new[]
                   {
                      new InlineKeyboardButton() { Text = "Northern Light Blue (Нет в наличии)", CallbackData = "NorthernLightBlueNF" },
                   };

                return NorthernLightBlue;
            }
        }
    }
}