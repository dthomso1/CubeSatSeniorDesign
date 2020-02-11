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


namespace CubeSatCommSim.View
{
    /// <summary>
    /// Interaction logic for ExternalSimView.xaml
    /// </summary>
    public partial class ExternalSimView : UserControl
    {
        public ExternalSimView()
        {
            var vm = new ErrorDataViewModel(this);
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
