using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Repositories;
using HomeCollector.Exceptions;
using HomeCollector.Models.Members;
using System.Linq;
using Newtonsoft.Json;

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
                ICollectionBase collection = GetTestCollection("initial", collectionType, N, 0);

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
                ICollectionBase collection = GetTestCollection("initial", collectionType, N, M);

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

            ICollectionBase collection = HomeCollectionRepository.ConvertJsonToCollection(jsonBookCollection);
            // This is a very fragile test.  Should look for expected tokens instead.

            Assert.AreEqual(expectedCollectionName, collection.CollectionName);
            Assert.AreEqual(0, collection.Collectables.Count);
            Assert.AreEqual(CollectableBaseFactory.BookType, collection.CollectionType);
        }

        [TestMethod]
        public void collection_serialize_deserialize_success()
        {
            string collectionName = "test";
            int N = 2;
            int M = 2;
            foreach(Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetTestCollection(collectionName, collectionType, N, M);

                //string jsonCollection = @"{""CollectionName"":""test"",""CollectionType"":""HomeCollector.Models.BookBase, HomeCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",""Collectables"":[]}";
                string jsonCollection = HomeCollectionRepository.ConvertCollectionToJson(testCollection);
                ICollectionBase resultCollection = HomeCollectionRepository.ConvertJsonToCollection(jsonCollection);
                
                Assert.AreEqual(collectionName, resultCollection.CollectionName);
                Assert.AreEqual(collectionType, resultCollection.CollectionType);
                Assert.AreEqual(N, resultCollection.Collectables.Count);
                Assert.AreEqual(N * M, resultCollection.Collectables.Count * resultCollection.Collectables[0].ItemInstances.Count);
            }
        }

        [TestMethod]
        public void collectable_serialize_deserialize_book_success()
        {
            BookBase collectable = GetTestBookBase(2);
            collectable.AddItem(GetTestBookItem(0));
            collectable.AddItem(GetTestBookItem(1));

            string jsonCollectable = JsonConvert.SerializeObject(collectable);
            BookBase newCollectable = (BookBase)HomeCollectionRepository.GetCollectableFromJson(jsonCollectable, collectable.CollectableType);  //JsonConvert.DeserializeObject<BookBase>(jsonCollectable);

            Assert.AreEqual(collectable.CollectableType, newCollectable.CollectableType);
            Assert.AreEqual(collectable.Author, newCollectable.Author);
            Assert.AreEqual(collectable.BookCode, newCollectable.BookCode);
            Assert.AreEqual(collectable.DatePublished, newCollectable.DatePublished);
            Assert.AreEqual(collectable.Description, newCollectable.Description);
            Assert.AreEqual(collectable.DisplayName, newCollectable.DisplayName);
            Assert.AreEqual(collectable.Edition, newCollectable.Edition);
            Assert.AreEqual(collectable.ISBN, newCollectable.ISBN);
            Assert.AreEqual(collectable.Publisher, newCollectable.Publisher);
            Assert.AreEqual(collectable.Series, newCollectable.Series);
            Assert.AreEqual(collectable.Title, newCollectable.Title);
            Assert.AreEqual(collectable.Year, newCollectable.Year);
        }

        [TestMethod]
        public void collectable_serialize_deserialize_stamp_success()
        {
            StampBase collectable = GetTestStampBase(5);
            collectable.AddItem(GetTestStampItem(0));
            collectable.AddItem(GetTestStampItem(1));
            collectable.AddItem(GetTestStampItem(2));

            string jsonItem = JsonConvert.SerializeObject(collectable);
            StampBase newCollectable = (StampBase)HomeCollectionRepository.GetCollectableFromJson(jsonItem, collectable.CollectableType); //JsonConvert.DeserializeObject<StampBase>(jsonItem);

            Assert.AreEqual(collectable.CollectableType, newCollectable.CollectableType);
            Assert.AreEqual(collectable.AlternateId, newCollectable.AlternateId);
            Assert.AreEqual(collectable.Country, newCollectable.Country);
            Assert.AreEqual(collectable.Description, newCollectable.Description);
            Assert.AreEqual(collectable.DisplayName, newCollectable.DisplayName);
            Assert.AreEqual(collectable.FirstDayOfIssue, newCollectable.FirstDayOfIssue);
            Assert.AreEqual(collectable.IsPostageStamp, newCollectable.IsPostageStamp);
            Assert.AreEqual(collectable.ScottNumber, newCollectable.ScottNumber);
            Assert.AreEqual(collectable.YearOfIssue, newCollectable.YearOfIssue);
        }

        [TestMethod]
        public void instanceitem_serialize_deserialize_book_type_success()
        {  
            BookItem item = GetTestBookItem(7);
            string jsonItem = JsonConvert.SerializeObject(item);

            BookItem newItem = (BookItem)HomeCollectionRepository.GetCollectableItemFromJson(jsonItem, item.CollectableType);

            Assert.AreEqual(item.CollectableType, newItem.CollectableType);
            Assert.AreEqual(item.EstimatedValue, newItem.EstimatedValue);
            Assert.AreEqual(item.ItemDetails, newItem.ItemDetails);
            Assert.AreEqual(item.IsFavorite, newItem.IsFavorite);
            Assert.AreEqual(item.Condition, newItem.Condition);
        }

        [TestMethod]
        public void instanceitem_serialize_deserialize_stamp_type_success()
        {
            StampItem item = GetTestStampItem(4);
            string jsonItem = JsonConvert.SerializeObject(item);

            StampItem newItem = (StampItem)HomeCollectionRepository.GetCollectableItemFromJson(jsonItem, item.CollectableType);

            Assert.AreEqual(item.CollectableType, newItem.CollectableType);
            Assert.AreEqual(item.EstimatedValue, newItem.EstimatedValue);
            Assert.AreEqual(item.ItemDetails, newItem.ItemDetails);
            Assert.AreEqual(item.IsFavorite, newItem.IsFavorite);
            Assert.AreEqual(item.Condition, newItem.Condition);
        }

        // test bad json

        /****** helper methods ***********************************************************************/
        private BookItem GetTestBookItem(int i)
        {
            BookItem item = new BookItem()
            {
                EstimatedValue = i * 0.75M,
                IsFavorite = false,
                ItemDetails = $"Test{i}",
                Condition = BookConditionEnum.Fine
            };
            return item;
        }
        private BookBase GetTestBookBase(int i)
        {
            BookBase collectable = new BookBase()
            {
                Author = $"Author{i}",
                BookCode = $"ABC{i}",
                DatePublished = DateTime.Today.AddDays(-i),
                Description = $"description{i}",
                DisplayName = $"display{i}",
                Edition = $"edition{i}",
                ISBN = $"123-4442111-{i}",
                Publisher = $"publisher{i}",
                Series = null,
                Title = $"title{i}",
                Year = 2000 + i
            };
            return collectable;
        }
        private StampItem GetTestStampItem(int i)
        {
            StampItem item = new StampItem()
            {
                EstimatedValue = i * 0.50M,
                IsFavorite = true,
                ItemDetails = $"Test{i}",
                Condition = StampConditionEnum.VeryFine,
                IsMintCondition = true
            };
            return item;
        }
        private StampBase GetTestStampBase(int i)
        {
            StampBase collectable = new StampBase()
            {
                AlternateId = $"alternateid{i}",
                Country = StampCountryEnum.USA,
                Description = $"description{i}",
                DisplayName = $"displayname{i}",
                FirstDayOfIssue = DateTime.Today.AddDays(- i * 100),
                IsPostageStamp = true,
                ScottNumber = $"scottnumber{i}",
                YearOfIssue =2000 + i
            };
            return collectable;
        }

        private ICollectionBase GetTestCollection(string collectionName, Type collectableType, int numberOfCollectables, int numberOfItemsPerCollectable = 0)
        {
            ICollectableBase collectable = null;
            ICollectableItem item = null;
            Type collectionType = collectableType;
            ICollectionBase testCollection = new HomeCollection(collectionName, collectionType);
            for (int i = 0; i < numberOfCollectables; i++)
            {
                //ICollectableBase collectable = CollectableBaseFactory.CreateCollectableBase(collectableType);
                //collectable.DisplayName = $"Collectable{i}";
                if (collectionType == CollectableBaseFactory.BookType)
                {
                    collectable = GetTestBookBase(i);
                    //((BookBase)collectable).Author = "Asimov";
                } else if (collectionType == CollectableBaseFactory.StampType)
                {
                    collectable = GetTestStampBase(i);
                    //((StampBase)collectable).Country = StampCountryEnum.Canada;
                }
                testCollection.AddToCollection(collectable);

                for (int j=0; j<numberOfItemsPerCollectable; j++)
                {
                    if (collectionType == CollectableBaseFactory.BookType)
                    {
                        item = GetTestBookItem(i);
                    }
                    else if (collectionType == CollectableBaseFactory.StampType)
                    {
                        item = GetTestStampItem(i);
                    }
                    collectable.AddItem(item);
                    //ICollectableItem item = CollectableBaseFactory.CreateAndAddCollectableItem(collectable);
                    //item.ItemDetails = $"Details{j}";
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
