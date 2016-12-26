using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Factories;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class StampBaseTests
    {

        // test equality
        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_not_equal_scottnumber()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_not_equal_scottnumber()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                AlternateId = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_not_equal_alternateid()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                AlternateId = "1001"
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_by_alternateid_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_by_explicitly_by_scottnumber_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }

    }
}
