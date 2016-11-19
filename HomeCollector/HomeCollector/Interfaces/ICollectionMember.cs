using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This represents a member of a generic collection, 
    // such as a stamp belonging to a stamp collection
    public interface ICollectionMember
    {
        ICollectableDefinition Item { get; set; }
        bool IsPlaceholder { get; set; }   // allow an item to be added to the collection as a missing item, i.e. a wish list item
        bool IsFavorite { get; set; }
        decimal EstimatedValue { get; set; }
        string MemberDetails { get; set; }  // detail information specific to this actual item
    }

}
