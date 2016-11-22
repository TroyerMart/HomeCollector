using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;

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
            book.ISBN = "";

            IBookDefinition testbook = new BookDefinition()
                { ISBN = book.ISBN };

            Assert.IsTrue(book.Equals(testbook));
        }


        // need to arrange all tests to Arrange Act  Assert - in books and stamps!

        [TestMethod]
        public void compare_book_definitions_by_country_and_scottnumber_are_not_equal_country()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.ScottNumber = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = book.ScottNumber
            };

            Assert.IsFalse(book.Equals(testbook));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_scottnumber_are_not_equal_scottnumber()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.ScottNumber = "1000";

            IBookDefinition testbook = new BookDefinition()
            { ScottNumber = "1001", Country = book.Country };

            Assert.IsFalse(book.Equals(testbook));
        }
        [TestMethod]
        public void compare_book_definitions_by_country_and_explicit_scottnumber_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.ScottNumber = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = book.Country,
                ScottNumber = book.ScottNumber
            };

            Assert.IsTrue(book.Equals(testbook, false));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_explicit_scottnumber_are_not_equal_country()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.ScottNumber = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = book.ScottNumber
            };

            Assert.IsFalse(book.Equals(testbook, false));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_explicit_scottnumber_are_not_equal_scottnumber()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.ScottNumber = "1000";

            IBookDefinition testbook = new BookDefinition()
            { ScottNumber = "1001", Country = book.Country };

            Assert.IsFalse(book.Equals(testbook, false));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_alternateid_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.AlternateId = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = book.Country,
                AlternateId = book.AlternateId
            };

            Assert.IsTrue(book.Equals(testbook, true));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_alternateid_are_not_equal_country()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.AlternateId = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = CountryEnum.Canada,
                ScottNumber = book.AlternateId
            };

            Assert.IsFalse(book.Equals(testbook, true));
        }

        [TestMethod]
        public void compare_book_definitions_by_country_and_alternateid_are_not_equal_alternateid()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Country = CountryEnum.USA;
            book.AlternateId = "1000";

            IBookDefinition testbook = new BookDefinition()
            {
                Country = book.Country,
                AlternateId = "1001"
            };

            Assert.IsFalse(book.Equals(testbook, true));
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            IBookDefinition testbook = null;

            Assert.IsFalse(book.Equals(testbook), "Expected test to fail when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_alternateid_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            IBookDefinition testbook = null;

            Assert.IsFalse(book.Equals(testbook, true), "Expected test to fail when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_explicit_scottnumber_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            IBookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            IBookDefinition testbook = null;

            Assert.IsFalse(book.Equals(testbook, false), "Expected test to fail when comparing a null definition");
        }


        // test equality

    }
}
