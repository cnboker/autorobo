using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class JsonDataMapField
    {
        public string CssClass { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public List<Option> Options { get; set; }
    }
}
