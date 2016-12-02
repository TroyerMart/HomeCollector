using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Factories;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class StampDefinitionTests
    {

        // test equality
        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_not_equal_scottnumber()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_not_equal_scottnumber()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.ScottNumber = "1000";
            StampDefinition testStamp = new StampDefinition()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                AlternateId = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = StampCountryEnum.Canada,
                ScottNumber = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_not_equal_alternateid()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = StampCountryEnum.USA;
            stamp.AlternateId = "1000";
            StampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                AlternateId = "1001"
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            StampDefinition testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_alternateid_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            StampDefinition testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_explicitly_by_scottnumber_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            StampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            StampDefinition testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

    }
}
