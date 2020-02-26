using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public abstract class Bus : ModelBase
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private ObservableCollection<Module> _ConnectedModules;
        public ObservableCollection<Module> ConnectedModules
        {
            get { return _ConnectedModules; }
            private set
            {
                _ConnectedModules = value;
                NotifyPropertyChanged("ConnectedModules");
            }
        }

        private bool _Idle;
        public bool Idle
        {
            get { return _Idle; }
            set
            {
                _Idle = value;
                NotifyPropertyChanged("Idle");
            }
        }

        public Bus(string name)
        {
            Idle = true;
            Name = name;
            ConnectedModules = new ObservableCollection<Module>();
        }

        public abstract void Process(int step);

        public void ConnectModule(Module newModule)
        {
            if (!ConnectedModules.Contains(newModule))
            {
                ConnectedModules.Add(newModule);
            }
        }

        public void DisconnectModule(Module module)
        {
            if (ConnectedModules.Contains(module))
            {
                ConnectedModules.Remove(module);
            }
        }
    }
}
