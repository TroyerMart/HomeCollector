using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HomeCollector.Interfaces;
using HomeCollector.Models.Members;
using HomeCollector.Models;
using HomeCollector.Factories;
using System.Collections.Generic;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class CollectableBaseTests
    {
        // test adding items
        [TestMethod]
        public void additem_inserts_new_member_into_collection()
        {
            Mock<ICollectableMember> mockBookItem = new Mock<ICollectableMember>();
            mockBookItem.Setup(b => b.ObjectType).Returns(typeof(BookItem));
            Type bookType = typeof(BookBase);
            BookBase book = (BookBase)CollectableBaseFactory.CreateCollectableItem(bookType);

            book.AddItem(mockBookItem.Object);

            IList<ICollectableMember> list = book.GetItems();
            Assert.AreEqual(1, list.Count);
        }

        // test removing items - exists, doesn't exist, empty

        // test get items

    }
}
