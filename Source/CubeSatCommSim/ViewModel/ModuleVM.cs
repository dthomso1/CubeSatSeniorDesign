using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;


namespace CubeSatCommSim.ViewModel
{
    class ModuleVM : ViewModelBase
    {
        public readonly Module md;        
        
        public string Name
        {
            get { return md.Name; }
            set
            {
                md.Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        
        public int Address
        {
            get { return md.Address; }
            set
            {
                md.Address = value;
                NotifyPropertyChanged("Address");
            }
        }
        
        public int Priority
        {
            get { return md.Priority; }
            set
            {
                md.Priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
        
        public ModuleVM(Module model)
        {
            md = model;
        }
    }
}
