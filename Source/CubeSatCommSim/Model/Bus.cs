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
        
        public Bus(string name)
        {
            Name = name;
            ConnectedModules = new ObservableCollection<Module>();
        }

        public abstract void Process(int step);

        public void ConnectModule(Module newModule)
        {
            ConnectedModules.Add(newModule);
        }
    }
}
