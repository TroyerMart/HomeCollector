﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using HomeCollector.Exceptions;
using System.Collections.Generic;

namespace HomeCollector_UnitTests.Factories
{
    [TestClass]
    public class BookBaseFactoryTests
    {
        // test book factory
        [TestMethod]
        public void create_new_book_from_factory_does_not_return_null()
        {
            ICollectableBase newBook = CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

            Assert.IsNotNull(newBook);
        }

        [TestMethod]
        public void create_new_book_from_factory_returns_bookbase_type()
        {
            ICollectableBase newBook = CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

            Assert.IsTrue(CollectableBaseFactory.BookType == newBook.CollectableType);
        }

        // test default values
        //[TestMethod]
        //public void create_new_book_from_factory_bookcondition_defaults_to_undefined()
        //{
        //    BookBase newBook = (BookBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

        //    Assert.IsTrue(newBook.Condition == BookConditionEnum.Undefined);
        //}

        [TestMethod]
        public void create_new_book_from_factory_getitems_does_not_return_null_list()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

            IList<ICollectableItem> bookItems = book.ItemInstances;

            Assert.IsNotNull(bookItems);
        }

        [TestMethod]
        public void create_new_book_from_factory_getitems_defaults_to_zero_items_in_list()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

            IList<ICollectableItem> bookItems = book.ItemInstances;

            Assert.AreEqual(0, bookItems.Count);
        }

        [TestMethod]
        public void create_new_book_from_factory_datepublished_defaults_to_min_date()
        {
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.BookType);

            Assert.AreEqual(0, book.Year);
            Assert.AreEqual(0, book.Month);
        }


    }
}
