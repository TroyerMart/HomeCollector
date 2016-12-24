using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Controllers
{
    public class HomeCollectionController
    {
        private ICollectionBase _homeCollection;

        public HomeCollectionController(ICollectionBase homeCollection)
        {
            if (homeCollection == null)
            {
                throw new CollectionException("Controller must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
        }

        public Type CollectionType { get { return _homeCollection.CollectionType; } }

        public void AddToCollection(ICollectableBase collectableToAdd)
        {
            try
            {
                _homeCollection.AddToCollection(collectableToAdd);
            }
            catch (Exception ex)
            {
                throw new CollectionException(ex.Message, ex);
            }
        }

        public IList<ICollectableBase> GetCollection()
        {
            return _homeCollection.GetCollection();
        }

        public void RemoveFromCollection(ICollectableBase collectableToRemove)
        {
            try
            {
                _homeCollection.RemoveFromCollection(collectableToRemove);
            }
            catch (Exception ex)
            {
                throw new CollectionException(ex.Message, ex);
            }
        }

        public void ClearCollection()
        {
            _homeCollection.ClearCollection();
        }

        public void SaveCollection(string fileName)
        {   // save the collection to persistent storage via Repository

        }

        public void LoadCollection(string fileName)
        {   // load the collection from persistent storage via Repository

        }


    }

}
