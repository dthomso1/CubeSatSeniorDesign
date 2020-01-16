using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;

namespace CubeSatCommSim.ViewModel
{
    public class ViewModel_EventLog : ViewModelBase
    {
        private EventLogView vw;
        public ObservableCollection<SimEvent> EventList {
            get { return EventLog.EventList; }
        }

        public ViewModel_EventLog(EventLogView vw)
        {
            this.vw = vw;
            vw.DataContext = EventList;
        }
    }
}