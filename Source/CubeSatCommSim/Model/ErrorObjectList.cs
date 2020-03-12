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
    public static class ErrorObjectList
    {
        public static ObservableCollection<ErrorObject> ErrorList { get; private set; } = new ObservableCollection<ErrorObject>();

        public static void AddError(ErrorObject error)
        {
            ErrorList.Add(error);
        }

        public static void FillErrorList()
        {
            //string xmlString =  System.IO.File.ReadAllText(@"C:\Users\David\source\repos\SeniorDesignNewBranch\Source\CubeSatCommSim\Data\ErrorInfo.xml");
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\ErrorInfo.xml");
            
            XDocument doc = XDocument.Parse(File.ReadAllText(filepath));
            IEnumerable<ErrorObject> result = from c in doc.Descendants("Error")
                                              select new ErrorObject()
                                              {
                                                  id = int.Parse(c.Element("id").Value),
                                                  Behaviour = c.Element("behaviour").Value,
                                                  Description = c.Element("description").Value,
                                                  ModuleAffected = c.Element("moduleAffected").Value
                                              };
            
            foreach(ErrorObject eo in result)
            {
                ErrorList.Add(eo);
            }
        }
    }
}