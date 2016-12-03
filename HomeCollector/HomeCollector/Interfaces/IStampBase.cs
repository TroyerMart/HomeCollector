﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    public enum StampCountryEnum
    {
        USA,
        Canada
    }
    public enum StampConditionEnum
    {
        ExtraFine,  // perfectly centered stamp with wide margins
        VeryFine,   // well-centered stamp with ample margins
        Fine,       // significantly offset but still has four margins
        Average,    // has at least one side with margin trimmed or cut by perforation
        Poor,        // heavily cancelled, soiled, cut
        Undefined
    }
    public interface IStampBase
    {
        StampCountryEnum Country { get; set; }
        bool IsPostageStamp { get; set; }
        string ScottNumber { get; set; }
        string AlternateId { get; set; }
        int YearOfIssue { get; set; }
        DateTime FirstDayOfIssue { get; set; }

    }

}