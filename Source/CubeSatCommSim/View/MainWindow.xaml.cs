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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CubeSatCommSim.ViewModel;
using CubeSatCommSim.Model;

namespace CubeSatCommSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InternalSimController IntSimController;
        private InternalSimVM IntSimControllerVM;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntSimController = new InternalSimController();
            IntSimControllerVM = new InternalSimVM(IntSimController);
            InternalSimViewPanel.DataContext = IntSimControllerVM;
            MenuBarStart.DataContext = IntSimControllerVM;
            MenuBarStop.DataContext = IntSimControllerVM;
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            IntSimController.RunSim();
        }

        private void StopSimulation_Click(object sender, RoutedEventArgs e)
        {
            IntSimController.StopSim();
        }
    }
}
