using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Factories
{
    public class CollectableBaseFactory
    {
        public static bool IsICollectableType (Type itemType)
        {   // returns true if the type implements ICollectable
            if (itemType == null)
            {
                throw new CollectableException($"Type to be checked cannot be null");
            }
            if (typeof(ICollectableBase).IsAssignableFrom(itemType))
            {
                return true;
            }
            return false;
        }

        public static ICollectableBase CreateCollectableItem(Type itemType)
        {   // this can be extended as more types are added            
            if (itemType == null)
            {
                throw new CollectableException($"Type cannot be null, Must be of a type implementing ICollectableBase");
            }
            if (! IsICollectableType(itemType))
            {
                throw new CollectableException($"Type must implement ICollectableBase");
            }
            if (itemType == typeof(BookBase)) {
                return new BookBase();
            }
            else if (itemType == typeof(StampBase))
            {
                return new StampBase();
            }
            else
            {
                throw new CollectableException($"Undefined type {itemType.ToString()}, Unknown type implementing ICollectableBase");
            }
        }

        // add property to return item by string name of type
        public static ICollectableBase CreateCollectableItem(string itemTypeName)
        {   // this can be extended as more types are added
            if (string.IsNullOrWhiteSpace(itemTypeName))
            {
                throw new CollectableException($"TypeName cannot be null or empty");
            }
            switch (itemTypeName.ToLower())
            {
                case "bookbase": return new BookBase();
                case "stampbase": return new StampBase();
                default:
                    throw new CollectableException($"Undefined type {itemTypeName}, Must be of a type inherited from ICollectableBase");
            }
        }
    

    }

}
