using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSatCommSim.Model
{
    public class InternalSimController : ModelBase
    {
        public ObservableCollection<Module> Modules { get; }
        public ObservableCollection<Bus> Buses { get; }

        //For testing
        private CSPBus CSPBus1;
        private Module Module1, Module2;

        public InternalSimController()
        {
            Modules = new ObservableCollection<Module>();
            Buses = new ObservableCollection<Bus>();
            
            //TEMP CODE FOR TESTING
            CSPBus1 = new CSPBus("CSPBus1");
            Module1 = new Module("Module1", 0);
            Module2 = new Module("Module2", 1);

            Buses.Add(CSPBus1);
            Modules.Add(Module1);
            Modules.Add(Module2);
        }

        public void TestSim()
        {
            //Temporary loop of 10 steps
            for (int step = 1; step < 16; step++)
            {
                //Example simulation event sequence
                switch (step)
                {
                    case 1:
                        Module1.ConnectCSP(CSPBus1);
                        break;
                    case 2:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
                        break;
                    case 4:
                        Module2.ConnectCSP(CSPBus1);
                        break;
                    case 6:
                        //Module 2 has priority in next step
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 1, 3);
                        break;
                    case 7:
                        //This one should interrupt the last one when its 1/3 transmitted
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
                        break;
                    case 11:
                        //Module 1 has priority
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 1, 1);
                        break;
                    case 14:
                        Module1.SendCSPPacket(CSPBus1, 2, 0, 0, 0, 1);
                        break;
                    default:
                        break;
                }

                //Modules step

                //Buses step
                CSPBus1.Process(step);
            }
            //END OF TEST CODE
        }
    }
}
