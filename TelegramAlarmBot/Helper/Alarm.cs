using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Requests;
using TelegramAlarmBot.Model;

namespace TelegramAlarmBot.Helper
{
    public class Alarm
    {
        public void getAlarm()
        {

            Console.WriteLine(DateTime.Now.ToString("HH:mm"));
            Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
            #region alrmbot
            List<AlarmModel> alarmList = new List<AlarmModel>();
            Data data = new Data();

            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += TimerElapsed;
            timer.Start();
            Console.WriteLine("Timer başlatıldı. Durdurmak için herhangi bir tuşa basın.");
            Console.ReadKey();

            async void TimerElapsed(object sender, ElapsedEventArgs e)
            {
                var currentTime = DateTime.UtcNow;
                alarmList = new Data().GetData();
                foreach (var item in alarmList)
                {
                    if (!item.Active)
                    {
                        continue;
                    }
                    if (!item.Repeat.Equals("Everyday"))
                    {
                        if ((DateTime.Parse(item.Repeat) <= DateTime.Parse(currentTime.ToString("mm/DD/yyyy"))) && (DateTime.Parse(item.Hour) < DateTime.Parse(currentTime.ToString("HH:mm"))))
                        {
                            Console.WriteLine("sleep1");
                            Thread.Sleep(10000);
                             Console.WriteLine(currentTime + " -> " + item.Message.ToString(new CultureInfo("ru-Ru")));
                            data.DeleteData(item, false);
                        }
                    }
                    if ((item.Hour == currentTime.ToString("HH:mm")) && (item.Repeat.Equals("Everyday") || item.Repeat == currentTime.ToString("mm/DD/yyyy")))
                    {
                        Console.WriteLine("sleep");
                        Thread.Sleep(10000);
                        Console.WriteLine(currentTime + " -> " + item.Message.ToString());
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

        }
    }
}