using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;

namespace CubeSatCommSim.ViewModel
{
    public class ViewModel_EventLog : ViewModelBase
    {
        private EventLog md;
        private EventLogView vw;
        public ObservableCollection<SimEvent> EventList {
            get { return md.EventList; }
        }

        public ViewModel_EventLog(EventLogView vw)
        {
            this.vw = vw;
            md = new EventLog();
            vw.DataContext = EventList;
        }
    }
}