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

            Assert.IsTrue(stampType == newStamp.GetType());
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

        // test equality
        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.Equals(testStamp);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.Equals(testStamp);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_scottnumber_are_not_equal_scottnumber()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.Equals(testStamp);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.Equals(testStamp, false);
            
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.Equals(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicitly_by_scottnumber_are_not_equal_scottnumber()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";
            IStampDefinition testStamp = new StampDefinition()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.Equals(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                AlternateId = stamp.AlternateId
            };

            bool isEqual = stamp.Equals(testStamp, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = stamp.AlternateId
            };

            bool isEqual = stamp.Equals(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_alternateid_are_not_equal_alternateid()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";
            IStampDefinition testStamp = new StampDefinition()
            {
                Country = stamp.Country,
                AlternateId = "1001"
            };

            bool isEqual = stamp.Equals(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            IStampDefinition testStamp = null;

            bool isEqual = stamp.Equals(testStamp);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_alternateid_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            IStampDefinition testStamp = null;

            bool isEqual = stamp.Equals(testStamp, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_explicitly_by_scottnumber_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            IStampDefinition testStamp = null;

            bool isEqual = stamp.Equals(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

    }
}
