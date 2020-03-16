﻿using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public static class EventLog
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

        public static void SaveLog() {
            //EventList.Save();
        }
    }
}
