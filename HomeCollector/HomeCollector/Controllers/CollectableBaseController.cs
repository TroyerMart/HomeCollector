using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Controllers
{
    public class CollectableBaseController
    {
        private ICollectableBase _collectableBase;

        public CollectableBaseController(ICollectableBase collectableBase)
        {
            if (collectableBase == null)
            {
                throw new CollectableException("Controller must be initialized with a collectable base object");
            }
            _collectableBase = collectableBase;
        }

        public Type CollectableType { get { return _collectableBase.CollectableType; } }

        public void AddItem(ICollectableItem itemToAdd)
        {
            _collectableBase.AddItem(itemToAdd);
        }

        public void RemoveItem(ICollectableItem itemToRemove)
        {
            _collectableBase.RemoveItem(itemToRemove);
        }

        public IList<ICollectableItem> GetItems()
        {
            return _collectableBase.ItemInstances;
        }

        public bool IsSame(ICollectableBase itemToCompare, bool useAlternateId)
        {
            return _collectableBase.IsSame(itemToCompare, useAlternateId);
        }
    
    }

}
