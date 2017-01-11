using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Repositories;
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
        private IFileIO _fileIO;
        IHomeCollectionRepository _repo;

        public HomeCollectionController(ICollectionBase homeCollection, IFileIO fileIO)
        {
            if (homeCollection == null)
            {
                throw new CollectionException("Injected controller must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
            if (fileIO == null)
            {
                throw new FileIOException("Injected file IO must not be null");
            }
            _fileIO = fileIO;
            _repo = null;
        }

        public HomeCollectionController(ICollectionBase homeCollection, IFileIO fileIO, IHomeCollectionRepository repo)
        {
            if (homeCollection == null)
            {
                throw new CollectionException("Injected controller must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
            if (fileIO == null)
            {
                throw new FileIOException("Injected file IO must not be null");
            }
            _fileIO = fileIO;
            if (repo == null)
            {
                throw new FileIOException("Injected repository must not be null");
            }
            _repo = repo;
        }

        public Type CollectionType { get { return _homeCollection.CollectionType; } }

        public IList<ICollectableBase> ToList()
        {
            return _homeCollection.Collectables;
        }

        public ICollectionBase GetCollection()
        {
            return _homeCollection;
        }

        public void AddToCollection(ICollectableBase collectableToAdd)
        {
            try
            {
                _homeCollection.AddToCollection(collectableToAdd);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Error adding item to collection", ex);
            }
        }

        public void RemoveFromCollection(ICollectableBase collectableToRemove)
        {
            try
            {
                _homeCollection.RemoveFromCollection(collectableToRemove);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Error removing item from collection", ex);
            }
        }

        public void ClearCollection()
        {
            _homeCollection.ClearCollection();
        }

        public void SaveCollection(string fullFilePath)
        {
            bool overwriteFile = false;
            SaveCollection(fullFilePath, overwriteFile);
        }
        public void SaveCollection(string fullFilePath, bool overwriteFile)
        {   // save the collection to persistent storage via Repository
            try
            {
                Repository().SaveCollection(fullFilePath, overwriteFile);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Unable to save collection", ex);
            }
        }

        public ICollectionBase LoadCollection(string fullFilePath)
        {   // load the collection from persistent storage via Repository
            try
            {   
                _homeCollection = Repository().LoadCollection(fullFilePath);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Unable to load collection", ex);
            }
            return _homeCollection;
        }




        /****************************************** helper methods **********************************************************/
        internal IHomeCollectionRepository Repository()
        {
            try
            {
                if (_repo == null)
                {   
                    _repo = new HomeCollectionRepository(_homeCollection, _fileIO);
                }                
                return _repo;
            }
            catch (Exception ex)
            {
                throw new CollectionException("Unable to initialize Repository", ex);
            }
        }
    }
}
