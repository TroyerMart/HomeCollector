using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    
    public interface IStampBase
    {
        String Country { get; set; }
        bool IsPostageStamp { get; set; }
        string ScottNumber { get; set; }
        string AlternateId { get; set; }
        int YearOfIssue { get; set; }
        DateTime FirstDayOfIssue { get; set; }

        //string Perforation { get; set; }
        //bool IsWatermarked { get; set; }
        //string CatalogImageCode { get; set; }
        //string Color { get; set; }
        //double Denomination { get; set; }

    }

}
