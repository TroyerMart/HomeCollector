using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This represents a generic collection of items 

    public interface ICollectionBase
    {
        string CollectionName { get; set; }
        Type CollectionType { get; }   // the actual type of collectable objects allowed to be contained in the collection
        IList<ICollectableBase> Collectables { get; }

        void AddToCollection(ICollectableBase collectableToAdd);
        void RemoveFromCollection(ICollectableBase collectableToRemove);
        void ClearCollection();

    }

}
