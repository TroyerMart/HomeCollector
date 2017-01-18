using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    
    public interface IStampBase
    {
        string Country { get; set; }
        bool IsPostageStamp { get; set; }
        string ScottNumber { get; set; }
        string AlternateId { get; set; }
        int IssueYearStart { get; set; }  
        int IssueYearEnd { get; set; }  
        DateTime FirstDayOfIssue { get; set; }

        string Perforation { get; set; }
        bool IsWatermarked { get; set; }
        string CatalogImageCode { get; set; }
        string Color { get; set; }
        string Denomination { get; set; }        

    }

}
