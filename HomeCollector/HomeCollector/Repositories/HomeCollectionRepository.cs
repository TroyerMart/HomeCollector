using HomeCollector.Exceptions;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.IO;
using HomeCollector.Models;
using HomeCollector.Models.Members;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Repositories
{
    public class HomeCollectionRepository
    {
        private ICollectionBase _homeCollection;
        private IFileIO _fileIO;

        public HomeCollectionRepository(ICollectionBase homeCollection, IFileIO fileIO)
        {   
            if (fileIO == null)
            {
                throw new FileIOException("File IO must not be null");
            }

            if (homeCollection == null)
            {
                throw new CollectionException("Repository must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
            _fileIO = fileIO;
        }

        // save collection to disk
        public void SaveCollection(string fullFilePath)
        {
            bool overwriteFile = false;
            SaveCollection(fullFilePath, overwriteFile);
        }
        public void SaveCollection(string fullFilePath, bool overwriteFile)
        {
            string jsonCollection = null;
            try
            {
                jsonCollection = ConvertCollectionToJson(_homeCollection);
            }
            catch (CollectionParseException ex)
            {
                throw new CollectionException($"Unable to parse collection to Json", ex);
            }

            _fileIO.WriteFile(fullFilePath, jsonCollection, overwriteFile);
        }

        public void SaveCollection(string path, string filename)
        {
            bool overwriteFile = false;
            SaveCollection(path, filename, overwriteFile);
        }
        public void SaveCollection(string path, string filename, bool overwriteFile)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new CollectionException("SaveCollection path cannot be null or blank");
            }
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new CollectionException("SaveCollection filename cannot be null or blank");
            }
            string fullFilePath = _fileIO.GetFullFilePath(path, filename);

            SaveCollection(fullFilePath, overwriteFile);
        }

        // load collection from disk
        public ICollectionBase LoadCollection(string path, string filename)
        {
            // read from file system, parse, and initialize
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new CollectionException("LoadCollection path cannot be null or blank");
            }
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new CollectionException("LoadCollection filename cannot be null or blank");
            }
            string fullFilePath = _fileIO.GetFullFilePath(path, filename);

            return LoadCollection(fullFilePath);
        }

        public ICollectionBase LoadCollection(string fullFilePath)
        {
            // read from file system, parse, and initialize
            string jsonCollection = _fileIO.ReadFile(fullFilePath);

            try
            {
                _homeCollection = ConvertJsonToCollection(jsonCollection);
            }
            catch (Exception ex)
            {
                throw new CollectionException($"Unable to load collection: {fullFilePath}", ex);
            }
            return _homeCollection;
        }

        // Internal methods
        internal static string ConvertCollectionToJson(ICollectionBase collectionToSerialize)
        {
            if (collectionToSerialize == null)
            {
                throw new CollectionException("Null collection cannot be serialized");
            }
            string jsonCollection = JsonConvert.SerializeObject(collectionToSerialize);
            
            return jsonCollection;
        }

        internal static ICollectionBase ConvertJsonToCollection(string jsonCollection)
        {
            string collectionTypeName = null;
            try
            {
                dynamic collection = JsonConvert.DeserializeObject(jsonCollection);

                // This is not working as expected when it has child members
                //ICollectionBase collection = JsonConvert.DeserializeObject<HomeCollection>(jsonCollection);

                string collectionName = collection.CollectionName;
                collectionTypeName = collection.CollectionType;
                Type collectionType = CollectableBaseFactory.GetTypeFromFullName(collectionTypeName);
                ICollectionBase newCollection = new HomeCollection(collectionName, collectionType);

                var collectables = collection.Collectables;
                foreach (var c in collectables)
                {
                    string jsonCollectable = c.ToString();
                    // add custom try/catch??
                    ICollectableBase collectable = GetCollectableFromJson(jsonCollectable, collectionType);

                    newCollection.AddToCollection(collectable);
                }
                return newCollection;
            }
            catch (Exception ex)
            {
                throw new CollectionParseException($"Unable to parse Json into a collection object.  Type={collectionTypeName}, Json={jsonCollection}", ex);
            }            
        }

        internal static ICollectableBase GetCollectableFromJson(string jsonCollectable, Type collectionType)
        {
            ICollectableBase newCollectable = null;

            try
            {
                dynamic collectable = JsonConvert.DeserializeObject(jsonCollectable);
                var items = collectable["ItemInstances"];
                collectable["ItemInstances"] = null;    // clear so we can parse it more easily
                switch (collectionType.Name)
                {
                    case "BookBase":
                        newCollectable = JsonConvert.DeserializeObject<BookBase>(collectable.ToString());
                        break;
                    case "StampBase":
                        newCollectable = JsonConvert.DeserializeObject<StampBase>(collectable.ToString());
                        break;
                    default:
                        throw new CollectableParseException($"Unable to parse Json.  Unsupported collection type={collectionType}");
                }
                foreach (var item in items)
                {
                    // add custom try/catch??
                    ICollectableItem newItem = GetCollectableItemFromJson(item.ToString(), collectionType);
                    newCollectable.AddItem(newItem);
                }
                return newCollectable;

            }
            catch (Exception ex)
            {
                throw new CollectableParseException($"Unable to parse Json into a collectable object.  Type={collectionType}, Json={jsonCollectable}", ex);
            }
        }

        internal static ICollectableItem GetCollectableItemFromJson(string jsonItem, Type collectionType)
        {
            ICollectableItem item = null;
            try
            {
                switch (collectionType.Name)
                {
                    case "BookBase":
                        item = JsonConvert.DeserializeObject<BookItem>(jsonItem);
                        break;
                    case "StampBase":
                        item = JsonConvert.DeserializeObject<StampItem>(jsonItem);
                        break;
                    default:
                        throw new CollectableItemInstanceParseException($"Unable to parse Json.  Unsupported collection type={collectionType}");
                }
            }
            catch (Exception ex)
            {
                throw new CollectableItemInstanceParseException($"Unable to parse Json into a collectable item.  Type={collectionType}, Json={jsonItem}", ex);
            }
            return item;
        }

    }

}
