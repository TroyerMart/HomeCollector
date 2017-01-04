using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Exceptions
{
    public class FileIOException: Exception
    {
        public FileIOException() { }
        public FileIOException(string err) : base(err) { }
        public FileIOException(string err, Exception ex) : base(err, ex) { }
    }
}
