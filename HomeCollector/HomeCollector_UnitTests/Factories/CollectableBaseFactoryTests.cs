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

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_empty_string_throws_exception()
        {
            string invalidTypeName = "";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_invalid_string_throws_exception()
        {
            string invalidTypeName = "unknown";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(invalidTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type name");
        }
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_from_interface_type_string_throws_exception()
        {
            string validTypeName = "ICollectableBase";

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

            Assert.IsFalse(true, "Expected test to fail if passed an interface type name");
        }

        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_name_succeeds()
        {
            foreach (Type validType in CollectableBaseFactory.CollectableTypes)
            {
                string validTypeName = validType.Name;   // implements ICollectableBase

                ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

                Assert.AreEqual(validTypeName, newItem.CollectableType.Name, $"Expected to get instance of a {validTypeName} base type");
            }
        }
                
        [TestMethod]
        public void create_new_factory_instance_from_valid_collectable_base_name_case_insensitive_succeeds()
        {
            string validTypeName = "BOOkBaSe";   // implements ICollectableBase

            ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(validTypeName);

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


    }
}
