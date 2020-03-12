﻿using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace CubeSatCommSim.Model
{
    public class Module : ModelBase
    {
        //public static List<int> UsedPriorities = new List<int>();
        public static List<int> UsedAddresses = new List<int>();

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
                if(value != _Address && (value >= 0 && value < 32))
                {
                    if (!UsedAddresses.Contains(value))
                    {
                        if (UsedAddresses.Contains(_Address))
                        {
                            UsedAddresses.Remove(_Address);
                        }
                        UsedAddresses.Add(value);
                        _Address = value;
                        NotifyPropertyChanged("Address");
                    }
                }
            }
        }

        private int _Priority;
        public int Priority
        {
            get { return _Priority; }
            set
            {
                if(value != _Priority && (value >= 0 && value < 4))
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

        private bool randomPriority, randomDestination, randomSource, randomDestinationPort, randomSourcePort;

        public Module(string name, int address)
        {
            randomPriority = false;
            randomDestination = false;
            randomSource = false;
            randomDestinationPort = false;
            randomSourcePort = false;
            Idle = true;
            Crashed = false;
            Name = name;
            Address = address;
            BusConnections = new ObservableCollection<Bus>();
            RegisteredErrors = new ObservableCollection<ErrorObject>();
        }

        public void Process(int step)
        {
            if (Crashed) return; //Module cannot do anything if it has crashed

            //Check for fatal errors
            foreach(ErrorObject err in RegisteredErrors)
            {
                if (err.Behaviour.ToUpper().Equals("FATAL"))
                {
                    if (err.IsActive)
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
                else if (err.Behaviour.ToUpper().Equals("RANDOMIZE PRIORITY"))
                {
                    randomPriority = err.IsActive;
                }
                else if (err.Behaviour.ToUpper().Equals("RANDOMIZE DESTINATION ADDRESS"))
                {
                    randomDestination = err.IsActive;
                }
                else if (err.Behaviour.ToUpper().Equals("RANDOMIZE SOURCE ADDRESS"))
                {
                    randomSource = err.IsActive;
                }
                else if (err.Behaviour.ToUpper().Equals("RANDOMIZE DESTINATION PORT"))
                {
                    randomDestinationPort = err.IsActive;
                }
                else if (err.Behaviour.ToUpper().Equals("RANDOMIZE SOURCE PORT"))
                {
                    randomSourcePort = err.IsActive;
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
            var rnd = new Random();
            BitVector32 packetHeader = new BitVector32(0x00000000);
            packetHeader[CSPPacket.SourceAddress] = randomSource ? ((byte)(rnd.Next(0, 32))) : Address;
            packetHeader[CSPPacket.DestinationAddress] = randomDestination ? ((byte)(rnd.Next(0, 32))) : destination_addr;
            packetHeader[CSPPacket.SourcePort] = randomSourcePort ? ((byte)(rnd.Next(0, 64))) : source_port;
            packetHeader[CSPPacket.DestinationPort] = randomDestinationPort ? ((byte)(rnd.Next(0, 64))) : destination_port;
            packetHeader[CSPPacket.Priority] = randomPriority ? ((byte)(rnd.Next(0,4))) : priority;
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
                //We are assuming that all packets with randomized bits are considered in error because the
                //probablity that the error is not detected is negligible
                if (randomPriority)
                {
                    packet.ErrorDetected = true;
                    //Log sending packet with random priority
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends a packet containing a random priority: " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.WARNING));
                }
                if (randomDestination)
                {
                    packet.ErrorDetected = true;
                    //Log sending packet with random destination
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends a packet containing a random destination address: " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.WARNING));
                }
                if (randomSource)
                {
                    packet.ErrorDetected = true;
                    //Log sending packet with random psource
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends a packet containing a random source address: " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.WARNING));
                }
                if (randomDestinationPort)
                {
                    packet.ErrorDetected = true;
                    //Log sending packet with random psource
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends a packet containing a random destination port: " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.WARNING));
                }
                if (randomSourcePort)
                {
                    packet.ErrorDetected = true;
                    //Log sending packet with random psource
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends a packet containing a random source port: " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.WARNING));
                }

                bus.EnqueuePacket(packet);
                if(!(randomPriority || randomDestination || randomSource || randomDestinationPort || randomSourcePort))
                {
                    //Log sending normal packet
                    EventLog.AddLog(new SimEvent(
                        "Module " + Name + " sends packet " + packet.ToString() + " to bus " + bus.Name,
                        EventSeverity.INFO));
                }
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
            if (packet.ErrorDetected)
            {
                //Log received packet
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " detected an error in received packet: " + packet.ToString() + ", the packet was discarded",
                    EventSeverity.ERROR));
            }
            else
            {
                //Log received packet
                EventLog.AddLog(new SimEvent(
                    "Module " + Name + " received packet: " + packet.ToString(),
                    EventSeverity.INFO));
                //Processing?
            }
        }

        public void Reset()
        {
            Crashed = false;
            randomDestination = false;
            randomPriority = false;
            randomSource = false;
            randomDestinationPort = false;
            randomSourcePort = false;
        }
    }
}
