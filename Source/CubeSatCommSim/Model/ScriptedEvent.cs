using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSatCommSim.Model
{
    public enum ModuleCommand
    {
        SEND,
        PING
    }

    public class ScriptedEvent
    {
        private int _Time;
        public int Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
            }
        }

        private string _Module;
        public string Module
        {
            get { return _Module; }
            set
            {
                _Module = value;
            }
        }

        private ModuleCommand _Command;
        public ModuleCommand Command
        {
            get { return _Command; }
            set
            {
                _Command = value;
            }
        }

        //Paramaters:
        //[0] -> target module
        //[1] -> target bus
        //others -> anything else
        private List<string> _Parameters;
        public List<string> Parameters
        {
            get { return _Parameters; }
            private set
            {
                _Parameters = value;

            }
        }

        public ScriptedEvent()
        {
            Parameters = new List<string>();
        }
    }
}
