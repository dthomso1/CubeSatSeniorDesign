using System.Collections.Specialized;
using System;

namespace CubeSatCommSim.Model
{
    public class CSPPacket : ModelBase, IComparable
    {
        public static BitVector32.Section CRC = BitVector32.CreateSection(1);                                   //0 = CRC32 Checksum
        public static BitVector32.Section RDP = BitVector32.CreateSection(1, CRC);                              //1 = RDP Header
        public static BitVector32.Section XTEA = BitVector32.CreateSection(1, RDP);                             //2 = XTEA Encryption
        public static BitVector32.Section HMAC = BitVector32.CreateSection(1, XTEA);                            //3 = HMAC
        public static BitVector32.Section Reserved = BitVector32.CreateSection(15, HMAC);                       //4-7 = Reserved
        public static BitVector32.Section SourcePort = BitVector32.CreateSection(63, Reserved);                 //8-13 = Source Port
        public static BitVector32.Section DestinationPort = BitVector32.CreateSection(63, SourcePort);          //14-19 = Destination Port
        public static BitVector32.Section DestinationAddress = BitVector32.CreateSection(31, DestinationPort);  //20-24 = Destination
        public static BitVector32.Section SourceAddress = BitVector32.CreateSection(31, DestinationAddress);    //29-25 = Source
        public static BitVector32.Section Priority = BitVector32.CreateSection(3, SourceAddress);               //31-30 = Priority

        private BitVector32 _Header;
        public BitVector32 Header
        {
            get { return _Header; }
            private set
            {
                _Header = value;
                NotifyPropertyChanged("Header");
            }
        }

        private short _DataSize;
        public short DataSize
        {
            get { return _DataSize; }
            private set
            {
                _DataSize = value;
                NotifyPropertyChanged("DataSize");
            }
        }

        private short _PartTransmitted;
        public short PartTransmitted
        {
            get { return _PartTransmitted; }
            set
            {
                _PartTransmitted = value;
                NotifyPropertyChanged("PartTransmitted");
            }
        }

        public CSPPacket(int headerValues, short dataSize)
        {
            PartTransmitted = 0;
            DataSize = dataSize;
            Header = new BitVector32(headerValues);
        }

        public CSPPacket(BitVector32 header, short dataSize)
        {
            PartTransmitted = 0;
            DataSize = dataSize;
            Header = header;
        }

        public override string ToString()
        {
            return "Header={" + Header[Priority]
                        + " " + Header[SourceAddress]
                        + " " + Header[DestinationAddress]
                        + " " + Header[DestinationPort]
                        + " " + Header[SourcePort]
                        + " " + Header[Reserved]
                        + " " + Header[HMAC]
                        + " " + Header[XTEA]
                        + " " + Header[RDP]
                        + " " + Header[CRC]
                        + "},Tx/Size=" + PartTransmitted + "/" + DataSize; 
        }

        int IComparable.CompareTo(object obj)
        {
            return this.Header[Priority].CompareTo(((CSPPacket)obj).Header[Priority]);
        }
    }
}
