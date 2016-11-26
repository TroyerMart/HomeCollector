using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class BookDefinitionFactoryTests
    {
        [TestMethod]
        public void create_new_book_item_from_factory_returns_bookdefinition_type()
        {
            Type bookType = typeof(BookDefinition);

            ICollectableDefinition newBook = CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.IsTrue(bookType == newBook.GetType());
        }

        // test defaults
        [TestMethod]
        public void create_new_book_item_from_factory_condition_defaults_to_undefined()
        {
            Type bookType = typeof(BookDefinition);

            BookDefinition newBook = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.IsTrue(newBook.Condition == BookConditionEnum.Undefined);
        }

        // test equality
        [TestMethod]
        public void compare_book_definitions_by_isbn_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            IBookDefinition testbook = new BookDefinition()
                { ISBN = book.ISBN };

            bool isEqual = book.Equals(testbook);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_isbn_are_not_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            IBookDefinition testbook = new BookDefinition()
                { ISBN = "123-0000000000" };

            bool isEqual = book.Equals(testbook);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_explicitly_by_isbn_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            IBookDefinition testbook = new BookDefinition()
                { ISBN = book.ISBN };

            bool isEqual = book.Equals(testbook, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_explicitly_isbn_are_not_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            IBookDefinition testbook = new BookDefinition()
                { ISBN = "123-0000000000" };

            bool isEqual = book.Equals(testbook, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            IBookDefinition testbook = new BookDefinition()
            {
                Title = book.Title,
                Author = book.Author
            };

            bool isEqual = book.Equals(testbook, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_not_equal_title()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            IBookDefinition testbook = new BookDefinition()
            {
                Title = "Something Else",
                Author = book.Author
            };

            bool isEqual = book.Equals(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_not_equal_author()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            IBookDefinition testbook = new BookDefinition()
            {
                Title = book.Title,
                Author = "Lee, Stan"
            };

            bool isEqual = book.Equals(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            IBookDefinition testbook = null;

            bool isEqual = book.Equals(testbook);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_title_and_author_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            IBookDefinition testbook = null;

            bool isEqual = book.Equals(testbook, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_explicit_scottnumber_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            IBookDefinition testbook = null;

            bool isEqual = book.Equals(testbook, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }
        
    }
}
