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
            _EventList = new ObservableCollection<SimEvent>();
            SimEvent a = new SimEvent("Hello");
            SimEvent b = new SimEvent("Good Bye");
            SimEvent c = new SimEvent("World");
            _EventList.Add(a);
            _EventList.Add(b);
            _EventList.Add(c);
        }

        public void addLog(SimEvent log)
        {
            _EventList.Add(log);
        }

        public void deleteLog()
        {
            _EventList.Clear();
        }
    }
}
