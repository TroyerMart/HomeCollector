using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using Newtonsoft.Json;
using System;
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
            ICollectionBase collection = JsonConvert.DeserializeObject<HomeCollection>(jsonCollection);

            return collection;
        }

    }

}
