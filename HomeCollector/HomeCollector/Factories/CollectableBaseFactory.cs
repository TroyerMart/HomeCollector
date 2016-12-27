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
        public static Type BookType = typeof(BookBase);
        public static Type StampType = typeof(StampBase);
        public static List<Type> CollectableTypes = new List<Type>() { BookType, StampType };

        public static bool IsCollectableType (Type testType)
        {   // returns true if the type implements ICollectableBase, but is not actually the interface itself
            if (testType == null)
            {
                return false;
            }

            // explicitly test for membership in CollectableTypes above
            foreach (Type validType in CollectableTypes)
            {
                if (validType == testType)
                {
                    return true;
                }
            }

            if (typeof(ICollectableBase).IsAssignableFrom(testType))
            {
                return (testType != typeof(ICollectableBase));
            }
            return false;
        }

        public static ICollectableBase CreateCollectableBase(Type itemType)
        {   // this can be extended as more types are added            
            if (itemType == null)
            {
                throw new CollectableException($"Type cannot be null, Must be of a type implementing ICollectableBase");
            }
            if (! IsCollectableType(itemType))
            {
                throw new CollectableException($"Type must implement ICollectableBase");
            }
            if (itemType == BookType) {
                return new BookBase();
            }
            else if (itemType == StampType)
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
            itemTypeName = itemTypeName.ToLower();
            if (itemTypeName == BookType.Name.ToLower()) {
                return new BookBase();
            }
            else if (itemTypeName == StampType.Name.ToLower())
            {
                return new StampBase();
            } else
            {
                throw new CollectableException($"Undefined type {itemTypeName}, Must be of a type inherited from ICollectableBase");
            }
        }
    

    }

}
