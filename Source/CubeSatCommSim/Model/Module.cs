using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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
                _Address = value;
                NotifyPropertyChanged("Address");
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

        public Module(string name, byte address)
        {
            Name = name;
            Address = address;
            BusConnections = new ObservableCollection<Bus>();
        }

        public void ConnectCSP(CSPBus newBus)
        {
            BusConnections.Add(newBus);
            newBus.ConnectModule(this);
            //Log new connection
            EventLog.AddLog(new SimEvent(
                "Module " + Name + " has connected to bus " + newBus.Name, 
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

            if (BusConnections.Contains(bus))
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
    }
}
