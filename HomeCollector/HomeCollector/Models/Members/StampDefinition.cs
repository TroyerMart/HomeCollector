using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class StampDefinition : IStampDefinition
    {
        // from ICollectableDefinition
        public string Title { get; set; }
        public string Description { get; set; }    // description of the generic item

        // from IStampDefinition
        public CountryEnum Country { get; set; } = CountryEnum.USA;
        public bool IsPostageStamp { get; set; } = true;
        public string ScottNumber { get; set; }
        public string AlternateId { get; set; }
        public int YearOfIssue { get; set; }
        public DateTime FirstDayOfIssue { get; set; }

        public StampDefinition()
        {
        }

        public bool Equals(ICollectableDefinition defnToCompare)
        {
            if (defnToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type defnType = defnToCompare.GetType();
            if (defnType != typeof(StampDefinition))
            {
                throw new CollectableException($"Invalid type {defnType}, expected type {typeof(StampDefinition)}");
            }
            StampDefinition stampDef = (StampDefinition)defnToCompare;
            if (Country != stampDef.Country)
            {
                return false;
            }
            if (ScottNumber != stampDef.ScottNumber)
            {
                return false;
            }
            return true;
        }

        public bool Equals(IStampDefinition defnToCompare, bool useAlternateId)
        {
            if (!useAlternateId)
            {
                return Equals(defnToCompare);
            }
            if (defnToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            if (Country != defnToCompare.Country)
            {
                return false;
            }
            if (AlternateId != defnToCompare.AlternateId)
            {
                return false;
            }
            return true;
        }

    }

}

    

