using System.Timers;
using Telegram.Bot;
using TelegramAlarmBot.Helper;
using TelegramAlarmBot.Model;
using static System.Net.Mime.MediaTypeNames;

// GLXY : -1001924922401
// DENEME: -1001965373215
// 23.05.2023-09:30

try
{
    new Alarm().getAlarm();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Thread.Sleep(100000000);
    throw;
}
Console.ReadKey();
