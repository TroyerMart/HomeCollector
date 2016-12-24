using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Exceptions;
using HomeCollector.Factories;
using System.Collections.Generic;
using HomeCollector.Interfaces;
using Moq;
using HomeCollector.Models.Members;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class BookBaseTests
    {
        // test validations
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void display_name_cannot_be_set_to_null()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);

            book.DisplayName = null;

            Assert.IsFalse(true, "Expected an exception to be thrown if a null display name is set");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void display_name_cannot_be_set_to_blank_string()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);

            book.DisplayName = "";

            Assert.IsFalse(true, "Expected an exception to be thrown if a blank display name is set");
        }

        [TestMethod]
        public void display_name_set_to_nonblank_value()
        {
            string expectedValue = "Display Name";
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);

            book.DisplayName = expectedValue;

            Assert.AreEqual(expectedValue, book.DisplayName);
        }

        [TestMethod]
        public void book_year_can_be_set_to_zero_successfully()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);

            book.Year = 0;

            Assert.AreEqual(0, book.Year);
        }

        [TestMethod]
        public void book_year_can_be_set_to_valid_year_successfully()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            int testYear = 2015;

            book.Year = testYear;

            Assert.AreEqual(testYear, book.Year);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void book_year_cannot_be_set_to_negative()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);

            book.Year = -100;

            Assert.IsFalse(true, "Expected an exception to be thrown if a negative year is set");
        }

        // test equality
        [TestMethod]
        public void compare_book_base_instances_by_isbn_are_equal()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.ISBN = "978-0465002047";
            BookBase testbook = new BookBase()
            { ISBN = book.ISBN };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_by_isbn_are_not_equal()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.ISBN = "978-0465002047";
            BookBase testbook = new BookBase()
            { ISBN = "123-0000000000" };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_explicitly_by_isbn_are_equal()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.ISBN = "978-0465002047";
            BookBase testbook = new BookBase()
            { ISBN = book.ISBN };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_explicitly_isbn_are_not_equal()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.ISBN = "978-0465002047";
            BookBase testbook = new BookBase()
            { ISBN = "123-0000000000" };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_by_title_and_author_are_equal()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookBase testbook = new BookBase()
            {
                Title = book.Title,
                Author = book.Author
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_by_title_and_author_are_not_equal_title()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookBase testbook = new BookBase()
            {
                DisplayName = "Something Else",
                Author = book.Author
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_base_instances_by_title_and_author_are_not_equal_author()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookBase testbook = new BookBase()
            {
                DisplayName = book.Title,
                Author = "Lee, Stan"
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_base_instances_returns_false_when_null()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            BookBase testbook = null;

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_base_instances_by_title_and_author_returns_false_when_null()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            BookBase testbook = null;

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_base_instances_by_explicit_scottnumber_returns_false_when_null()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(CollectableBaseFactory.BookType);
            BookBase testbook = null;

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }

       //display name defaults

    }
}
