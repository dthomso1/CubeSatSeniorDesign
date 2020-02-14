using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public class ErrorObject : ModelBase
    {
        private int _id;
        public int id 
        {
            get { return _id; }
            set{
                _id = value;
                NotifyPropertyChanged("id");
            }
        }

        private bool _isSelected;
        public bool isSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged("isSelected");
                }
            }
        }

        private string _ModuleAffected;
        public string ModuleAffected
        {
            get { return _ModuleAffected; }
            set
            {
                if (!value.Equals(_ModuleAffected))
                {
                    _ModuleAffected = value;
                    NotifyPropertyChanged("ModuleAffected");
                }
            }
        }

        public ErrorObject(int ID, bool IsSelected, string module_affected)
        {
            id = ID;
            isSelected = IsSelected;
            ModuleAffected = module_affected;
        }
    }
}