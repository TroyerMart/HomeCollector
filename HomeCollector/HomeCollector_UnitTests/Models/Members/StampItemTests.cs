using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models.Members;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Factories;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class StampItemTests
    {
        // test defaults
        [TestMethod]
        public void estimated_value_defaults_to_zero_for_new_item()
        {
            StampItem stamp = new StampItem();

            decimal defaultValue = stamp.EstimatedValue;

            Assert.AreEqual(0, defaultValue);
        }

        [TestMethod]
        public void isfavorite_defaults_to_false_for_new_item()
        {
            StampItem stamp = new StampItem();

            bool defaultValue = stamp.IsFavorite;

            Assert.IsFalse(defaultValue);
        }

        [TestMethod]
        public void ismintcondition_value_defaults_to_false_for_new_item()
        {
            StampItem stamp = new StampItem();

            bool defaultValue = stamp.IsMintCondition;

            Assert.IsFalse(defaultValue);
        }

        [TestMethod]
        public void stampcondition_value_defaults_to_undefined_for_new_item()
        {
            StampItem stamp = new StampItem();

            string defaultValue = stamp.Condition;

            Assert.AreEqual(StampItem.CONDITION_DEFAULT, defaultValue);
        }


        // test property validations
        [TestMethod]
        public void estimated_value_allows_zero_value_to_be_set()
        {
            StampItem stamp = new StampItem();

            stamp.EstimatedValue = 0;
            
            Assert.AreEqual(0, stamp.EstimatedValue);
        }

        [TestMethod]
        public void estimated_value_allows_positive_value_to_be_set()
        {
            StampItem stamp = new StampItem();
            decimal value = 0.55M;

            stamp.EstimatedValue = value;

            Assert.AreEqual(value, stamp.EstimatedValue);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void estimated_value_set_to_negative_value_throws_exception()
        {
            StampItem stamp = new StampItem();
            decimal value = -10M;

            stamp.EstimatedValue = value;

            Assert.IsFalse(true, "Expected test to throw exception when setting a negative value");
        }

        [TestMethod]
        public void new_instance_of_bootitem_has_correct_type()
        {
            StampItem stamp = new StampItem();

            Type stampType = stamp.CollectableType;

            Assert.AreEqual(CollectableBaseFactory.StampType, stampType);
        }

        // test defaults - condition
        [TestMethod]
        public void validatecondition_returns_false_when_passed_null_condition()
        {
            string invalidCondition = null;

            bool isValid = StampItem.ValidateCondition(invalidCondition);

            Assert.IsFalse(isValid, $"Expected ValidateCondition to return false an unexpected condition: {invalidCondition}");
        }

        [TestMethod]
        public void validatecondition_returns_false_when_passed_unexpected_condition()
        {
            string invalidCondition = "invalid";

            bool isValid = StampItem.ValidateCondition(invalidCondition);

            Assert.IsFalse(isValid, $"Expected ValidateCondition to return false an unexpected condition: {invalidCondition}");
        }

        [TestMethod]
        public void validate_stamp_conditions_set_successfully()
        {
            foreach (string condition in StampItem.STAMP_CONDITIONS)
            {
                bool isValid = StampItem.ValidateCondition(condition);

                Assert.IsTrue(isValid);
            }
        }
    }
}
