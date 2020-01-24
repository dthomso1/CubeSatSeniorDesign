using System;
using System.Collections.Generic;

namespace CubeSatCommSim.Model
{
    public class CSPBus : Bus
    {
        private CSPPacket _CurrentPacket;
        public CSPPacket CurrentPacket
        {
            get { return _CurrentPacket; }
            private set
            {
                _CurrentPacket = value;
                NotifyPropertyChanged("CurrentPacket");
            }
        }

        //Data rate is bits/step, NOT bits/second -- may need conversion during input?
        private int _DataRate;
        public int DataRate
        {
            get { return _DataRate; }
            set
            {
                _DataRate = value;
                NotifyPropertyChanged("DataRate");
            }
        }

        private PriorityQueue<CSPPacket> PacketQueue;

        public CSPBus(string name, int dataRate = 1) : base(name)
        {
            CurrentPacket = null;
            DataRate = dataRate;
            PacketQueue = new PriorityQueue<CSPPacket>();
        }

        public void EnqueuePacket(CSPPacket pkt)
        {
            PacketQueue.Enqueue(pkt);
        }

        public override void Process(int step)
        {
            //Transmit next packet if possible
            if (CurrentPacket == null && PacketQueue.Count > 0)
            {
                CurrentPacket = PacketQueue.Dequeue();
                //Log new packet on bus
                EventLog.AddLog(new SimEvent(
                    "New packet transmitting on bus " + Name + ": " + CurrentPacket.ToString(),
                    EventSeverity.INFO));
            }

            if (CurrentPacket != null)
            {
                bool destination_exists = false;
                foreach (Module module in ConnectedModules)
                {
                    if (module.Address == CurrentPacket.Header[CSPPacket.DestinationAddress])
                    {
                        destination_exists = true;
                        //Transmit part of the packet based on data rate
                        CurrentPacket.PartTransmitted = (short)Math.Min(CurrentPacket.PartTransmitted + DataRate, CurrentPacket.DataSize);
                        if(CurrentPacket.PartTransmitted >= CurrentPacket.DataSize)
                        {
                            //If packet is fully transmitted, receive it
                            module.ReceiveCSPPacket(CurrentPacket);
                            //Remove packet after transmission
                            CurrentPacket = null;
                            break;
                        }
                    }
                }

                if (!destination_exists)
                {
                    //log packet going nowhere
                    EventLog.AddLog(new SimEvent(
                        "Packet was dropped because it has no valid destination: " + CurrentPacket.ToString(),
                        EventSeverity.WARNING));
                    //Take packet off the bus, since it will just go nowhere
                    CurrentPacket = null;
                }
            }
        }
    }
}
