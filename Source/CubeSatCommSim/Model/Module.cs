using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace CubeSatCommSim.Model
{
    public class Module : ModelBase
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private int _Address;
        public int Address
        {
            get { return _Address; }
            set
            {              
                if(value >= 0 && value < 32)
                {
                    _Address = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        private int _Priority;
        public int Priority
        {
            get { return _Priority; }
            set
            {
                if(value >= 0 && value < 4)
                {
                    _Priority = value;
                    NotifyPropertyChanged("Priority");
                }
            }
        }
        
        private bool _Idle;
        public bool Idle
        {
            get { return _Idle; }
            set
            {
                _Idle = value;
                NotifyPropertyChanged("Idle");
            }
        }

        private ObservableCollection<Bus> _BusConnections;
        public ObservableCollection<Bus> BusConnections
        {
            get { return _BusConnections; }
            private set
            {
                _BusConnections = value;
                NotifyPropertyChanged("BusConnections");
            }
        }

        private ObservableCollection<ErrorObject> _RegisteredErrors;
        public ObservableCollection<ErrorObject> RegisteredErrors
        {
            get { return _RegisteredErrors; }
            private set
            {
                _RegisteredErrors = value;
                NotifyPropertyChanged("RegisteredErrors");
            }
        }

        private bool _Crashed;
        public bool Crashed
        {
            get { return _Crashed; }
            private set
            {
                if(_Crashed != value)
                {
                    _Crashed = value;
                    NotifyPropertyChanged("Crashed");
                }
            }
        }

        public Module(string name, int address)
        {
            Idle = true;
            Crashed = false;
            Name = name;
            Address = address;
            BusConnections = new ObservableCollection<Bus>();
            RegisteredErrors = new ObservableCollection<ErrorObject>();
        }
        public Module()
        {
        //blank constructor for InternalSimController to allow XDocument to function
        }

        public void Process(int step)
        {
            if (Crashed) return; //Module cannot do anything if it has crashed

            //Check for fatal errors
            foreach(ErrorObject err in RegisteredErrors)
            {
                if (err.IsActive)
                {
                    if (err.IsFatal)
                    {
                        Crashed = true;
                        EventLog.AddLog(new SimEvent(
                                                "Module " + Name +
                                                " has ceased operation due to a fatal error: "
                                                + err.Description,
                                                EventSeverity.FATAL_ERROR
                                            )
                                        );
                        return;
                    }
                }
            }

            //Modules are not really doing any processing in this version of the program, so Idle is always true
            Idle = true;
        }

        public void ConnectBus(Bus newBus)
        {
            if (!BusConnections.Contains(newBus))
            {
                BusConnections.Add(newBus);
                newBus.ConnectModule(this);
                //Log new connection
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " has connected to bus " + newBus.Name, 
                    EventSeverity.INFO));
            }
        }

        public void DisconnectBus(Bus bus)
        {
            if (BusConnections.Contains(bus))
            {
                BusConnections.Remove(bus);
            }
            bus.DisconnectModule(this);
            //Log new connection
            EventLog.AddLog(new SimEvent(
                "Module " + Name + " has disconnected from bus " + bus.Name,
                EventSeverity.INFO));
        }

        public void SendCSPPacket(CSPBus bus, byte destination_addr, byte destination_port, byte source_port, byte priority, short dataSize)
        {
            BitVector32 packetHeader = new BitVector32(0x00000000);
            packetHeader[CSPPacket.SourceAddress] = Address;
            packetHeader[CSPPacket.DestinationAddress] = destination_addr;
            packetHeader[CSPPacket.SourcePort] = source_port;
            packetHeader[CSPPacket.DestinationPort] = destination_port;
            packetHeader[CSPPacket.Priority] = priority;
            CSPPacket packet = new CSPPacket(packetHeader, dataSize);
            
            if(bus == null)
            {
                //Log failed send
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " failed to send a packet because the target bus does not exist (check that the bus in your script exists in the simulation)",
                    EventSeverity.ERROR));
            }
            else if (BusConnections.Contains(bus))
            {
                bus.EnqueuePacket(packet);
                //Log sending packet
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " sends packet " + packet.ToString() + " to bus " + bus.Name,
                    EventSeverity.INFO));
            }
            else
            {
                //Log failed send
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " failed to send packet " + packet.ToString() + " because it is not connected to bus " + bus.Name,
                    EventSeverity.ERROR));
            }
        }

        public void ReceiveCSPPacket(CSPPacket packet)
        {
            //Log received packet
            EventLog.AddLog(new SimEvent(
                "Module " + Name + " received packet: " + packet.ToString(),
                EventSeverity.INFO));
            //Processing?
        }

        public void Reset()
        {
            Crashed = false;
        }
    }
}
