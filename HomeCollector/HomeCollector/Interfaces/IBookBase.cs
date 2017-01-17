using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    public interface IBookBase
    {
        string Title { get; set; }
        string Publisher { get; set; }
        string Author { get; set; }
        int Year { get; set; }
        int Month { get; set; }
        string Edition { get; set; }
        string ISBN { get; set; }
        string Series { get; set; }
        string BookCode { get;  set;}

    }

}
