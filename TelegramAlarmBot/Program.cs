using System.Globalization;
using System.Timers;
using Telegram.Bot;
using TelegramAlarmBot.Helper;
using TelegramAlarmBot.Model;
using static System.Net.Mime.MediaTypeNames;

// GLXY : -1001924922401
// DENEME: -1001965373215
// 23.05.2023-09:30

Console.WriteLine(DateTime.UtcNow);
Console.WriteLine(DateTime.Now);
#region alrmbot

List<AlarmModel> alarmList = new List<AlarmModel>();
Data data = new Data();

System.Timers.Timer timer = new System.Timers.Timer(60000);
timer.Elapsed += TimerElapsed;
timer.Start();
Console.WriteLine("Timer başlatıldı. Durdurmak için herhangi bir tuşa basın.");
Console.ReadKey();

async void TimerElapsed(object sender, ElapsedEventArgs e)
{

    alarmList = new Data().GetData();

    foreach (var item in alarmList.Where(x => x.Active == true))
    {
        var currentTime = DateTime.UtcNow;
        var parseHour = DateTime.Parse(item.Hour);

        if (String.IsNullOrEmpty(item.Repeat))
        {
            if (item.Date.Date <= DateTime.UtcNow.Date && parseHour < DateTime.Parse(currentTime.ToString("HH:mm")))
            {
                data.DeleteData(item, false);
            }
        }
        if ((!String.IsNullOrEmpty(item.Repeat) || item.Date.Date == DateTime.UtcNow.Date) && parseHour == DateTime.Parse(currentTime.ToShortTimeString()))
        {
            var botClient = new TelegramBotClient("6139678513:AAFIqbFynW0Xt_kDEEBzgke_S2GYak7iqjQ");
            await botClient.SendTextMessageAsync(
#if DEBUG
            chatId: "-1001965373215",
#else
            chatId: "-1001924922401",
#endif
            text: item.Message.ToString());
        }
    }
}
#endregion }
