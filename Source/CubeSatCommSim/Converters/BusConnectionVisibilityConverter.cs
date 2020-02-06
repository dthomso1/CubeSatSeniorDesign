using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Windows.Data;
using CubeSatCommSim.Model;

namespace CubeSatCommSim.Converters
{
    class BusConnectionVisibilityConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var connectedModules = (IEnumerable<Module>)values[0];
            var module = (Module)values[1];
            if (connectedModules.Contains(module))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
