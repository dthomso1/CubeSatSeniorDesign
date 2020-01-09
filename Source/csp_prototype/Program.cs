using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csp_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            CSPBus CSPBus1 = new CSPBus("CSPBus1");
            Module Module1 = new Module("Module1", 0);
            Module Module2 = new Module("Module2", 1);

            //Temporary loop of 10 steps
            for(int step = 1; step < 11; step++){
                Console.WriteLine("Step: " + step);
                //Example simulation event sequence
                switch (step) {
                    case 1:
                        Module1.ConnectCSP(CSPBus1);
                        break;
                    case 2:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0);
                        break;
                    case 4:
                        Module2.ConnectCSP(CSPBus1);
                        break;
                    case 6:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0);
                        break;
                    case 7:                        
                        Module1.SendCSPPacket(CSPBus1, 2, 0, 0, 0);
                        break;
                   default:
                        break;
                }

                CSPBus1.Process(step);
            }

            //Read any input before closing so we can actually read the output
            Console.ReadLine();            
        }
    }
    
    public abstract class Bus
    {
        public string name;
        public List<Module> connected_modules;
        
        public abstract void Process(int step);

        public void ConnectModule(Module new_module){
            connected_modules.Add(new_module);
        }
    }

    public class CSPBus : Bus
    {
        public CSPPacket current_packet;
        public Queue<CSPPacket> packet_queue;

        public CSPBus(string name){
            this.name = name;
            current_packet = null;
            packet_queue = new Queue<CSPPacket>();
            connected_modules = new List<Module>();
        }
        
        public void EnqueuePacket(CSPPacket pkt){
            packet_queue.Enqueue(pkt);
        }
        
        public override void Process(int step){
            //Transmit next packet if possible
            if(current_packet == null && packet_queue.Count > 0){
                current_packet = packet_queue.Dequeue();
                //Log new packet on bus
                Console.WriteLine("New packet transmitting on bus " + name + ": " + current_packet.ToString());
            }

            if(current_packet != null){
                bool packet_received = false;
                foreach(Module module in connected_modules){
                    if(module.address == current_packet.Header[CSPPacket.dest]){
                        module.ReceiveCSPPacket(current_packet);   
                        packet_received = true;
                    }
                }

                if(!packet_received){
                    //log packet going nowhere
                    Console.WriteLine("Packet was dropped because it has no valid destination: " + current_packet.ToString());
                }

                //Remove packet after transmission
                current_packet = null;
                //Log packet off the bus?
            }
        }
    }

    public class Module
    {
        public string name;
        public byte address;
        public List<Bus> BusConnections;

        public Module(string name, byte address){
            this.name = name;
            this.address = address;
            BusConnections = new List<Bus>();
        }

        public void ConnectCSP(CSPBus new_bus){
            BusConnections.Add(new_bus);
            new_bus.ConnectModule(this);
            //Log new connection
            Console.WriteLine("Module " + name + " has connected to bus " + new_bus.name);
        }

        public void SendCSPPacket(CSPBus bus, byte destination_addr, byte destination_port, byte source_port, byte priority){
            CSPPacket packet = new CSPPacket(0x00000000);
            packet.Header[CSPPacket.src] = address;
            packet.Header[CSPPacket.dest] = destination_addr;
            packet.Header[CSPPacket.src_port] = source_port;
            packet.Header[CSPPacket.dest_port] = destination_port;
            packet.Header[CSPPacket.priority] = priority;

            if(BusConnections.Contains(bus)){
                bus.EnqueuePacket(packet);
                //Log sending packet
                Console.WriteLine("Module " + name + " sends packet " + packet.ToString() + " to bus " + bus.name);
            }
            else{
                //Log failed send
                Console.WriteLine("Module " + name + " failed to send packet " + packet.ToString() + " because it is not connected to bus " + bus.name);
            }
        }
        
        public void ReceiveCSPPacket(CSPPacket packet){
            //Log received packet
            Console.WriteLine("Module " + name + " received packet: " + packet.ToString());
            //Processing?
        }
    }

    public class CSPPacket
    {
        public BitVector32 Header;

        public static BitVector32.Section crc = BitVector32.CreateSection(1);             //0 = CRC32 Checksum
        public static BitVector32.Section rdp = BitVector32.CreateSection(1, crc);              //1 = RDP Header
        public static BitVector32.Section xtea = BitVector32.CreateSection(1, rdp);             //2 = XTEA Encryption
        public static BitVector32.Section hmac = BitVector32.CreateSection(1, xtea);            //3 = HMAC
        public static BitVector32.Section reserved = BitVector32.CreateSection(15, hmac);       //4-7 = Reserved
        public static BitVector32.Section src_port = BitVector32.CreateSection(63, reserved);   //8-13 = Source Port
        public static BitVector32.Section dest_port = BitVector32.CreateSection(63, src_port);  //14-19 = Destination Port
        public static BitVector32.Section dest = BitVector32.CreateSection(31, dest_port);      //20-24 = Destination
        public static BitVector32.Section src = BitVector32.CreateSection(31, dest);            //29-25 = Source
        public static BitVector32.Section priority = BitVector32.CreateSection(3, src);         //31-30 = Priority
        
        public byte[] Data;
        
        public CSPPacket(int header_values, byte[] content = null){
            Data = content;
            Header = new BitVector32(header_values);
        }

        public string ToString(){
            return "Header={" + Header[priority]
                        + " " + Header[src]
                        + " " + Header[dest]
                        + " " + Header[dest_port]
                        + " " + Header[src_port]
                        + " " + Header[reserved]
                        + " " + Header[hmac]
                        + " " + Header[xtea]
                        + " " + Header[rdp]
                        + " " + Header[crc]
                        + "}" + Header.ToString();
        }
    }
    
}
