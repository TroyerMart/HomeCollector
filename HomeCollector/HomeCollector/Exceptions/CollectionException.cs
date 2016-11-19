using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class CollectionException : Exception
    {
        public CollectionException() { }
        public CollectionException(string err) : base(err) { }
        public CollectionException(string err, Exception ex) : base(err, ex) { }
    }
}
