using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;
using System.Windows.Input;

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

        public void ClearLog_vm() => EventLog.ClearLog();

        RelayCommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param => this.ClearLog_vm());
                }
                return _clearCommand;
            }
        }
    }
}