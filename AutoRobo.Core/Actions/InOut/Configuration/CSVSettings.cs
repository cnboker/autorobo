using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoRobo.Core.Actions.InOut.Configuration
{
    [Serializable()]
    public class CSVSettings : IOSettings
    {
        public string FileName { get; set; }
        public char Spilter { get; set; }
        public bool IncludeHeader { get; set; }

        public CSVSettings() { }

        //Deserialization constructor.
        public CSVSettings(SerializationInfo info, StreamingContext ctxt):base(info,ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            FileName = (string)info.GetValue("FileName", typeof(string));
            Spilter = (char)info.GetValue("Spilter", typeof(char));
            IncludeHeader = (bool)info.GetValue("IncludeHeader", typeof(bool));
        }
        
        //Serialization function.
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("FileName", FileName);
            info.AddValue("Spilter", Spilter);
            info.AddValue("IncludeHeader", IncludeHeader ? "1" :  "0");
            base.GetObjectData(info, ctxt);
        }
    }
}
