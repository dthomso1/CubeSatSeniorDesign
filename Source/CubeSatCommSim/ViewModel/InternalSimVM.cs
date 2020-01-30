using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Collections.Specialized;
using System.ComponentModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;
using System.Windows.Input;

namespace CubeSatCommSim.ViewModel
{
    class InternalSimVM : ViewModelBase
    {
        private InternalSimController md;

        public ICollectionView Modules { get; private set; }
        public ICollectionView Buses { get; private set; }

        RelayCommand _ModuleEditCommand;
        public ICommand ModuleEditCommand
        {
            get
            {
                if (_ModuleEditCommand == null)
                {
                    _ModuleEditCommand = new RelayCommand(param => this.EditModule(param));
                }
                return _ModuleEditCommand;
            }
        }

        RelayCommand _BusEditCommand;
        public ICommand BusEditCommand
        {
            get
            {
                if (_BusEditCommand == null)
                {
                    _BusEditCommand = new RelayCommand(param => this.EditBus(param));
                }
                return _BusEditCommand;
            }
        }

        public InternalSimVM(InternalSimController model)
        {
            md = model;
            Modules = CollectionViewSource.GetDefaultView(md.Modules);
            Buses = CollectionViewSource.GetDefaultView(md.Buses);
        }

        //Opens module edit dialog on the clicked module
        public void EditModule(object parameter)
        {
            Module module = (Module)parameter;
            ModuleVM moduleVM = new ModuleVM(module);
            ModuleEditDialog editDialog = new ModuleEditDialog();
            editDialog.DataContext = moduleVM;
            editDialog.BusListDataContext = Buses;
            editDialog.ShowDialog();

            //Forces binding of button text to update
            Modules.Refresh();
        }

        //Opens bus edit dialog on the clicked bus
        public void EditBus(object parameter)
        {
            if (parameter is CSPBus)
            {
                CSPBusEditDialog editDialog = new CSPBusEditDialog();
                CSPBusVM busVM = new CSPBusVM((CSPBus)parameter);
                editDialog.DataContext = busVM;
                editDialog.ShowDialog();
                Buses.Refresh();
            }
        }
    }
}
