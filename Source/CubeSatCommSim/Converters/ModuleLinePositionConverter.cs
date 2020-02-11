using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Data;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.ViewModel;

namespace CubeSatCommSim.Converters
{
    class ModuleLinePositionConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var buses = (ListCollectionView)values[0];
            var bus = (Bus)values[1];
            var param = int.Parse((string)parameter);

            var listIndex = buses.IndexOf(bus);
            double length = param * (listIndex + 1) + (param / 2.0f);
            return -length;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
