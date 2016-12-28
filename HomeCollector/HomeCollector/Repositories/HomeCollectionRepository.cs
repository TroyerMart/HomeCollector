using HomeCollector.Exceptions;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using HomeCollector.Models.Members;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Repositories
{
    public class HomeCollectionRepository
    {
        private ICollectionBase _homeCollection;

        public HomeCollectionRepository(ICollectionBase homeCollection)
        {   
            // should also inject the IO ???

            if (homeCollection == null)
            {
                throw new CollectionException("Repository must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
        }

        // save collection to disk
        public void SaveCollection(string path, string filename)
        {
            string jsonCollection = ConvertCollectionToJson(_homeCollection);
            // write to disk
        }

        // load collection from disk

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
                    ICollectableBase collectable = GetCollectableFromJson(jsonCollectable, collectionType);

                    newCollection.AddToCollection(collectable);
                }
                return newCollection;
            }
            catch (Exception ex)
            {
                throw new CollectionException($"Unable to parse Json into a collection object.  Type={collectionTypeName}, Json={jsonCollection}", ex);
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
                        throw new CollectionException($"Unable to parse Json.  Unsupported collection type={collectionType}");
                }
                foreach (var item in items)
                {
                    ICollectableItem newItem = GetCollectableItemFromJson(item.ToString(), collectionType);
                    newCollectable.AddItem(newItem);
                }
                return newCollectable;

            }
            catch (Exception ex)
            {
                throw new CollectionException($"Unable to parse Json into a collectable object.  Type={collectionType}, Json={jsonCollectable}", ex);
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
                        throw new CollectionException($"Unable to parse Json.  Unsupported collection type={collectionType}");
                }
            }
            catch (Exception ex)
            {
                throw new CollectionException($"Unable to parse Json into a collectable item.  Type={collectionType}, Json={jsonItem}", ex);
            }
            return item;
        }

    }

}
