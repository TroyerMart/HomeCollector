﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models.Members;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;
using HomeCollector.Factories;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class BookItemTests
    {
        // test defaults
        [TestMethod]
        public void estimated_value_defaults_to_zero_for_new_item()
        {
            decimal expectedValue = 0;

            BookItem book = new BookItem();
            decimal defaultValue = book.EstimatedValue;

            Assert.AreEqual(expectedValue, defaultValue);
        }

        [TestMethod]
        public void isfavorite_defaults_to_false_for_new_item()
        {
            bool expectedValue = false;

            BookItem book = new BookItem();
            bool defaultValue = book.IsFavorite;

            Assert.AreEqual(expectedValue, defaultValue);
        }

        [TestMethod]
        public void bookcondition_value_defaults_to_undefined_for_new_item()
        {
            BookConditionEnum expectedValue = BookConditionEnum.Undefined;

            BookItem book = new BookItem();
            BookConditionEnum defaultValue = book.Condition;

            Assert.AreEqual(expectedValue, defaultValue);
        }

        // test property validations
        [TestMethod]
        public void estimated_value_allows_zero_value_to_be_set()
        {
            decimal expectedValue = 0;
            BookItem book = new BookItem();

            book.EstimatedValue = expectedValue;

            Assert.AreEqual(expectedValue, book.EstimatedValue);
        }

        [TestMethod]
        public void estimated_value_allows_positive_value_to_be_set()
        {
            decimal expectedValue = 0.55M;
            BookItem book = new BookItem();

            book.EstimatedValue = expectedValue;

            Assert.AreEqual(expectedValue, book.EstimatedValue);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void estimated_value_set_to_negative_value_throws_exception()
        {
            decimal expectedValue = -10M;
            BookItem book = new BookItem();

            book.EstimatedValue = expectedValue;

            Assert.IsFalse(true, "Expected test to throw exception when setting a negative value");
        }

        [TestMethod]
        public void new_instance_of_bootitem_has_correct_type()
        {
            BookItem book = new BookItem();

            Type bookType = book.CollectableType;

            Assert.AreEqual(CollectableBaseFactory.BookType, bookType);
        }
    }
}
