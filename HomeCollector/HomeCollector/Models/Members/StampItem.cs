using HomeCollector.Exceptions;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models.Members
{
    public class StampItem: ICollectableItem
    {
        public const string CONDITION_DEFAULT = "UNDEFINED";
        public static readonly List<string> STAMP_CONDITIONS = new List<string> {
            CONDITION_DEFAULT,
            "EF",   // Extra Fine - perfectly centered stamp with wide margins
            "VF",   // Very Fine - well-centered stamp with ample margins
            "F",    // Fine - significantly offset but still has four margins
            "A",    // Average - has at least one side with margin trimmed or cut by perforation
            "P"     // Poor - heavily cancelled, soiled, cut
        };

        private string _condition = CONDITION_DEFAULT;
        private decimal _estimatedValue = 0;

        public StampItem()
        {
        }

        public Type CollectableType  { get { return CollectableBaseFactory.StampType; } }

        // from ICollectableItem
        public string ItemDetails { get; set; }  // detail information specific to this actual item
        public bool IsFavorite { get; set; } = false;
        public decimal EstimatedValue
        {
            get
            {
                return _estimatedValue;
            }

            set
            {
                if (value < 0)
                {
                    throw new CollectableException("Estimated value must not be less than zero");
                }
                _estimatedValue = value;
            }
        }

        // Stamp properties
        public bool IsMintCondition { get; set; } = false;
        
        public string Condition {
            get { return _condition; }
            set {
                if (ValidateCondition(value))
                {
                    _condition = value;
                }
            else
                {
                    throw new CollectableException($"Invalid stamp condition: {value}");
                }
            }
        } 

        /************************** helper methods **********************************************************/
        internal static bool ValidateCondition(string condition)
        {
            if (STAMP_CONDITIONS.Contains(condition))
            {
                return true;
            }
            return false;
        }

    }

}
