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
        public void create_new_collectable_item_from_null_type_throws_exception()
        {
            Type invalidType = null;

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_invalid_type_throws_exception()
        {
            Type invalidType = typeof(int);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_ICollectableItem_interface_throws_exception()
        {
            Type invalidType = typeof(ICollectableBase);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_interface_type_throws_exception()
        {
            Type invalidType = typeof(IStampBase);

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an interface instead of valid collectable base type");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_stamptype_succeeds()
        {
            Type validType = CollectableBaseFactory.StampType;   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validType);

            Assert.AreEqual(validType, newItem.CollectableType, "Expected to get instance of a stamp base type");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_booktype_succeeds()
        {
            Type validType = CollectableBaseFactory.BookType;   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validType);

            Assert.AreEqual(validType, newItem.CollectableType, "Expected to get instance of a book base type");
        }


        // factory tests based on type names
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_null_string_throws_exception()
        {
            string invalidTypeName = null;

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_empty_string_throws_exception()
        {
            string invalidTypeName = "";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_invalid_string_throws_exception()
        {
            string invalidTypeName = "unknown";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type name");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_interface_type_string_throws_exception()
        {
            string validTypeName = "ICollectableBase";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an interface type name");
        }
        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_stamp_name_succeeds()
        {
            string validTypeName = "StampBase";   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

            Assert.AreEqual(validTypeName, newItem.CollectableType.Name, "Expected to get instance of a stamp base type");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_book_name_succeeds()
        {
            string validTypeName = "BookBase";   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

            Assert.AreEqual(validTypeName, newItem.CollectableType.Name, "Expected to get instance of a book base type");
        }
        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_name_case_insensitive_succeeds()
        {
            string validTypeName = "BOOkBaSe";   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

            Assert.AreEqual(validTypeName.ToUpper(), newItem.CollectableType.Name.ToUpper(), "Expected to get instance of a book base type");
        }

        // IsICollectableType tests
        [TestMethod]
        public void isicollectabletype_null_type_returns_false()
        {
            Type invalidType = null;

            bool isICollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isICollectable, "Expected to return false if passed a null type");
        }

        [TestMethod]
        public void isicollectabletype_invalid_type_returns_false()
        {
            Type invalidType = typeof(int);

            bool isICollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isICollectable, "Expected to return false if passed an non-ICollectable type");
        }

        [TestMethod]
        public void isicollectabletype_interface_type_returns_false()
        {
            Type invalidType = typeof(ICollectableBase);

            bool isICollectable = CollectableBaseFactory.IsCollectableType(invalidType);

            Assert.IsFalse(isICollectable, "Expected to return false if passed the ICollectable interface type");
        }

        [TestMethod]
        public void isicollectabletype_valid_type_returns_true()
        {
            Type validType = CollectableBaseFactory.StampType;

            bool isICollectable = CollectableBaseFactory.IsCollectableType(validType);

            Assert.IsTrue(isICollectable, "Expected to return true if passed a ICollectable type");
        }

    }
}
