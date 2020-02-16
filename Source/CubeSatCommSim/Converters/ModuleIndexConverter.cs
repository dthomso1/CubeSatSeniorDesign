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
    class ModuleIndexConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var module = (Module)values[0];
            var simVM = (InternalSimVM)values[1];
            var moduleIndex = ((ObservableCollection<Module>)(simVM.Modules.SourceCollection)).IndexOf(module);
            return moduleIndex;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
