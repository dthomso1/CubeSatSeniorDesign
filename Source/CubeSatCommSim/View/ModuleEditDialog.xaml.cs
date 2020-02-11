using System.Windows;
using System.Windows.Controls;
using CubeSatCommSim.Model;
using CubeSatCommSim.ViewModel;

namespace CubeSatCommSim.View
{
    /// <summary>
    /// Interaction logic for ModuleEditDialog.xaml
    /// </summary>
    public partial class ModuleEditDialog : Window
    {
        public object BusListDataContext
        {
            get
            {
                return BusList.DataContext;
            }
            set
            {
                BusList.DataContext = value;
            }
        }
        private InternalSimController simController;

        public ModuleEditDialog(InternalSimController controller)
        {
            simController = controller;
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Module module = ((ModuleVM)DataContext).md;
            Bus bus = (Bus)((CheckBox)sender).DataContext;
            module.ConnectBus((CSPBus)bus);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Module module = ((ModuleVM)DataContext).md;
            Bus bus = (Bus)((CheckBox)sender).DataContext;
            module.DisconnectBus(bus);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(
                    "Are you sure you want to delete '" + ((ModuleVM)DataContext).Name + "'?",
                    "Deleting Module",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
            {
                simController.RemoveModule(((ModuleVM)DataContext).md);
                EventLog.AddLog(new SimEvent("Module '" + ((ModuleVM)DataContext).Name + "' was deleted", EventSeverity.INFO));
                DialogResult = true;
                Close();
            }
        }
    }
}
