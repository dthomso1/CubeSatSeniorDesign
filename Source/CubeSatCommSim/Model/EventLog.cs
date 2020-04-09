using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public class EventLog
    {
        public static ObservableCollection<SimEvent> EventList { get; private set; } = new ObservableCollection<SimEvent>();
        

        public static void AddLog(SimEvent log)
        {
            EventList.Add(log);            
        }

        public static void ClearLog()
        {
            EventList.Clear();
        }
        public static string[] writeLog()
        {
            string[] stringOut = new string[100];
            int i = 0;

            foreach(SimEvent c in EventList)
            {
                stringOut[i] = c.ToString();
                i++;
            }

            return stringOut;
        }
    }
}
