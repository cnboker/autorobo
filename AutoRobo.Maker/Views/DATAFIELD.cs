using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoRobo.Makers.Views
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DATAFIELD
    {
        public DataType type;
        public string name;
        public string xpath;
        public string heading;
        public bool pattern;
        public string regex;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct URLDATA
    {
        public string name;
        public string url;
        public string headers;
        public byte[] postData;
    }
    public enum DataType
    {
        Text,
        Url,
        Image,
        HTML,
        File,
        Link_Follow,
        Link_Back,
        Link_NextPage,
        Text_Near_Heading,
        Click
    }
}
