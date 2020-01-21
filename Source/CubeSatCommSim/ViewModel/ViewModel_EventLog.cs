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

        public void ClearLog_vm() => md.ClearLog();

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