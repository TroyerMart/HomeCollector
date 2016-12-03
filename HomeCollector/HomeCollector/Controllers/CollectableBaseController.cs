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

        public Type ObjectType { get { return _collectableBase.ObjectType; } }

        public void AddItem(ICollectableMember memberToAdd)
        {
            _collectableBase.AddItem(memberToAdd);
        }

        public void RemoveItem(ICollectableMember memberToRemove)
        {
            _collectableBase.RemoveItem(memberToRemove);
        }

        public IList<ICollectableMember> GetItems()
        {
            return _collectableBase.GetItems();
        }

        public bool IsSame(ICollectableBase itemToCompare, bool useAlternateId)
        {
            return _collectableBase.IsSame(itemToCompare, useAlternateId);
        }
    

    }

}
