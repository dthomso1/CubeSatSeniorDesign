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
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace CubeSatCommSim.Model
{
    public class InternalSimController : ModelBase
    {
        public ObservableCollection<Module> Modules { get; }
        public ObservableCollection<Bus> Buses { get; }

        private bool _LoopSimulation;
        public bool LoopSimulation
        {
            get { return _LoopSimulation; }
            set
            {
                _LoopSimulation = value;
                NotifyPropertyChanged("LoopSimulation");
            }
        }

        private bool _SimulationRunning;
        public bool SimulationRunning
        {
            get { return _SimulationRunning; }
            set
            {
                _SimulationRunning = value;
                NotifyPropertyChanged("SimulationRunning");
            }
        }

        private List<ScriptedEvent> eventList; //List as used by the program (retry events may be added)
        private bool abort;
        private bool scriptWarningShown;
       
        //private String busConnectionString;
        //private String connectedModulesString;
        //private String[] modulesConnectedList;

        //For testing
        //private CSPBus CSPBus1;
        //private Module Module1, Module2;

        public InternalSimController()
        {
            eventList = new List<ScriptedEvent>();
            abort = false;
            Modules = new ObservableCollection<Module>();
            Buses = new ObservableCollection<Bus>();
            LoopSimulation = true;
            //TEMP CODE FOR TESTING
            //CSPBus1 = new CSPBus("CANBUS", 100);
            //Module1 = new Module("OBC", 0);
            //Module2 = new Module("SASI", 1);
            //Buses.Add(CSPBus1);
            //Modules.Add(Module1);
            //Modules.Add(Module2);
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
        //David: allows module and bus lists to be cleared before loading module and bus configuration
        public void clearModuleBusToLoad()
        {
            Modules.Clear();
            Buses.Clear();
        }

        //Looks through the error list and applies the selected errors to the matching modules
        public void RegisterErrors()
        {
            foreach(ErrorObject err in ErrorObjectList.ErrorList)
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

        //Tells each module which errors are being applied to them
        public void UnregisterErrors()
        {
            foreach (Module m in Modules)
            {
                m.RegisteredErrors.Clear();
            }
        }

        //Resets all the modules
        public void ResetCrashedModules()
        {
            foreach (Module m in Modules)
            {
                m.Reset();
            }
        }

        //Reads a script file to determine the sequence of events that will take place in the simulation, returns the latest time of any event
        private int LoadScript()
        {
            int latestEventTime = 0;

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
                            eventList.Clear();
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

                                    //Update latest event time and add new event
                                    latestEventTime = scriptEvent.Time > latestEventTime ? scriptEvent.Time : latestEventTime;
                                    eventList.Add(scriptEvent);
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
            return latestEventTime;
        }

        //Returns true if all buses are idling, false if at least one is busy
        public bool AllBusesIdle()
        {
            foreach(Bus b in Buses)
            {
                if (!b.Idle)
                {
                    return false;
                }
            }
            return true;
        }

        //Returns true if all modules are idling, false if at least one is busy
        public bool AllModulesIdle()
        {
            foreach (Module m in Modules)
            {
                if (!m.Crashed && !m.Idle)
                {
                    return false;
                }
            }
            return true;
        }

        //Runs the chosen simulation script
        public async void RunSim()
        {
            if (SimulationRunning) return;
            abort = false;
            scriptWarningShown = false;

            var latestEventTime = LoadScript();
            if (latestEventTime <= 0) return;
            
            SimulationRunning = true;

            EventLog.AddLog(new SimEvent("Starting simulation...", EventSeverity.IMPORTANT));

            //Register the errors with the modules
            RegisterErrors();

            int step = 0;
            //Continue the simulation until it is aborted, or until all modules and buses are idle after the last event occurs if not looping
            while (!abort && (LoopSimulation || ((!(AllBusesIdle() && AllModulesIdle()) || step <= latestEventTime))))
            {
                var retryList = new List<ScriptedEvent>();
                var stepCheck = LoopSimulation ? step % (latestEventTime + 1) : step;
                //Run each scripted event scheduled for the current time step
                foreach (ScriptedEvent ev in eventList.Where(ev => ev.Time == stepCheck))
                {
                    Module source = Modules.Where(m => m.Name.Equals(ev.Module)).FirstOrDefault();

                    if (ExecuteScriptedEvent(ev))
                    {
                        break;
                    }

                    //If module needs to retry, trigger same action on next time step
                    if (source.RetryAction)
                    {
                        EventLog.AddLog(
                            new SimEvent(
                                "Module " + source.Name + " is retrying its last action",
                                EventSeverity.WARNING
                            )
                        );
                        ev.Time++;
                    }
                }
                

                if (!abort)
                {
                    //Modules step
                    foreach (Module m in Modules)
                    {
                        m.Process(step);
                    }

                    //Buses step
                    foreach (Bus b in Buses)
                    {
                        b.Process(step);
                    }

                    await Task.Delay(1000);

                    step++;
                }
            }

            //Unregister the errors so that they will be re-registered next time the sim runs
            UnregisterErrors();
            ResetCrashedModules();

            if (abort)
            {
                EventLog.AddLog(new SimEvent("Simulation aborted", EventSeverity.IMPORTANT));
            }
            else
            {
                EventLog.AddLog(new SimEvent("Simulation complete", EventSeverity.IMPORTANT));
            }
            SimulationRunning = false;
        }

        //Aborts the current simulation
        public void StopSim()
        {
            abort = true;
        }

        public void LoadConfiguration()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Open Configuration File";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";
            dlg.CheckFileExists = true;

            //for testing
            //CSPBus bus1;
            CSPBus CSPBus1 = new CSPBus("CANBUS", 100);

            if(dlg.ShowDialog() == true)
            {
                //Open script file
                if (File.Exists(dlg.FileName))
                {
                    try
                    {    //using the selected filename, adds modules to list for modules
                         XDocument doc = XDocument.Parse(File.ReadAllText(dlg.FileName));
                         IEnumerable<Module> ModuleResult = from c in doc.Descendants("Module")
                                             select new Module()
                                             {
                                                 Name = c.Element("name").Value,
                                                 Address = int.Parse(c.Element("address").Value),
                                                 Priority = int.Parse(c.Element("priority").Value),
                                                 BusConnections = new ObservableCollection<Bus>(),
                                                 //BusConnections.Add(new CSPBus(c.Element("BusConnections").Value)),
                                                 RegisteredErrors = new ObservableCollection<ErrorObject>(),
                                                 Idle = true,
                                                 Crashed = false
                                             };

                         foreach (Module mo in ModuleResult)
                         {
                             //mo.BusConnections.Add(new CSPBus(("CANBUS")));
                             mo.ConnectBus(CSPBus1);
                             Modules.Add(mo);
                         }

                         //using the selected filename, adds bus to list for bus
                         XDocument doc2 = XDocument.Parse(File.ReadAllText(dlg.FileName));
                         IEnumerable<CSPBus> BusResult = from c in doc2.Descendants("Bus")
                                             select new CSPBus()
                                             {
                                                 Name = c.Element("name").Value,
                                                 ConnectedModules = new ObservableCollection<Module>()
                                                 /*{
                                                     from b in doc.Descendants("connectedModules")
                                                     select new Module()
                                                     {
                                                        Name = b.Element("name").Value,
                                                        Address = int.Parse(b.Element("address").Value),
                                                     }
                                                 }*/
                                                 
                                                 //load in ConnectedModules from seperated by space string
                                             };

                         foreach (Bus bo in BusResult)
                         {
                             Buses.Add(bo);
                         }
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
        }//end of LoadConfiguration
        public void SaveConfiguration()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    "); 
            var filepath = Path.Combine(@"..\..\Data\ModuleConfiguration");
            string filePathWithTime = string.Concat(filepath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),".txt");
            XmlWriter writer = XmlWriter.Create(filePathWithTime);
            writer.WriteStartDocument();
            //String busConnectionString;
                
            writer.WriteStartElement("ModulesAndBuses");
            
            foreach(Module m in Modules)
            {
                //busConnectionString = null;
                writer.WriteStartElement("Module");
                writer.WriteElementString("name", m.Name);
                writer.WriteElementString("address", m.Address.ToString());
                writer.WriteElementString("priority", m.Priority.ToString());
                //loop through busConnections and add each one to a string
                foreach(Bus b1 in m.BusConnections)
                {
                    /*if(busConnectionString == null)
                    {
                       busConnectionString = b1.Name;
                    }
                    else
                    {
                        busConnectionString = busConnectionString + "-" + b1.Name;
                    }*/
                    
                    writer.WriteStartElement("connectedBuses");
                    writer.WriteElementString("name", b1.Name);
                    writer.WriteEndElement();
                }
                //writer.WriteElementString("BusConnections", busConnectionString);
                writer.WriteEndElement();
            }
            foreach(Bus b in Buses)
            {
                //connectedModulesString = null;
                writer.WriteStartElement("Bus");
                writer.WriteElementString("name", b.Name);
                foreach(Module m1 in b.ConnectedModules)
                {
                    /*if(connectedModulesString == null)
                    {
                       connectedModulesString = m1.Name + "," + m1.Address;
                    }
                    else
                    {
                        connectedModulesString = connectedModulesString + "-" + m1.Name + "," + m1.Address;
                    }*/

                    writer.WriteStartElement("connectedModules");
                    writer.WriteElementString("name", m1.Name);
                    writer.WriteElementString("address", m1.Address.ToString());
                    writer.WriteEndElement();
                }
                //writer.WriteElementString("connectedModules", connectedModulesString);
                writer.WriteEndElement();
            }
            writer.Flush();
            writer.Close();
        }

        public void SaveLog()
        {
            //File.WriteAllLines(@"C:\Users\David\source\repos\SeniorDesignNewBranch\Source\CubeSatCommSim\Data\SavedLog.txt", 
            string[] c = EventLog.writeLog();
            var filepath = Path.Combine(@"..\..\Data\SavedLog");
            string filePathWithTime = string.Concat(filepath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),".txt");
            File.WriteAllLines(filePathWithTime, c);
        }

        //Executes the event and returns true if the execution should halt
        private bool ExecuteScriptedEvent(ScriptedEvent ev)
        {
            Module source = Modules.Where(m => m.Name.Equals(ev.Module)).FirstOrDefault();

            //Send packet with given data size from source to target over target bus 
            if (ev.Command == ModuleCommand.SEND || ev.Command == ModuleCommand.PING)
            {
                //Parse parameters
                byte dest_address;
                if (!byte.TryParse(ev.Parameters[0], out dest_address))
                {
                    MessageBox.Show("An event in your script has an invalid parameter. This simulation will now abort.", "Parameter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    abort = true;
                    return true;
                }
                Bus targetBus = Buses.Where(b => b.Name.Equals(ev.Parameters[1])).FirstOrDefault();
                short dataSize;
                if (!short.TryParse(ev.Parameters[2], out dataSize))
                {
                    dataSize = 1;
                }

                //Check source module exists
                if (source == null)
                {
                    if (!scriptWarningShown)
                    {
                        scriptWarningShown = true;
                        if (MessageBox.Show("A scripted event cannot be run because the source module does not exist. Continue the simulation?", "Script Error", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                            == MessageBoxResult.No)
                        {
                            abort = true;
                            return true;
                        }
                    }
                    return true;
                }

                //Send a packet
                if (targetBus is CSPBus)
                {
                    source.SendCSPPacket((CSPBus)targetBus, dest_address, 0, 0, (byte)source.Priority, dataSize, ev.Command);
                }
            }

            return false;
        }
    }
}
