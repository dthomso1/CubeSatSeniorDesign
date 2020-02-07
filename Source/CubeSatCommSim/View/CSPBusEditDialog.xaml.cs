using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CubeSatCommSim.Model;
using CubeSatCommSim.ViewModel;

namespace CubeSatCommSim.View
{
    /// <summary>
    /// Interaction logic for CSPBusEditDialog.xaml
    /// </summary>
    public partial class CSPBusEditDialog : Window
    {
        private InternalSimController simController;

        public CSPBusEditDialog(InternalSimController controller)
        {
            simController = controller;
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                    "Are you sure you want to delete '" + ((CSPBusVM)DataContext).Name + "'?",
                    "Deleting Bus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                ) == MessageBoxResult.Yes)
            {
                simController.RemoveBus(((CSPBusVM)DataContext).md);
                EventLog.AddLog(new SimEvent("Bus '" + ((CSPBusVM)DataContext).Name + "' was deleted", EventSeverity.INFO));
                DialogResult = true;
                Close();
            }
        }
    }
}
