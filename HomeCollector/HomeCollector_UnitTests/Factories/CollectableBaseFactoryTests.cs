using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using HomeCollector.Factories;
using HomeCollector.Models;
using HomeCollector.Exceptions;
using Moq;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class CollectableBaseFactoryTests
    {
        // factory tests based on explicit types
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_null_type_throws_exception()
        {
            Type invalidType = null;

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_invalid_type_throws_exception()
        {
            Type invalidType = typeof(int);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_ICollectableItem_interface_throws_exception()
        {
            Type invalidType = typeof(ICollectableBase);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_interface_type_throws_exception()
        {
            Type invalidType = typeof(IStampBase);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an interface instead of valid collectable base type");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_succeeds()
        {
            foreach (Type validType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase newCollectable = CollectableBaseFactory.CreateCollectableBase(validType);

                Assert.AreEqual(validType, newCollectable.CollectableType, $"Expected to get instance of a {validType.Name} base type");
            }
        }

        // factory tests based on type names
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_null_string_throws_exception()
        {
            string invalidTypeName = null;

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_empty_string_throws_exception()
        {
            string invalidTypeName = "";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_invalid_string_throws_exception()
        {
            string invalidTypeName = "unknown";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type name");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_interface_type_string_throws_exception()
        {
            string validTypeName = "ICollectableBase";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(validTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an interface type name");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_name_succeeds()
        {
            foreach (Type validType in CollectableBaseFactory.CollectableTypes)
            {
                string validTypeName = validType.Name;   // implements ICollectableBase

                ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(validTypeName);

                Assert.AreEqual(validTypeName, newItem.CollectableType.Name, $"Expected to get instance of a {validTypeName} base type");
            }
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_name_case_insensitive_succeeds()
        {
            string validTypeName = "BOOkBaSe";   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableBase(validTypeName);

            Assert.AreEqual(validTypeName.ToUpper(), newItem.CollectableType.Name.ToUpper(), "Expected to get instance of a book base type");
        }

        // IsCollectableType tests
        [TestMethod]
        public void iscollectabletype_null_type_returns_false()
        {
            Type invalidType = null;

            bool isCollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isCollectable, "Expected to return false if passed a null type");
        }

        [TestMethod]
        public void iscollectabletype_invalid_type_returns_false()
        {
            Type invalidType = typeof(int);

            bool isCollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isCollectable, "Expected to return false if passed an non-ICollectable type");
        }

        [TestMethod]
        public void iscollectabletype_interface_type_returns_false()
        {
            Type invalidType = typeof(ICollectableBase);

            bool isCollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isCollectable, "Expected to return false if passed the ICollectable interface type");
        }

        [TestMethod]
        public void iscollectabletype_valid_type_returns_true()
        {
            foreach (Type validType in CollectableBaseFactory.CollectableTypes)
            {
                bool isCollectable = CollectableBaseFactory.IsCollectableType(validType);

                Assert.IsTrue(isCollectable, "Expected to return true if passed a ICollectable type");
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void createcollectableitem_null_type_throws_exception()
        {
            //Type collectableType = CollectableBaseFactory.CollectableTypes[0];
            //ICollectableBase collectable = null; // CollectableBaseFactory.CreateCollectableBase(collectableType);
            Type nullType = null;

            CollectableBaseFactory.CreateCollectableItem(nullType);

            Assert.Fail("Expected CreateCollectableItem to fail when passed a null collectable type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void createcollectableitem_invalid_type_throws_exception()
        {
            Type collectableType = typeof(double);

            CollectableBaseFactory.CreateCollectableItem(collectableType);

            Assert.Fail("Expected CreateCollectableItem to fail when passed an invalid collectable type");
        }

        [TestMethod]
        public void createcollectableitem_valid_type_returns_new_instance()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableItem newItem = CollectableBaseFactory.CreateCollectableItem(collectableType);

                Assert.IsNotNull(newItem);
            }
        }

        [TestMethod]
        public void createcollectableitem_valid_type_returns_new_instance_with_type()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableItem newItem = CollectableBaseFactory.CreateCollectableItem(collectableType);

                Assert.AreEqual(collectableType, newItem.CollectableType);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void createandaddcollectableitem_null_collectable_throws_exception()
        {
            ICollectableBase nullCollectable = null; 

            CollectableBaseFactory.CreateAndAddCollectableItem(nullCollectable);

            Assert.Fail("Expected CreateAndAddCollectableItem to fail when passed a null collectable");
        }

        [TestMethod]
        public void createandaddcollectableitem_valid_type_returns_new_item_with_type_set()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = CollectableBaseFactory.CreateCollectableBase(collectableType);
                ICollectableItem newItem = CollectableBaseFactory.CreateAndAddCollectableItem(collectable);

                Assert.AreEqual(collectableType, newItem.CollectableType);
            }
        }

        [TestMethod]
        public void createandaddcollectableitem_valid_type_adds_new_item_to_collectable()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = CollectableBaseFactory.CreateCollectableBase(collectableType);
                ICollectableItem newItem = CollectableBaseFactory.CreateAndAddCollectableItem(collectable);
                ICollectableItem fromCollectable = collectable.ItemInstances[0];

                Assert.AreEqual(newItem, fromCollectable);
            }
        }

        [TestMethod]
        public void gettypefromfullname_returns_collectable_type_successfully()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase collection = new HomeCollection("name", collectableType);
                string fullName = collection.CollectionType.FullName;

                Type collectionType = CollectableBaseFactory.GetTypeFromFullName(fullName);

                Assert.AreEqual(collectableType, collectionType);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void gettypefromfullname_invalid_name_throws_exception()
        {
            string invalidName = "bad name";

            Type collectionType = CollectableBaseFactory.GetTypeFromFullName(invalidName);

            Assert.Fail("Expected GetTypeFromFullName to fail when unable to parse type from name");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void gettypefromfullname_null_name_throws_exception()
        {
            string invalidName = null;

            Type collectionType = CollectableBaseFactory.GetTypeFromFullName(invalidName);

            Assert.Fail("Expected GetTypeFromFullName to fail when unable to parse type from name");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void gettypefromfullname_blank_name_throws_exception()
        {
            string invalidName = "";

            Type collectionType = CollectableBaseFactory.GetTypeFromFullName(invalidName);

            Assert.Fail("Expected GetTypeFromFullName to fail when unable to parse type from name");
        }

    }
}
