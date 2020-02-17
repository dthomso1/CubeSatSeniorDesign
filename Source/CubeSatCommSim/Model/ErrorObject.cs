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

        private bool _isFatal;
        public bool isFatal
        {
            get { return _isFatal; }
            set
            {
                if(_isFatal != value)
                {
                    _isFatal = value;
                    NotifyPropertyChanged("isFatal");
                }
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
        
        /*public ErrorObject(int ID, bool IsFatal)
        {
            id = ID;
            isFatal = isFatal;
        }*/
    }
}