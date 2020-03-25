using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public class ModuleObject : ModelBase
    {
        private String _name;
        public bool Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        
        private int _address;
        public int Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (!value.Equals(_priority))
                {
                    _priority = value;
                    NotifyPropertyChanged("Priority");
                }
            }
        }

        private string _connections;
        public string Connections
        {
            get { return _connections; }
            set
            {
                if (!value.Equals(_connections))
                {
                    _connections = value;
                    NotifyPropertyChanged("Connections");
                }
            }
        }

        public ModuleObject() { }
    }

}