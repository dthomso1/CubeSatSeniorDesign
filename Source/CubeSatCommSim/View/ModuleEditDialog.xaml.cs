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

        public ModuleEditDialog()
        {
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
    }
}
