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

namespace CubeSatCommSim.View
{
    /// <summary>
    /// Interaction logic for EventLogView.xaml
    /// </summary>
    public partial class EventLogView : UserControl
    {
        public EventLogView()
        {
            var viewModel = new ViewModel.ViewModel_EventLog();
            DataContext = viewModel;
            InitializeComponent();
            /*
            TextBlock printTextBlock = new TextBlock();
            printTextBlock.Text = "Hello, World!";
            this.EventLogPanel.Children.Add(printTextBlock);
            Button btn = new Button();
            btn.Content = "Dynamic Button";
            this.EventLogPanel.Children.Add(btn);
            */
        }
    }
}



/*
 
        <TextBlock Text="{Binding Path=FirstName}" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
        <TextBlock Text="Placeholder"/>
     */
