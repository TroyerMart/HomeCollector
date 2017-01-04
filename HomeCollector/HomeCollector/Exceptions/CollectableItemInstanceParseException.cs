using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class CollectableItemInstanceParseException : CollectableException
    {
        public CollectableItemInstanceParseException() { }
        public CollectableItemInstanceParseException(string err) : base(err) { }
        public CollectableItemInstanceParseException(string err, Exception ex) : base(err, ex) { }
    }
}
