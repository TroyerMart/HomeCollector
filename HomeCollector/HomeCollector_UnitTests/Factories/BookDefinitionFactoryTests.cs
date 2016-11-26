using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;
using System.Collections.Generic;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class BookDefinitionFactoryTests
    {
        // test book factory
        [TestMethod]
        public void create_new_book_item_from_factory_does_not_return_null()
        {
            Type bookType = typeof(BookDefinition);

            ICollectableDefinition newBook = CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.IsNotNull(newBook);
        }

        [TestMethod]
        public void create_new_book_item_from_factory_returns_bookdefinition_type()
        {
            Type bookType = typeof(BookDefinition);

            ICollectableDefinition newBook = CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.IsTrue(bookType == newBook.ObjectType);
        }

        // test default values
        [TestMethod]
        public void create_new_book_item_from_factory_bookcondition_defaults_to_undefined()
        {
            Type bookType = typeof(BookDefinition);

            BookDefinition newBook = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.IsTrue(newBook.Condition == BookConditionEnum.Undefined);
        }

        [TestMethod]
        public void create_new_book_item_from_factory_getitems_does_not_return_null_list()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            IList<ICollectionMember> books = book.GetItems();

            Assert.IsNotNull(books);
        }

        [TestMethod]
        public void create_new_book_item_from_factory_getitems_defaults_to_zero_items_in_list()
        {
            Type bookType = typeof(BookDefinition);
            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            IList<ICollectionMember> books = book.GetItems();

            Assert.AreEqual(0, books.Count);
        }

        [TestMethod]
        public void create_new_book_item_from_factory_datepublished_defaults_to_min_date()
        {
            Type bookType = typeof(BookDefinition);

            BookDefinition book = (BookDefinition)CollectableDefinitionFactory.CreateCollectableItem(bookType);

            Assert.AreEqual(DateTime.MinValue, book.DatePublished);
        }


    }
}
