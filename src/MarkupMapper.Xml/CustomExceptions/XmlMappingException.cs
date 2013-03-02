using MarkupMapper.Core.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupMapper.Xml.CustomExceptions
{
    public class XmlMappingException : MappingException
    {
        public XmlMappingException(string message)
            : base(message)
        {}

        public XmlMappingException(string message, Exception inner)
            : base(message, inner)
        {}
    }
}
