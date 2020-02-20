using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System;

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
            XDocument doc = XDocument.Parse(filepath);
            IEnumerable<ErrorObject> result = from c in doc.Descendants("Error")
                                              select new ErrorObject()
                                              {
                                                  id = (int)c.Attribute("id"),
                                                  isFatal = (bool)c.Attribute("isFatal")
                                              };
            
            for(int i = 0; i < result.Count(); i++)
            {
                ErrorObject toAdd = new ErrorObject();
                toAdd.id = result.Skip(i).First().id;
                toAdd.isFatal = result.Skip(i).First().isFatal;
                ErrorList.Add(toAdd);
            }

        }
    }
}