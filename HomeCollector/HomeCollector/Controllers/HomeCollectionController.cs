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

        public HomeCollectionController(ICollectionBase homeCollection, IFileIO fileIO)
        {
            if (homeCollection == null)
            {
                throw new CollectionException("Controller must be initialized with a collection base object");
            }
            if (fileIO == null)
            {
                throw new FileIOException("File IO must not be null");
            }
            _homeCollection = homeCollection;
            _fileIO = fileIO;
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
                throw new CollectionException("Error adding item to collection", ex);
            }
        }

        public IList<ICollectableBase> GetCollection()
        {
            return _homeCollection.Collectables;
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
            HomeCollectionRepository repo = new HomeCollectionRepository(_homeCollection, _fileIO);
            try
            {
                repo.SaveCollection(fullFilePath, overwriteFile);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Unable to save collection", ex);
            }
        }

        public ICollectionBase LoadCollection(string fullFilePath)
        {   // load the collection from persistent storage via Repository
            HomeCollectionRepository repo = new HomeCollectionRepository(_homeCollection, _fileIO);
            try
            {
                _homeCollection = repo.LoadCollection(fullFilePath);
            }
            catch (Exception ex)
            {
                throw new CollectionException("Unable to load collection", ex);
            }
            return _homeCollection;
        }


    }

}
