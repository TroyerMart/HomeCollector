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

            Assert.IsTrue(stamp.Equals(testStamp));
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

            Assert.IsFalse(stamp.Equals(testStamp));
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

            Assert.IsFalse(stamp.Equals(testStamp));
        }
        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicit_scottnumber_are_equal()
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

            Assert.IsTrue(stamp.Equals(testStamp, false));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicit_scottnumber_are_not_equal_country()
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

            Assert.IsFalse(stamp.Equals(testStamp, false));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_country_and_explicit_scottnumber_are_not_equal_scottnumber()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";

            IStampDefinition testStamp = new StampDefinition()
            { ScottNumber = "1001", Country = stamp.Country };

            Assert.IsFalse(stamp.Equals(testStamp, false));
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

            Assert.IsTrue(stamp.Equals(testStamp, true));
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

            Assert.IsFalse(stamp.Equals(testStamp, true));
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

            Assert.IsFalse(stamp.Equals(testStamp, true));
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);

            IStampDefinition testStamp = null;

            Assert.IsFalse(stamp.Equals(testStamp), "Expected test to fail when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_alternateid_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);

            IStampDefinition testStamp = null;

            Assert.IsFalse(stamp.Equals(testStamp, true), "Expected test to fail when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_definitions_by_explicit_scottnumber_returns_false_when_null()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);

            IStampDefinition testStamp = null;

            Assert.IsFalse(stamp.Equals(testStamp, false), "Expected test to fail when comparing a null definition");
        }

    }
}
