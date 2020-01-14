using System.ComponentModel;
using System.Collections.Generic;

namespace CubeSatCommSim.ViewModel
{
    public class ViewModel_EventLog : ViewModelBase
    {
        private Model.EventLog _EventLogger;
        public Model.EventLog EventLogger {
            get {
                return _EventLogger;
            }
            set {
                _EventLogger = value;
                NotifyPropertyChanged("EventLogger");
            }
        }
    }
}