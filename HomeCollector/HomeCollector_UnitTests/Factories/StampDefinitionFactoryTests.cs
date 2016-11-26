using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class StampDefinitionFactoryTests
    {
        [TestMethod]
        public void create_new_stamp_item_from_factory_returns_stampdefinition_type()
        {
            Type stampType = typeof(StampDefinition);

            ICollectableDefinition newStamp = CollectableDefinitionFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(stampType == newStamp.ObjectType);
        }

        // test defaults
        [TestMethod]
        public void create_new_stamp_item_from_factory_country_defaults_to_usa()
        {
            Type stampType = typeof(StampDefinition);

            StampDefinition newStamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(newStamp.Country == CountryEnum.USA);
        }

        [TestMethod]
        public void create_new_stamp_item_from_factory_isPostageStamp_defaults_to_true()
        {
            Type stampType = typeof(StampDefinition);

            StampDefinition newStamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(newStamp.IsPostageStamp);
        }

        
    }
}
