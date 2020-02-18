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
        private ExternalSimView vw;
        public ObservableCollection<ErrorObject> ErrorList {
            get { return ErrorObjectList.ErrorList; }
        }

        public ErrorDataViewModel(ExternalSimView vw)
        {
            this.vw = vw;
            ErrorObjectList.fillErrorList();
        }
    }
}