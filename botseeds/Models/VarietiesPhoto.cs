using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;

namespace botseeds.Models
{
    public class VarietiesPhoto
    {
        public InlineKeyboardButton[] Bubblicious(bool availability)
        {
            if (availability)
            {
                var Bubblicious = new[] 
                   {
                      new InlineKeyboardButton() { Text = "Bubblicious", CallbackData = "Bubblicious" }
                   };

                return Bubblicious;
            }
            else
            {
                var Bubblicious = new[]
                   {
                      new InlineKeyboardButton() { Text = "Bubblicious (Нет в наличии)", CallbackData = "BubbliciousNF" },                     
                   };

                return Bubblicious;
            }
        }
    }
}