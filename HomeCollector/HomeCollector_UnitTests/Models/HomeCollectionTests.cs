using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using Moq;
using HomeCollector.Factories;
using System.Collections.Generic;

namespace HomeCollector_UnitTests.Models
{
    [TestClass]
    public class HomeCollectionTests
    {
        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_with_null_collectionname_throws_exception()
        {
            string collectionName = null;

            ICollectionBase testCollection = new HomeCollection(collectionName, CollectableBaseFactory.CollectableTypes[0]);

            Assert.IsFalse(true, "Expected collection initialization to fail with null collection name");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_with_null_collectiontype_throws_exception()
        {
            Type collectionType = null;

            ICollectionBase testCollection = new HomeCollection("initial", collectionType);

            Assert.IsFalse(true, "Expected collection initialization to fail with null collection type");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_with_empty_collectionname_throws_exception()
        {
            string collectionName = "";

            ICollectionBase testCollection = new HomeCollection(collectionName, CollectableBaseFactory.CollectableTypes[0]);

            Assert.IsFalse(true, "Expected collection initialization to fail with empty collection name");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_invalid_collectiontype_throws_exception()
        {
            Type invalidCollectionType = typeof(Int64);    // anything that is not ICollectableBase

            ICollectionBase testCollection = new HomeCollection("initial", invalidCollectionType);

            Assert.IsFalse(true, "Expected collection initialization to fail with invalid collection type");
        }

        [TestMethod]
        public void initialize_collection_sets_name_property_successfully()
        {
            string collectionName = "Test Collection";

            ICollectionBase testCollection = new HomeCollection(collectionName, CollectableBaseFactory.CollectableTypes[0]);

            Assert.AreEqual(collectionName, testCollection.CollectionName);
        }

        [TestMethod]
        public void initialize_collection_sets_collectiontype_successfully()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = new HomeCollection("initial", collectableType);

                Assert.AreEqual(collectableType, testCollection.CollectionType);
            }            
        }

        [TestMethod]
        public void collectionname_returns_set_value()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = new HomeCollection("initial", collectableType);
                string validCollectionName = "Test Collection";

                testCollection.CollectionName = validCollectionName;

                Assert.AreEqual(validCollectionName, testCollection.CollectionName);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void set_collectionname_with_null_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", CollectableBaseFactory.CollectableTypes[0]);
            string nullCollectionName = null;

            testCollection.CollectionName = nullCollectionName;

            Assert.IsFalse(true, "Expected set collectionname to fail with null value");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void set_collectionname_with_empty_value_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", CollectableBaseFactory.CollectableTypes[0]);
            string emptyCollectionName = " ";

            testCollection.CollectionName = emptyCollectionName;

            Assert.IsFalse(true, "Expected set collectionname to fail with empty value");
        }
        [TestMethod]
        public void get_collectiontype_returns_initialized_type()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase collection = new HomeCollection("initial", collectableType);

                Assert.AreEqual(collectableType, collection.CollectionType);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_null_collectable_to_collection_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", CollectableBaseFactory.CollectableTypes[0]);

            ICollectableBase nullCollectable = null;
            testCollection.AddToCollection(nullCollectable);

            Assert.IsFalse(true, "Expected that an exception is thrown when a null value is added to the collection");
        }

        [TestMethod]
        public void add_invalid_type_to_collection_throws_exception()
        {
            bool threwException = false;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                threwException = false;
                ICollectionBase collection = new HomeCollection("initial", collectableType);
                ICollectableBase collectable = GetDifferentTypeMockCollectable(collectableType).Object; //CollectableBaseFactory.CreateCollectableBase(collectableType);

                try
                {
                    collection.AddToCollection(collectable);
                }
                catch (CollectionException)
                {
                    threwException = true;
                }

                Assert.IsTrue(threwException, "Expected that an exception is thrown when an invalid type is added to the collection");
            }
        }
        
        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_invalid_type_to_stamp_collection_throws_exception()
        {
            ICollectionBase stampCollection = new HomeCollection("initial", CollectableBaseFactory.CollectableTypes[1]);

            stampCollection.AddToCollection(new BookBase());

            Assert.IsFalse(true, "Expected that an exception is thrown when an invalid type is added to the collection");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_invalid_type_to_book_collection_throws_exception()
        {
            ICollectionBase bookCollection = new HomeCollection("initial", CollectableBaseFactory.CollectableTypes[0]);

            bookCollection.AddToCollection(new StampBase());

            Assert.IsFalse(true, "Expected that an exception is thrown when an invalid type is added to the collection");
        }

        [TestMethod]
        public void add_valid_type_to_collection_succeeds()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase collection = new HomeCollection("initial", collectableType);
                ICollectableBase collectable = GetMockCollectableObject(collectableType);

                collection.AddToCollection(collectable);

                Assert.AreEqual(1, collection.GetCollection().Count);
            }
        }

        [TestMethod]
        public void getcollection_returns_count_of_collectables_added_to_collection()
        {
            int N = 3;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetMockCollection(N, collectionType);

                int count = testCollection.GetCollection().Count;

                Assert.AreEqual(N, count);
            }
        }

        [TestMethod]
        public void getcollection_returns_all_collectables_added_to_collection()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                Type collectionType = collectableType;
                IList<ICollectableBase> collectables = new List<ICollectableBase>();
                ICollectionBase testCollection = new HomeCollection("initial", collectionType);

                for (int i = 0; i < N; i++)
                {
                    ICollectableBase collectable = GetMockCollectableObject(collectionType);
                    collectables.Add(collectable);

                    testCollection.AddToCollection(collectable);
                }
                
                for (int i=0; i<N; i++)
                {
                    Assert.AreEqual(collectables[i], testCollection.GetCollection()[i]);
                }
            }
        }

        [TestMethod]
        public void removefromcollection_deletes_collectable_from_collection()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetMockCollection(N, collectableType);
                ICollectableBase collectableToRemove = testCollection.GetCollection()[0]; //[N - 1];
                
                testCollection.RemoveFromCollection(collectableToRemove);

                bool foundCollectable = false;
                foreach (ICollectableBase collectable in testCollection.GetCollection())
                {
                    if (collectableToRemove == collectable)
                    {
                        foundCollectable = true;
                        break;
                    }
                }
                Assert.IsFalse(foundCollectable);
            }
        }

        [TestMethod]
        public void clearcollection_removes_all_collectables_from_collection()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetMockCollection(N, collectableType);

                testCollection.ClearCollection();

                Assert.AreEqual(0, testCollection.GetCollection().Count);
            }
        }

        /****** helpers **********************************************************************************/

        private Mock<ICollectableBase>  GetMockCollectable(Type collectionType)
        {
            Mock<ICollectableBase> collectable1 = new Mock<ICollectableBase>();
            collectable1.Setup(b => b.CollectableType).Returns(collectionType);
            return collectable1;
        }

        private ICollectableBase GetMockCollectableObject(Type collectionType)
        {
            return GetMockCollectable(collectionType).Object;
        }

        private Mock<ICollectableBase> GetDifferentTypeMockCollectable(Type collectableType)
        {
            Mock<ICollectableBase> mockCollectable;
            if (collectableType == CollectableBaseFactory.CollectableTypes[0])
            {
                mockCollectable = GetMockCollectable(CollectableBaseFactory.CollectableTypes[1]);
            }
            else
            {
                mockCollectable = GetMockCollectable(CollectableBaseFactory.CollectableTypes[0]);
            }
            return mockCollectable;
        }

        private ICollectionBase GetMockCollection(int N, Type collectableType)
        {
            Type collectionType = collectableType;
            ICollectionBase testCollection = new HomeCollection("initial", collectionType);
            for (int i = 0; i < N; i++)
            {
                ICollectableBase collectable = GetMockCollectableObject(collectableType);
                testCollection.AddToCollection(collectable);
            }

            return testCollection;
        }

    }
}
