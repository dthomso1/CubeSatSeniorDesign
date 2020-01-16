using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public class EventLog : ModelBase
    {
        private ObservableCollection<SimEvent> _EventList;
        public ObservableCollection<SimEvent> EventList {
            get {
                return _EventList;
            }
            set {
                _EventList = value;
                NotifyPropertyChanged("EventList");
            }
        }
        public EventLog()
        {
            EventList = new ObservableCollection<SimEvent>();

            //Test data
            SimEvent a = new SimEvent("Test message 1", EventSeverity.INFO);
            SimEvent b = new SimEvent("Test message 2", EventSeverity.WARNING);
            SimEvent c = new SimEvent("Test message 3", EventSeverity.ERROR);
            SimEvent d = new SimEvent("Test message 4", EventSeverity.FATAL_ERROR);
            EventList.Add(a);
            EventList.Add(b);
            EventList.Add(c);
            EventList.Add(d);
            EventList.Add(b);
            EventList.Add(c);
            EventList.Add(a);
            EventList.Add(d);
            EventList.Add(c);
        }

        public void AddLog(SimEvent log)
        {
            _EventList.Add(log);            
        }

        public void ClearLog()
        {
            _EventList.Clear();
        }
    }
}
