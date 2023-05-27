using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using TelegramAlarmBot.Model;

namespace TelegramAlarmBot.Helper
{
    public class Data
    {
        public static IFirebaseConfig firebaseConfig = new FirebaseConfig()
        {
            AuthSecret = "ylHpuuaQbcZJgPFjGz2afb5PTtkL0p8FEg0wbeE0",
            BasePath = "https://twds-alarm-default-rtdb.firebaseio.com/"
        };

        public static FireSharp.FirebaseClient client = new FireSharp.FirebaseClient(firebaseConfig);

        public List<AlarmModel> GetData()
        {
            var response = client.Get("Alarms");
            var result = response.Body;
            var data = JsonConvert.DeserializeObject<Dictionary<string, AlarmModel>>(result);

            var listModel = new List<AlarmModel>();

            foreach (var item in data)
            {
                item.Value.Alarm_name = item.Key;
                listModel.Add(item.Value);
            }

            return listModel;
        }

        public void DeleteData(AlarmModel model, bool status)
        {
            model.Active = status;
            client.Update("Alarms/" + model.Alarm_name, model);
        }

    }
}
