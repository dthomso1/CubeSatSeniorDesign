using System.ComponentModel;

namespace CubeSatCommSim.Model
{
    public enum EventSeverity
    {
        INFO,
        WARNING,
        ERROR,
        FATAL_ERROR,
        IMPORTANT
    }

    public class SimEvent : ModelBase
    {
        private string _Log;
        public string Log {
            get { return _Log; }
            set{
                _Log = value;
                NotifyPropertyChanged("Log");
            }
        }

        private EventSeverity _Severity;
        public EventSeverity Severity
        {
            get { return _Severity; }
            set
            {
                if(_Severity != value)
                {
                    _Severity = value;
                    NotifyPropertyChanged("Severity");
                }
            }
        }
        
        public SimEvent(string log, EventSeverity severity)
        {
            Log = log;
            Severity = severity;
        }

        public override string ToString()
        {
            return "[" + Severity.ToString() + "] " + Log;
        }
    }
}
