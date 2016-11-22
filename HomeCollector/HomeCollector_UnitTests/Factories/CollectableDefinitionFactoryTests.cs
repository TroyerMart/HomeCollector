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
        public void create_new_item_from_null_type_fails()
        {
            Type invalidType = null;
            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);
            Assert.IsFalse(true, "Expected test to fail if passed a null type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_item_from_invalid_type_fails()
        {
            Type invalidType = typeof(int);
            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);
            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_item_from_ICollectableItem_type_fails()
        {
            Type invalidType = typeof(ICollectableDefinition);
            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);
            Assert.IsFalse(true, "Expected test to fail if passed an invalid type");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void create_new_item_from_interface_type_fails()
        {
            Type invalidType = typeof(IStampDefinition);
            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(invalidType);
            Assert.IsFalse(true, "Expected test to fail if passed an interface instead of valid object type");
        }


    }
}
