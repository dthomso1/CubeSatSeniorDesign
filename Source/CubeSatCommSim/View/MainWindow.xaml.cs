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
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TEMP CODE FOR TESTING
            CSPBus CSPBus1 = new CSPBus("CSPBus1");
            Module Module1 = new Module("Module1", 0);
            Module Module2 = new Module("Module2", 1);

            //Temporary loop of 10 steps
            for (int step = 1; step < 11; step++)
            {
                Console.WriteLine("Step: " + step);
                //Example simulation event sequence
                switch (step)
                {
                    case 1:
                        Module1.ConnectCSP(CSPBus1);
                        break;
                    case 2:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
                        break;
                    case 4:
                        Module2.ConnectCSP(CSPBus1);
                        break;
                    case 6:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
                        break;
                    case 7:
                        Module1.SendCSPPacket(CSPBus1, 2, 0, 0, 0, 1);
                        break;
                    default:
                        break;
                }

                CSPBus1.Process(step);
            }
            //END OF TEST CODE
        }
    }
}
