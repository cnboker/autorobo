using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class DataMapFactory
    {
        static public DataMapProvider Create(string name) {
            if (name == "sqlProvider") {
                return new SqlDataMapProvider();
            }
            else if (name == "sqlliteProvider")
            {
                return new SqlliteDataMapProvider();
            }
            else {
                return new DataMapProvider();
            }
        }
    }
}
