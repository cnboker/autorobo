using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
  
    public interface IMapAttribute 
    {
        string Name { get; set; }
        string Get(string attributeName);
        void Set(string attributeName, string val);
        void Merge(IMapAttribute map);
        List<DataMapField> Fields
        {
            get;
            set;
        }
    }
}
