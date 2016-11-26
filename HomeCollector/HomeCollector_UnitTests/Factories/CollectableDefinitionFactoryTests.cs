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
    public class CollectableDefinitionFactoryTests
    {
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_null_type_throws_exception()
        {
            Type invalidType = null;

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_invalid_type_throws_exception()
        {
            Type invalidType = typeof(int);

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_ICollectableItem_interface_throws_exception()
        {
            Type invalidType = typeof(ICollectableDefinition);

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_collectable_item_from_interface_type_throws_exception()
        {
            Type invalidType = typeof(IStampDefinition);

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);

            Assert.IsFalse(true, "Expected test to fail if passed an interface instead of valid collectable definition type");
        }

        [TestMethod]
        public void create_new_definition_from_valid_collectable_definition_stamptype_succeeds()
        {
            Type validType = typeof(StampDefinition);   // implements ICollectableDefinition

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(validType);

            Assert.AreEqual(validType, newItem.ObjectType, "Expected to get instance of a stamp defintion type");
        }

        [TestMethod]
        public void create_new_definition_from_valid_collectable_definition_booktype_succeeds()
        {
            Type validType = typeof(BookDefinition);   // implements ICollectableDefinition

            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(validType);

            Assert.AreEqual(validType, newItem.ObjectType, "Expected to get instance of a book definition type");
        }

    }
}
