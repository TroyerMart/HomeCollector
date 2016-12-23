using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using Moq;

namespace HomeCollector_UnitTests.Models
{
    [TestClass]
    public class HomeCollectionTests
    {
        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_with_null_collectionname_throws_exception()
        {
            string collectionName = null;

            ICollectionBase testCollection = new HomeCollection(collectionName, typeof(StampBase));

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

            ICollectionBase testCollection = new HomeCollection(collectionName, typeof(BookBase));

            Assert.IsFalse(true, "Expected collection initialization to fail with empty collection name");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void initialize_collection_invalid_collectiontype_throws_exception()
        {
            Type collectionType = typeof(Int64);    // anything that is not ICollectableBase

            ICollectionBase testCollection = new HomeCollection("initial", collectionType);

            Assert.IsFalse(true, "Expected collection initialization to fail with invalid collection type");
        }

        [TestMethod]
        public void initialize_collection_sets_name_property_successfully()
        {
            string collectionName = "Test Collection";

            ICollectionBase testCollection = new HomeCollection(collectionName, typeof(StampBase));

            Assert.AreEqual(collectionName, testCollection.CollectionName);
        }

        [TestMethod]
        public void initialize_collection_sets_collectiontype_successfully()
        {
            Type stampType = typeof(StampBase);
            Type bookType = typeof(BookBase);

            ICollectionBase testStampCollection = new HomeCollection("initial", stampType);
            ICollectionBase testBookCollection = new HomeCollection("initial", bookType);

            Assert.AreEqual(stampType, testStampCollection.CollectionType);
            Assert.AreEqual(bookType, testBookCollection.CollectionType);
        }

        [TestMethod]
        public void set_get_collectionname_success()
        {
            ICollectionBase testCollection = new HomeCollection("initial", typeof(StampBase));
            string validCollectionName = "Test Collection";

            testCollection.CollectionName = validCollectionName;

            Assert.AreEqual(validCollectionName, testCollection.CollectionName);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void set_collectionname_with_null_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", typeof(StampBase));
            string nullCollectionName = null;

            testCollection.CollectionName = nullCollectionName;

            Assert.IsFalse(true, "Expected set collectionname to fail with null value");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void set_collectionname_with_empty_value_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", typeof(StampBase));
            string emptyCollectionName = " ";

            testCollection.CollectionName = emptyCollectionName;

            Assert.IsFalse(true, "Expected set collectionname to fail with empty value");
        }
        [TestMethod]
        public void get_collectiontype_returns_initialized_value()
        {
            Type stampType = typeof(StampBase);
            Type bookType = typeof(BookBase);

            ICollectionBase stampCollection = new HomeCollection("initial",stampType);
            ICollectionBase bookCollection = new HomeCollection("initial", bookType);

            Assert.AreEqual(stampType, stampCollection.CollectionType);
            Assert.AreEqual(bookType, bookCollection.CollectionType);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_null_to_collection_throws_exception()
        {
            ICollectionBase testCollection = new HomeCollection("initial", typeof(BookBase));

            ICollectableBase nullCollectable = null;
            testCollection.AddToCollection(nullCollectable);

            Assert.IsFalse(true, "Expected that an exception is thrown when a null value is added to the collection");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_invalid_type_to_stamp_collection_throws_exception()
        {
            Type stampType = typeof(StampBase);
            ICollectionBase stampCollection = new HomeCollection("initial", stampType);

            stampCollection.AddToCollection(new BookBase());

            Assert.IsFalse(true, "Expected that an exception is thrown when an invalid type is added to the collection");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void add_invalid_type_to_book_collection_throws_exception()
        {
            Type bookType = typeof(BookBase);
            ICollectionBase bookCollection = new HomeCollection("initial", bookType);

            bookCollection.AddToCollection(new StampBase());

            Assert.IsFalse(true, "Expected that an exception is thrown when an invalid type is added to the collection");
        }

        [TestMethod]
        public void add_valid_type_to_collection_succeeds()
        {
            Type stampType = typeof(StampBase);
            Type bookType = typeof(BookBase);
            ICollectionBase stampCollection = new HomeCollection("initial", stampType);
            ICollectionBase bookCollection = new HomeCollection("initial", bookType);

            ICollectableBase collectableStamp = new StampBase();
            stampCollection.AddToCollection(collectableStamp);
            ICollectableBase collectableBook = new BookBase();
            bookCollection.AddToCollection(collectableBook);

            Assert.AreEqual(1, stampCollection.GetCollection().Count);
            Assert.AreEqual(1, bookCollection.GetCollection().Count);
        }

        [TestMethod]
        public void getcollection_returns_books_added_to_collection()
        {
            int N = 3;
            Type collectionType = typeof(BookBase);
            ICollectionBase testCollection = new HomeCollection("initial", collectionType);

            for (int i=0; i<N; i++)
            {
                ICollectableBase collectable = GetMockCollectableObject(collectionType);
                testCollection.AddToCollection(collectable);
            }

            Assert.AreEqual(N, testCollection.GetCollection().Count);
        }

        // test GetCollection
        // test RemoveFromCollection
        // test ClearCollection

        // Globally define StampType and BookType???  In CollectableBaseFactory

        // Helper methods

        private ICollectableBase  GetMockCollectableObject(Type collectionType)
        {
            Mock<ICollectableBase> collectable1 = new Mock<ICollectableBase>();
            collectable1.Setup(b => b.ObjectType).Returns(collectionType);
            return collectable1.Object;
        }

    }
}
