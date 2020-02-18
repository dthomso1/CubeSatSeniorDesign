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

        public static void fillErrorList()
        {
            //string xmlString =  System.IO.File.ReadAllText(@"C:\Users\David\source\repos\SeniorDesignNewBranch\Source\CubeSatCommSim\Data\ErrorInfo.xml");

            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\ErrorInfo.xml");
            var com = from p in  XElement.Load(filepath).Elements("id")
                                 .Elements("isFatal")
                       orderby (string)p.Element("id") ascending
                       select new ErrorObject
                       {
                          id = (int)p.Element("id"),
                          isFatal = (bool)p.Element("isFatal")
                       };

            foreach (var c in com)
                ErrorList.Add(c);

        }
    }
}