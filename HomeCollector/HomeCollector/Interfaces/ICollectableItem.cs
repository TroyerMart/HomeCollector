using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This represents an instance of a base type, 
    // such as a particular stamp of a given base type belonging to a stamp collection
    public interface ICollectableItem
    {
        Type CollectableType { get; }

        bool IsFavorite { get; set; }
        decimal EstimatedValue { get; set; }
        string ItemDetails { get; set; }  // detail information specific to this actual item
        
    }

}
