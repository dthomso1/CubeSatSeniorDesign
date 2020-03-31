using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CubeSatCommSim.Model
{
    public enum ErrorBehaviour
    {
        FATAL = 0,
        GARBAGE_DATA = 1, //NYI
        LOSE_POWER = 2, //NYI
        LOSE_COMMUNICATION = 3, //NYI
        RETRY_TASK = 4,
        unknown = 5,
        RANDOM_PRIORITY = 6,
        RANDOM_DESTINATION_ADDRESS = 7,
        RANDOM_SOURCE_ADDRESS = 8,
        RANDOM_DESTINATION_PORT = 9,
        RANDOM_SOURCE_PORT = 10
    }

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

        private ErrorBehaviour _Behaviour;
        public ErrorBehaviour Behaviour
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