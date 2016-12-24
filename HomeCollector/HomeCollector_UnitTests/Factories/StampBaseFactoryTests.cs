using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class StampBaseFactoryTests
    {
        [TestMethod]
        public void create_new_stamp_item_from_factory_returns_stampbase_type()
        {
            Type stampType = CollectableBaseFactory.StampType;

            ICollectableBase newStamp = CollectableBaseFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(stampType == newStamp.CollectableType);
        }

        // test defaults
        [TestMethod]
        public void create_new_stamp_item_from_factory_country_defaults_to_usa()
        {
            Type stampType = CollectableBaseFactory.StampType;

            StampBase newStamp = (StampBase)CollectableBaseFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(newStamp.Country == StampCountryEnum.USA);
        }

        [TestMethod]
        public void create_new_stamp_item_from_factory_isPostageStamp_defaults_to_true()
        {
            Type stampType = CollectableBaseFactory.StampType;

            StampBase newStamp = (StampBase)CollectableBaseFactory.CreateCollectableItem(stampType);

            Assert.IsTrue(newStamp.IsPostageStamp);
        }

        
    }
}
