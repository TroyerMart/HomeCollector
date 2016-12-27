using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
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
            // should also inject the IO

            if (homeCollection == null)
            {
                throw new CollectionException("Repository must be initialized with a collection base object");
            }
            _homeCollection = homeCollection;
        }

        // save collection to disk
        public void SaveCollection()
        {
            string jsonCollection = Newtonsoft.Json.JsonConvert.SerializeObject(_homeCollection);

            //jsonCollection = 

        }

        // load collection from disk

    }

}
