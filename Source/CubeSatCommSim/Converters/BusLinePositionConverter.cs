using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.ViewModel;

namespace CubeSatCommSim.Converters
{
    class BusLinePositionConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var bus = (Bus)values[0];
            var simVM = (InternalSimVM)values[1];
            var param = int.Parse((string)parameter);

            if(bus.ConnectedModules.Count == 0)
            {
                return 0;
            }

            var lastModuleIndex = 0;
            foreach(Module connectedModule in bus.ConnectedModules) 
            {
                lastModuleIndex = Math.Max(lastModuleIndex, ((ObservableCollection<Module>)(simVM.Modules.SourceCollection)).IndexOf(connectedModule));
            }
            double length =  param * (lastModuleIndex + 1) - (param / 2.0f);
            return length;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
