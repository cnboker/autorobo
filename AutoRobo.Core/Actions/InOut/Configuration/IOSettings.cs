using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoRobo.Core.Actions.InOut.Configuration
{
    [Serializable()]
    public class IOSettings : ISerializable 
    {
        public IODirection Direction { get; set; }

        public IOSettings() { }
        public IOSettings(SerializationInfo info, StreamingContext ctxt)
        {
            Direction = (IODirection)info.GetValue("Direction", typeof(int));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Direction", (int)Direction);
        }
    }
}
