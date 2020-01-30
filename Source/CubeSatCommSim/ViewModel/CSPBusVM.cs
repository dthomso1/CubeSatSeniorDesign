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
    class CSPBusVM : ViewModelBase
    {
        private CSPBus md;
        
        public string Name
        {
            get { return md.Name; }
            set
            {
                md.Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public int DataRate
        {
            get { return md.DataRate; }
            set
            {
                md.DataRate = value;
                NotifyPropertyChanged("DataRate");
            }
        }

        public CSPBusVM(CSPBus model)
        {
            md = model;
        }
    }
}
