using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoRobo.Core.Actions.InOut.Configuration
{
    [Serializable()]
    public class MssqlSettings : IOSettings
    {
        public string ConnectionString { get; set; }
        public string SqlText { get; set; }

        public MssqlSettings() { }
              //Deserialization constructor.
        public MssqlSettings(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            ConnectionString = (string)info.GetValue("ConnectionString", typeof(string));
            SqlText = (string)info.GetValue("SqlText", typeof(string));            
        }
        
        //Serialization function.
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("ConnectionString", ConnectionString);
            info.AddValue("SqlText", SqlText); 
            base.GetObjectData(info, ctxt);
        }
    }
}
