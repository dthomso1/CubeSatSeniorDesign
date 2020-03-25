using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

namespace CubeSatCommSim.Model
{
    public static class ModuleObjectList
    {
        public static ObservableCollection<ModuleObject> ModuleList { get; private set; } = new ObservableCollection<ModuleObject>();

        public static void AddModule(ModuleObject module)
        {
            ModuleList.Add(module);
        }

        public static void FillModuleList()
        {
            //string xmlString =  System.IO.File.ReadAllText(@"C:\Users\David\source\repos\SeniorDesignNewBranch\Source\CubeSatCommSim\Data\ErrorInfo.xml");
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\ModuleInformation.xml");
            	//<name>SASI</name>
	            //<address>1</address>
	            //<priority>0</priority>
	            //<connections>CANBUS</connections>

            XDocument doc = XDocument.Parse(File.ReadAllText(filepath));
            IEnumerable<ModuleObject> result = from c in doc.Descendants("Module")
                                              select new ModuleObject()
                                              {
                                                  name = c.Element(c.Element("name").Value),
                                                  address = int.Parse(c.Element("address").Value),
                                                  priority = int.Parse("priority").Value,
                                                  connections = c.Element("connections").Value
                                              };

            foreach (ModuleObject mo in result)
            {
                ModuleList.Add(mo);
            }
        }
        public static void ListToXml()
        {
            foreach (ModuleObject mod in ModuleList)
            {
                ObjectXMLSerializer<ModuleObject>.Save(mod, "ModuleInformation.xml");
            }
        }
    }
}