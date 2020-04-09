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

        RelayCommand _AddModuleCommand;
        public ICommand AddModuleCommand
        {
            get
            {
                if(_AddModuleCommand == null)
                {
                    _AddModuleCommand = new RelayCommand(param => this.AddModule());
                }
                return _AddModuleCommand;
            }
        }

        RelayCommand _AddBusCommand;
        public ICommand AddBusCommand
        {
            get
            {
                if (_AddBusCommand == null)
                {
                    _AddBusCommand = new RelayCommand(param => this.AddBus());
                }
                return _AddBusCommand;
            }
        }
        
        public bool LoopSimulation
        {
            get { return md.LoopSimulation; }
            set
            {
                md.LoopSimulation = value;
                NotifyPropertyChanged("LoopSimulation");
            }
        }

        public bool SimulationRunning
        {
            get { return md.SimulationRunning; }
        }

        public InternalSimVM(InternalSimController model)
        {
            md = model;
            Modules = CollectionViewSource.GetDefaultView(md.Modules);
            Buses = CollectionViewSource.GetDefaultView(md.Buses);
            md.PropertyChanged += ModelPropertyChanged;
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        //Opens module edit dialog on the clicked module
        public void EditModule(object parameter)
        {
            Module module = (Module)parameter;
            ModuleVM moduleVM = new ModuleVM(module);
            ModuleEditDialog editDialog = new ModuleEditDialog(md);
            editDialog.DataContext = moduleVM;
            editDialog.BusListDataContext = Buses;
            editDialog.ShowDialog();

            //Forces binding of button text to update
            Modules.Refresh();
            Buses.Refresh();
        }

        //Opens bus edit dialog on the clicked bus
        public void EditBus(object parameter)
        {
            if (parameter is CSPBus)
            {
                CSPBusEditDialog editDialog = new CSPBusEditDialog(md);
                CSPBusVM busVM = new CSPBusVM((CSPBus)parameter);
                editDialog.DataContext = busVM;
                editDialog.ShowDialog();
                Buses.Refresh();
            }
        }

        public void AddModule()
        {
            Module m;
            if (Modules.IsEmpty)
            {
                m = new Module("New Module", 0, 0);
            }
            else
            {
                m = new Module("New module", Math.Min(md.Modules.Last().Address + 1, 31), 0);
            }
            ModuleVM vm = new ModuleVM(m);
            AddModuleDialog dlg = new AddModuleDialog();
            dlg.DataContext = vm;

            if (dlg.ShowDialog() == true)
            {
                md.Modules.Add(m);
                EventLog.AddLog(new SimEvent("A new module was added: '" + m.Name + "'", EventSeverity.INFO));
            }
        }

        public void AddBus()
        {
            CSPBus b = new CSPBus("New bus", 1);
            CSPBusVM vm = new CSPBusVM(b);
            AddBusDialog dlg = new AddBusDialog();
            dlg.DataContext = vm;

            if(dlg.ShowDialog() == true)
            {
                md.Buses.Add(b);
                EventLog.AddLog(new SimEvent("A new bus was added: '" + b.Name + "'", EventSeverity.INFO));
            }
        }
    }
}
