using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CubeSatCommSim.Model
{
    public class InternalSimController : ModelBase
    {
        public ObservableCollection<Module> Modules { get; }
        public ObservableCollection<Bus> Buses { get; }

        //For testing
        private CSPBus CSPBus1, CSPBus2, CSPBus3, CSPBus4;
        private Module Module1, Module2, Module3, Module4;

        public InternalSimController()
        {
            Modules = new ObservableCollection<Module>();
            Buses = new ObservableCollection<Bus>();
            
            //TEMP CODE FOR TESTING
            CSPBus1 = new CSPBus("CSPBus1");
            CSPBus2 = new CSPBus("CSPBus2");
            CSPBus3 = new CSPBus("CSPBus3");
            CSPBus4 = new CSPBus("CSPBus4");
            Module1 = new Module("Module1", 0);
            Module2 = new Module("Module2", 1);
            Module3 = new Module("Module3", 2);
            Module4 = new Module("Module4", 3);
            Buses.Add(CSPBus1);
            Buses.Add(CSPBus2);
            Buses.Add(CSPBus3);
            Buses.Add(CSPBus4);
            Modules.Add(Module1);
            Modules.Add(Module2);
            Modules.Add(Module3);
            Modules.Add(Module4);
        }


        public void RemoveModule(Module m)
        {
            Modules.Remove(m);
            foreach (Bus b in m.BusConnections)
            {
                b.ConnectedModules.Remove(m);
            }
        }

        public void RemoveBus(Bus b)
        {
            Buses.Remove(b);
            foreach (Module m in b.ConnectedModules)
            {
                m.BusConnections.Remove(b);
            }
        }

        //Looks through the error list and applies the selected errors to the matching modules
        public void RegisterErrors()
        {
            foreach(ErrorObject err in ErrorObjectList.ErrorList)
            {
                if (err.isSelected)
                {
                    foreach(Module m in Modules)
                    {
                        if (err.ModuleAffected.Equals(m.Name))
                        {
                            m.RegisteredErrors.Add(err);
                        }
                    }
                }
            }
        }

        //Tells each module which errors are being applied to them
        public void UnregisterErrors()
        {
            foreach (Module m in Modules)
            {
                m.RegisteredErrors.Clear();
            }

        }

        //Runs the chosen simulation script
        public void RunSim()
        {
            EventLog.AddLog(new SimEvent("Starting simulation...", EventSeverity.IMPORTANT));

            //Register the errors with the modules
            RegisterErrors();

            int step = 0;
            while (step < 100)
            {
                //Modules step
                foreach (Module m in Modules)
                {
                    //m.Process(step);
                }

                //Example simulation event sequence
                switch (step)
                {
                    /*case 1:
                        Module1.ConnectBus(CSPBus1);
                        break;*/
                    case 2:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
                        break;
                    /*case 4:
                        Module2.ConnectBus(CSPBus1);
                        break;*/
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

                //Buses step
                foreach(Bus b in Buses)
                {
                   b.Process(step);
                }

                //Thread.Sleep(1);

                step++;
            }

            //Unregister the errors so that they will be re-registered next time the sim runs
            UnregisterErrors();

            EventLog.AddLog(new SimEvent("Simulation complete", EventSeverity.IMPORTANT));
        }
    }
}
