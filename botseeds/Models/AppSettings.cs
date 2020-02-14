using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace botseeds.Models
{
    public class AppSettings
    {
        public static string Url { get; set; } = "()/{0}"; // Url хостинга

        public static string Key { get; set; } = ""; // Твой API ключ бота
    }
}