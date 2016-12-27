using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Repositories;
using HomeCollector.Exceptions;
using HomeCollector.Models.Members;
using System.Linq;

namespace HomeCollector_UnitTests.Repositories
{
    [TestClass]
    public class HomeCollectionRepositoryTests
    {
        [TestMethod]
        public void converttojson_serializes_new_collection_successfully()
        {
            string collectionName = "test";
            Type collectableType = CollectableBaseFactory.CollectableTypes[0];
            ICollectionBase collection = new HomeCollection(collectionName, collectableType);
            string expected = @"{""CollectionName"":""test"",""CollectionType"":""HomeCollector.Models.BookBase, HomeCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",""Collectables"":[]}";

            string json = HomeCollectionRepository.ConvertCollectionToJson(collection);
            // This is a very fragile test.  Should look for expected tokens instead.
            Assert.AreEqual(expected, json);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void converttojson_throws_exception_when_collection_is_null()
        {
            ICollectionBase collection = null;

            string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

            Assert.Fail("Should have thrown exception when repository is null");
        }

        [TestMethod]
        public void converttojson_collection_with_collectables_only_success()
        {
            int N = 3;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase collection = GetTestCollection(collectionType, N, 0);

                string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

                int countCollectionNameTag = CountOccurences(json, "CollectionName");
                int countCollectionTypeTag = CountOccurences(json, "CollectionType");
                int countDisplayNameTag = CountOccurences(json, "DisplayName");
                int countDescriptionTag = CountOccurences(json, "Description");
                int countItemInstanceTag = CountOccurences(json, "ItemInstances");

                Assert.AreEqual(1, countCollectionNameTag);
                Assert.AreEqual(1, countCollectionTypeTag);
                Assert.AreEqual(N, countDisplayNameTag);
                Assert.AreEqual(N, countDescriptionTag);
                Assert.AreEqual(N, countItemInstanceTag);
            }
        }

        [TestMethod]
        public void converttojson_collection_with_collectables_and_instances_success()
        {
            int N = 3;
            int M = 2;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {  
                ICollectionBase collection = GetTestCollection(collectionType, N, M);

                string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

                int countCollectionNameTag = CountOccurences(json, "CollectionName");
                int countCollectionTypeTag = CountOccurences(json, "CollectionType");
                int countDisplayNameTag = CountOccurences(json, "DisplayName");
                int countDescriptionTag = CountOccurences(json, "Description");
                int countItemInstanceTag = CountOccurences(json, "ItemInstances");
                int countItemDetailsTag = CountOccurences(json, "ItemDetails");
                int countIsFavoriteTag = CountOccurences(json, "IsFavorite");

                Assert.AreEqual(1, countCollectionNameTag);
                Assert.AreEqual(1, countCollectionTypeTag);
                Assert.AreEqual(N, countDisplayNameTag);
                Assert.AreEqual(N, countDescriptionTag);
                Assert.AreEqual(N, countItemInstanceTag);
                Assert.AreEqual(N * M, countItemDetailsTag);
                Assert.AreEqual(N * M, countIsFavoriteTag);                
            }
        }

        [TestMethod]
        public void convertjsontocollection_deserializes_to_collection_success()
        {
            string expectedCollectionName = "test";
            
            string jsonBookCollection = @"{""CollectionName"":""test"",""CollectionType"":""HomeCollector.Models.BookBase, HomeCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",""Collectables"":[]}";
            //Type collectableType = CollectableBaseFactory.CollectableTypes[0];
            //ICollectionBase collection = new HomeCollection(collectionName, collectableType);

            ICollectionBase collection = HomeCollectionRepository.ConvertJsonToCollection(jsonBookCollection);
            // This is a very fragile test.  Should look for expected tokens instead.

            Assert.AreEqual(expectedCollectionName, collection.CollectionName);
            Assert.AreEqual(0, collection.Collectables.Count);
            Assert.AreEqual(CollectableBaseFactory.BookType, collection.CollectionType);
        }

        // want to test separately each type: book, stamp, ...

        /****** helper methods ***********************************************************************/
        private ICollectionBase GetTestCollection(Type collectableType, int numberOfCollectables, int numberOfItemsPerCollectable = 0)
        {
            Type collectionType = collectableType;
            ICollectionBase testCollection = new HomeCollection("initial", collectionType);
            for (int i = 0; i < numberOfCollectables; i++)
            {
                ICollectableBase collectable = CollectableBaseFactory.CreateCollectableBase(collectableType);
                collectable.DisplayName = $"Collectable{i}";
                testCollection.AddToCollection(collectable);

                for (int j=0; j<numberOfItemsPerCollectable; j++)
                {
                    ICollectableItem item = CollectableBaseFactory.CreateAndAddCollectableItem(collectable);
                    //ICollectableItem item = new BookItem();   // need a factory??
                    item.ItemDetails = $"Details{j}";
                    //collectable.AddItem(item);
                }
            }
            return testCollection;
        }

        private int CountOccurences(string body, string findThis)
        {
            int count = 0;
            int position = 0;
            if (String.IsNullOrEmpty(findThis)) return 0;
            while ((position = body.IndexOf(findThis, position)) != -1)
            {
                position += findThis.Length;
                count += 1;
            }
            return count;
        }

    }

}
