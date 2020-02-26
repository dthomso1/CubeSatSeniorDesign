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

        private bool _IsFatal;
        public bool IsFatal
        {
            get { return _IsFatal; }
            set
            {
                if(_IsFatal != value)
                {
                    _IsFatal = value;
                    NotifyPropertyChanged("IsFatal");
                }
            }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    NotifyPropertyChanged("IsActive");
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

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (!value.Equals(Description))
                {
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public ErrorObject() { }
    }
}