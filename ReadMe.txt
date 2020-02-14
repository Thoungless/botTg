Настройка самого бота:

botseeds\Models\AppSettings.cs - поставить свои значения

botseeds\Models\Commands\HowToOrder.cs - поставить ссылку на видео как пользоваться ботом

Настройка яндекс кошелька:

регистрируешь приложение https://yandex.ru/dev/money/doc/dg/concepts/About-docpage/ тут написано что и как (redirect_url) - ссылка на бота t.me/изернейм бота

переходишь по этой ссылке - https://m.sp-money.yandex.ru/oauth/authorize?client_id=(твой клиент айди)&response_type=code&redirect_uri=http%3A%2F%2Ft.me%2F(username бота)&scope=account-info%20operation-history%20operation-details%20payment-p2p
получаешь ответ в формате json, копируешь код

botseeds\Models\HttpRequests.cs - поставить свой API яндекса
botseeds\Models\InlineKeyboards\InlineKeyboard.cs - строка 238, там будут комментарии куда надо поставить свою ссылку


Настройка базы данных:

Регистрируешься тут - mongodb.com
Создаешь базу данных
В коллекциях создаешь базу User и в ней коллекцию userId


botseeds\Models\database.cs - поставить своё значение в строку подключения