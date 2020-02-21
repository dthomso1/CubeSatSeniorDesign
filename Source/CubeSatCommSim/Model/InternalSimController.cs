using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows;
using Microsoft.Win32;

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
            Module1 = new Module("SASI", 0);
            Module2 = new Module("EPS", 1);
            Buses.Add(CSPBus1);
            Modules.Add(Module1);
            Modules.Add(Module2);
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
                if (err.IsActive)
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

        //Reads a script file to determine the sequence of events that will take place in the simulation
        private List<ScriptedEvent> LoadScript()
        {
            var scriptedEventList = new List<ScriptedEvent>();
            var dlg = new OpenFileDialog();
            dlg.Title = "Open Simulation Script";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV files (.csv)|*.csv";
            dlg.CheckFileExists = true;
            if(dlg.ShowDialog() == true)
            {
                //Open script file
                if (File.Exists(dlg.FileName))
                {
                    using(FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            //Read each line as an event in the simulation
                            while (!sr.EndOfStream)
                            {
                                try
                                {
                                    var line = sr.ReadLine();
                                    var values = line.Split(',');
                                    var scriptEvent = new ScriptedEvent();

                                    scriptEvent.Time = int.Parse(values[0]);
                                    scriptEvent.Module = values[1];
                                    scriptEvent.Command = (ModuleCommand)(Enum.Parse(typeof(ModuleCommand), values[2].ToUpper()));
                                    string[] cmdParams = new string[values.Length - 3];
                                    for (int i = 3; i < values.Length; i++)
                                    {
                                        scriptEvent.Parameters.Add(values[i]);
                                    }

                                    scriptedEventList.Add(scriptEvent);
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(
                                        "An error occured while trying to read the script file: " + ex.Message,
                                        "Error Reading File",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                    );
                                }
                            }
                        }
                    }                    
                }
            }
            return scriptedEventList;
        }

        //Runs the chosen simulation script
        public void RunSim()
        {
            var eventList = LoadScript();

            EventLog.AddLog(new SimEvent("Starting simulation...", EventSeverity.IMPORTANT));

            //Register the errors with the modules
            RegisterErrors();

            int step = 0;
            while (step < 100)
            {
                //Modules step
                foreach (Module m in Modules)
                {
                    m.Process(step);
                }

                //Example simulation event sequence
                switch (step)
                {
                    case 2:
                        Module1.SendCSPPacket(CSPBus1, 1, 0, 0, 0, 1);
                        Module2.SendCSPPacket(CSPBus1, 0, 0, 0, 0, 1);
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
                }//*/

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
