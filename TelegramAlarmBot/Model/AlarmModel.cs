using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramAlarmBot.Model
{
    public class AlarmModel
    {
        public string Alarm_name { get; set; }
        public DateTime Date { get; set; }
        public string Repeat { get; set; }
        public string Hour{ get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }

    }
}
