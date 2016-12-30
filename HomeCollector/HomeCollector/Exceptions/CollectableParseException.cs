using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class CollectableParseException : Exception
    {
        public CollectableParseException() { }
        public CollectableParseException(string err) : base(err) { }
        public CollectableParseException(string err, Exception ex) : base(err, ex) { }
    }
}
