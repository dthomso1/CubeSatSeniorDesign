using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using CubeSatCommSim.Model;

namespace CubeSatCommSim.Converters
{
    class SeverityColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((EventSeverity)value)
            {
                case EventSeverity.FATAL_ERROR:
                    return Brushes.DarkRed;
                case EventSeverity.ERROR:
                    return Brushes.Red;
                case EventSeverity.WARNING:
                    return Brushes.DarkGoldenrod;
                default:
                    return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
