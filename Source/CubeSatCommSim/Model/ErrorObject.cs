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
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged("isSelected");
                }
            }
        }
        

        private bool _isFatal;
        public bool isFatal
        {
            get { return _isFatal; }
            set
            {
                if (_isFatal != value)
                {
                    _isFatal = value;
                    NotifyPropertyChanged("isFatal");
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

        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                if (!value.Equals(description))
                {
                    _description = value;
                    NotifyPropertyChanged("description");
                }
            }
        }

        public ErrorObject()
        {

        }
        
        /*public ErrorObject(int ID, bool IsFatal)
        {
            id = ID;
            isFatal = isFatal;
        }*/
    }
}