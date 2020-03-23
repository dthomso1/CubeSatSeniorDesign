using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.IO;


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

        public static void SaveLog()
        {
            //how to print to new file every month?
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\EventLog.txt");
            string filename = Path.GetFileName("C:\\Users\\Maurice\\source\\repos\\dthomso1\\CubeSatSeniorDesign\\Source\\CubeSatCommSim\\Data");
            foreach (SimEvent data in EventList) {
                Console.WriteLine(data.ToString(), Path.Combine("C:\\Users\\Maurice\\source\\repos\\dthomso1\\CubeSatSeniorDesign\\Source\\CubeSatCommSim\\Data"));
                //Console.WriteLine(data.ToString(), filepath);
            }
            
        }
    }
}
