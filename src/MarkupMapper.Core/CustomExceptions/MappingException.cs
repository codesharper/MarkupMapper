using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupMapper.Core.CustomExceptions
{
    public class MappingException : Exception
    {
        public MappingException(string message)
            : base(message)
        { }

        public MappingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
