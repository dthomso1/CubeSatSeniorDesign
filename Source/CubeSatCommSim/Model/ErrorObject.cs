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

        private string _Behaviour;
        public string Behaviour
        {
            get { return _Behaviour; }
            set
            {
                if(!value.Equals(_Behaviour))
                {
                    _Behaviour = value;
                    NotifyPropertyChanged("Behaviour");
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