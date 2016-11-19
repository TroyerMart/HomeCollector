using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models.Members
{
    public class Stamp: ICollectionMember
    {
        public ICollectableDefinition Item { get; set; }
        public bool IsPlaceholder { get; set; }   // allow an item to be added to the collection as a missing item, i.e. a wish list item
        public bool IsFavorite { get; set; }
        public decimal EstimatedValue { get; set; }
        public string MemberDetails { get; set; }  // detail information specific to this actual item
        public bool IsMintCondition { get; set; } = false;
        public StampConditionEnum Condition { get; set; } = StampConditionEnum.Undefined;

    }

}
