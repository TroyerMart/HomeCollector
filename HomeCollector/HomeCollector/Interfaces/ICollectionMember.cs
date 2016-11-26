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
        Type ObjectType { get; }

        bool IsFavorite { get; set; }
        decimal EstimatedValue { get; set; }
        string ItemDetails { get; set; }  // detail information specific to this actual item
        
    }

}
