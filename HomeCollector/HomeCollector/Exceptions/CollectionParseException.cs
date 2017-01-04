using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class CollectionParseException : CollectionException
    {
        public CollectionParseException() { }
        public CollectionParseException(string err) : base(err) { }
        public CollectionParseException(string err, Exception ex) : base(err, ex) { }
    }
}
