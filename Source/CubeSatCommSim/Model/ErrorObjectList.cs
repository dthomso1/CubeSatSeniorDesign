using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;

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
            var com = from p in XElement.Load(filepath).Elements("id")
                                 .Elements("isFatal")
                      orderby (string)p.Element("id") ascending
                      select new ErrorObject
                      {
                          id = (int)p.Element("id"),
                          isFatal = (bool)p.Element("isFatal"),
                          isSelected = false,
                          description = (string)p.Element("description"),
                          ModuleAffected = (string)p.Element("moduleAffected")
                      };

            foreach (var c in com)
                ErrorList.Add(c);

        }
    }
}