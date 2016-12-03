using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This is a base description of a collectable item of a particular type
    //    such as a 1997 Bugs Bunny US Postal Stamp 
    //    or a 2nd edition of Foundation by Isaac Asimov
    public interface ICollectableBase
    {
        Type ObjectType { get; }

        string DisplayName { get; set; }
        string Description { get; set; }    // description of the generic item

        IList<ICollectableMember> GetItems();
        void AddItem(ICollectableMember itemToAdd);
        void RemoveItem(ICollectableMember itemToRemove);

        bool IsSame(ICollectableBase itemToCompare, bool useAlternateId);
        
    }

}
