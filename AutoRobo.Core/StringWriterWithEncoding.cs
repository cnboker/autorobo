using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AutoRobo.Core
{
    public class StringWriterWithEncoding : StringWriter
    {
        private Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }

}
