using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CubeSatCommSim.Model;
using CubeSatCommSim.View;
using System.Windows.Input;

namespace CubeSatCommSim.ViewModel
{
    public class ErrorDataViewModel : ViewModelBase
    {
        private ErrorSelectionView vw;
        public ObservableCollection<ErrorObject> ErrorList {
            get { return ErrorObjectList.ErrorList; }
        }

        public ErrorDataViewModel(ErrorSelectionView vw)
        {
            this.vw = vw;
            vw.DataContext = ErrorList;
        }
    }
}