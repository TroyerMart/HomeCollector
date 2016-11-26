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
    public class BookDefinitionTests
    {
        // test validations
        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void display_name_cannot_be_set_to_null()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            book.DisplayName = null;

            Assert.IsFalse(true, "Expected an exception to be thrown if a null display name is set");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void display_name_cannot_be_set_to_blank_string()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            book.DisplayName = "";

            Assert.IsFalse(true, "Expected an exception to be thrown if a blank display name is set");
        }

        [TestMethod]
        public void display_name_set_to_nonblank_value()
        {
            string expectedValue = "Display Name";
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            book.DisplayName = expectedValue;

            Assert.AreEqual(expectedValue, book.DisplayName);
        }

        // test equality
        [TestMethod]
        public void compare_book_definitions_by_isbn_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            BookDefinition testbook = new BookDefinition()
            { ISBN = book.ISBN };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_isbn_are_not_equal()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            BookDefinition testbook = new BookDefinition()
            { ISBN = "123-0000000000" };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_explicitly_by_isbn_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            BookDefinition testbook = new BookDefinition()
            { ISBN = book.ISBN };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_explicitly_isbn_are_not_equal()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.ISBN = "978-0465002047";
            BookDefinition testbook = new BookDefinition()
            { ISBN = "123-0000000000" };

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_equal()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookDefinition testbook = new BookDefinition()
            {
                Title = book.Title,
                Author = book.Author
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_not_equal_title()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookDefinition testbook = new BookDefinition()
            {
                DisplayName = "Something Else",
                Author = book.Author
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_book_definitions_by_title_and_author_are_not_equal_author()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            book.Title = "Pebble in the Sky";
            book.Author = "Asimov, Isaac";

            BookDefinition testbook = new BookDefinition()
            {
                DisplayName = book.Title,
                Author = "Lee, Stan"
            };

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            BookDefinition testbook = null;

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_title_and_author_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            BookDefinition testbook = null;

            bool isEqual = book.IsSame(testbook, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_book_definitions_by_explicit_scottnumber_returns_false_when_null()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);
            BookDefinition testbook = null;

            bool isEqual = book.IsSame(testbook, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null definition");
        }

        // test adding items
        [TestMethod]
        public void additem_inserts_new_member_into_collection()
        {   
            Mock<ICollectionMember> mockBookItem = new Mock<ICollectionMember>();
            mockBookItem.Setup(b => b.ObjectType).Returns(typeof(BookItem));
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            book.AddItem(mockBookItem.Object);
            
            IList<ICollectionMember> list = book.GetItems();
            Assert.AreEqual(1, list.Count);
        }

        // test removing items - exists, doesn't exist, empty

        // test get items


    }
}
