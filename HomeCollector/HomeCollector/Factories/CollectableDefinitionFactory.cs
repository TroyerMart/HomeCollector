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
    public class CollectableDefinitionFactory
    {
        public static ICollectableDefinition CreateCollectableItem(Type itemType)
        {   // this needs to be extended as more types are added
            if (itemType == null)
            {
                throw new CollectableException($"Type cannot be null, Must be of a type inherited from ICollectableItem");
            }
            if (itemType == typeof(BookDefinition)) {
                return new BookDefinition();
            }
            else if (itemType == typeof(StampDefinition))
            {
                return new StampDefinition();
            }
            else
            {
                throw new CollectableException($"Undefined type {itemType.ToString()}, Must be of a type inherited from ICollectableItem");
            }

        }

    }

}
