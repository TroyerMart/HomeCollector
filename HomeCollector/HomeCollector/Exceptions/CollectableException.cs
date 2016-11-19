using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class CollectableException: Exception
    {
        public CollectableException() { }
        public CollectableException(string err) : base(err) { }
        public CollectableException(string err, Exception ex) : base(err, ex) { }
    }
}
