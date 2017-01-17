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
        public void create_new_stamp_from_factory_does_not_return_null()
        {
            ICollectableBase newStamp = CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            Assert.IsNotNull(newStamp);
        }

        [TestMethod]
        public void create_new_stamp_from_factory_returns_stampbase_type()
        {
            Type stampType = CollectableBaseFactory.StampType;

            ICollectableBase newStamp = CollectableBaseFactory.CreateCollectableBase(stampType);

            Assert.IsTrue(stampType == newStamp.CollectableType);
        }

        // test defaults
        [TestMethod]
        public void create_new_stamp_from_factory_country_set_to_country_default()
        {
            Type stampType = CollectableBaseFactory.StampType;

            StampBase newStamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(stampType);

            Assert.IsTrue(newStamp.Country == StampBase.COUNTRY_DEFAULT);
        }

        [TestMethod]
        public void create_new_stamp_from_factory_isPostageStamp_defaults_to_true()
        {
            Type stampType = CollectableBaseFactory.StampType;

            StampBase newStamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(stampType);

            Assert.IsTrue(newStamp.IsPostageStamp);
        }

        
    }
}
