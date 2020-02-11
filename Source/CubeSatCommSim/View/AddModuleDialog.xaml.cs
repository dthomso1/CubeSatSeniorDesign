using System.Windows;
using System.Windows.Controls;
using CubeSatCommSim.Model;
using CubeSatCommSim.ViewModel;

namespace CubeSatCommSim.View
{
    /// <summary>
    /// Interaction logic for ModuleEditDialog.xaml
    /// </summary>
    public partial class AddModuleDialog : Window
    {
        public AddModuleDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
