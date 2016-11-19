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

        // tests for creating new instances of collectable items
        [TestMethod]
        public void create_new_stamp_item_from_factory()
        {
            Type stampType = typeof(StampDefinition);
            ICollectableDefinition newStamp = CollectableDefinitionFactory.CreateCollectableItem(stampType);
            Assert.IsTrue(stampType == newStamp.GetType());
        }

        [TestMethod]
        public void create_new_book_item_from_factory()
        {
            Type bookType = typeof(BookDefinition);
            ICollectableDefinition newBook = CollectableDefinitionFactory.CreateCollectableItem(bookType);
            Assert.IsTrue(bookType == newBook.GetType());
        }

        // tests for default state of collectable items
        //    STAMPS
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

        // test stamp equality
        [TestMethod]
        public void compare_stamp_definitions_by_default_id_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition) CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";

            IStampDefinition testStamp = new StampDefinition()
                { ScottNumber= stamp.ScottNumber };
            
            Assert.IsTrue(stamp.Equals(testStamp));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_default_id_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.ScottNumber = "1000";

            IStampDefinition testStamp = new StampDefinition()
            { ScottNumber = stamp.ScottNumber, Country = CountryEnum.Canada };

            Assert.IsFalse(stamp.Equals(testStamp));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_default_id_are_not_equal_id()
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
        public void compare_stamp_definitions_by_alternate_id_are_equal()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";

            IStampDefinition testStamp = new StampDefinition()
            { AlternateId = stamp.AlternateId};

            Assert.IsTrue(stamp.Equals(testStamp, true));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_alternate_id_are_not_equal_country()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";

            IStampDefinition testStamp = new StampDefinition()
            { ScottNumber = stamp.AlternateId, Country = CountryEnum.Canada };

            Assert.IsFalse(stamp.Equals(testStamp, true));
        }

        [TestMethod]
        public void compare_stamp_definitions_by_alternate_id_are_not_equal_id()
        {
            Type stampType = typeof(StampDefinition);
            IStampDefinition stamp = (StampDefinition)CollectableDefinitionFactory.CreateCollectableItem(stampType);
            stamp.Country = CountryEnum.USA;
            stamp.AlternateId = "1000";

            IStampDefinition testStamp = new StampDefinition()
            { AlternateId = "1001", Country = stamp.Country };

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

        //    BOOKS
        [TestMethod]
        public void create_new_book_item_from_factory_country_defaults_to_undefined()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition newBook = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            Assert.IsTrue(newBook.Condition == BookConditionEnum.Undefined);
        }

    }
}
