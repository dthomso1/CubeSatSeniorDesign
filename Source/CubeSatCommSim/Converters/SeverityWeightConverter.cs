using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CubeSatCommSim.Model;

namespace CubeSatCommSim.Converters
{
    class SeverityWeightConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((EventSeverity)value)
            {
                case EventSeverity.IMPORTANT:
                    return FontWeights.Bold;
                default:
                    return FontWeights.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
