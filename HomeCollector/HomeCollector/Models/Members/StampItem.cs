using HomeCollector.Exceptions;
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
        decimal _estimatedValue = 0;

        public StampItem()
        {
        }

        public Type ObjectType  { get { return GetType(); } }

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
        public StampConditionEnum Condition { get; set; } = StampConditionEnum.Undefined;

    }

}
