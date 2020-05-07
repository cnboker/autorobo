using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoRobo.Core.Actions.InOut.Configuration
{
    [Serializable()]
    public class ExcelSettings : IOSettings
    {
        public string FileName { get; set; }
        public bool IncludeHeader { get; set; }

        public ExcelSettings() { }
           //Deserialization constructor.
        public ExcelSettings(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            FileName = (string)info.GetValue("FileName", typeof(string));
            IncludeHeader = (bool)info.GetValue("IncludeHeader", typeof(bool));
        }
        
        //Serialization function.
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("FileName", FileName);
            info.AddValue("IncludeHeader", IncludeHeader ? "1" : "0");
            base.GetObjectData(info, ctxt);
        }
    }
}
