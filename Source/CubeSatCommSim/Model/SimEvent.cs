using System.ComponentModel;

namespace CubeSatCommSim.Model
{
    public class SimEvent : ModelBase
    {
        private string _Log;
        public string Log {
            get {
                return _Log;
            }
            set{
                _Log = value;
                NotifyPropertyChanged("Log");
            }
        }
        public SimEvent(string log)
        {
            Log = log;
        }
    }
}
