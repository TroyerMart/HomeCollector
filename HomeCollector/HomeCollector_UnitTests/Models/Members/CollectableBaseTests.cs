﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HomeCollector.Interfaces;
using HomeCollector.Models.Members;
using HomeCollector.Models;
using HomeCollector.Factories;
using System.Collections.Generic;
using HomeCollector.Exceptions;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class CollectableBaseTests
    {
        // ICollectableBase tests

        // test adding items
        [TestMethod]
        public void additem_inserts_new_item_into_empty_list()
        {
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                Mock<ICollectableMember> mockItem = GetMockMember(collectable.ObjectType.Name);

                collectable.AddItem(mockItem.Object);

                IList<ICollectableMember> list = collectable.GetItems();
                Assert.AreEqual(1, list.Count);
            }
        }

        [TestMethod]
        public void additem_inserts_new_item_into_existing_list()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableMember> mockItem = GetMockMember(collectable.ObjectType.Name);

                collectable.AddItem(mockItem.Object);
                IList<ICollectableMember> list = collectable.GetItems();
                Assert.AreEqual(N + 1, list.Count);
            }
        }

        [TestMethod]
        public void additem_insert_into_empty_list_fails_for_null()
        {
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                try
                {
                    collectable.AddItem(null);
                    Assert.IsFalse(true, "Expected to fail adding a null member");
                }
                catch (CollectableException ex)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        [TestMethod]
        public void additem_fails_when_inserting_incorrect_type()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                Mock<ICollectableMember> mockItem;
                ICollectableBase collectable = GetTestBase(collectableType, N);
                if (collectable.ObjectType.Name == "BookBase")
                {
                    mockItem = GetMockMember("StampBase");
                }
                else
                {
                    mockItem = GetMockMember("BookBase");
                }

                try
                {
                    collectable.AddItem(mockItem.Object);
                    Assert.IsFalse(true, "Expected to fail adding a member of the wrong type");
                }
                catch (CollectableException ex)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        // test removing items  
        [TestMethod]
        public void removeitem_from_existing_list()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                ICollectableMember mockItem = collectable.GetItems()[N - 1];

                collectable.RemoveItem(mockItem);

                IList<ICollectableMember> list = collectable.GetItems();
                Assert.AreEqual(N - 1, list.Count);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_list_is_empty()
        {
            int N = 0;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableMember> mockItem = mockItem = GetMockMember(collectableType);

                collectable.RemoveItem(mockItem.Object);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_null()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                ICollectableMember mockItem = null;

                collectable.RemoveItem(mockItem);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_incorrect_type()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableMember> mockItem;
                if (collectable.ObjectType.Name == "BookBase")
                {
                    mockItem = GetMockMember("StampBase");
                }
                else
                {
                    mockItem = GetMockMember("BookBase");
                }
                collectable.RemoveItem(mockItem.Object);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_not_in_list()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableMember> mockItem = GetMockMember(collectable.ObjectType.Name);

                collectable.RemoveItem(mockItem.Object);
            }
        }


        // test get items list
        [TestMethod]
        public void getitems_list_has_all_items()
        {
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                Mock<ICollectableMember> mockItem1 = GetMockMember(collectable.ObjectType.Name);
                Mock<ICollectableMember> mockItem2 = GetMockMember(collectable.ObjectType.Name);
                Mock<ICollectableMember> mockItem3 = GetMockMember(collectable.ObjectType.Name);

                collectable.AddItem(mockItem1.Object);
                collectable.AddItem(mockItem2.Object);
                collectable.AddItem(mockItem3.Object);

                IList<ICollectableMember> items = collectable.GetItems();

                Assert.AreEqual(mockItem1.Object, items[0]);
                Assert.AreEqual(mockItem2.Object, items[1]);
                Assert.AreEqual(mockItem3.Object, items[2]);
            }
        }

        [TestMethod]
        public void getitems_returns_empty_list_when_there_are_no_members()
        {
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);

                IList<ICollectableMember> items = collectable.GetItems();

                Assert.AreEqual(0, items.Count);
            }
        }

        // clear members
        [TestMethod]
        public void clearitems_from_existing_list()
        {
            int N = 3;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);

                collectable.ClearItems();

                IList<ICollectableMember> list = collectable.GetItems();
                Assert.AreEqual(0, list.Count);
            }
        }

        [TestMethod]
        public void clearitems_from_empty_list()
        {
            int N = 0;
            List<String> collectableTypes = new List<string>() { "BookBase", "StampBase" };
            foreach (string collectableType in collectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);

                collectable.ClearItems();

                IList<ICollectableMember> list = collectable.GetItems();
                Assert.AreEqual(0, list.Count);
            }
        }

        /****** helpers **********************************************************************************/
        private ICollectableBase GetTestBase(string typeName, int numberOfMembers)
        {
            ICollectableBase testBase = null;
            Type itemType = null;
            switch (typeName)
            {
                case "BookBase":
                    itemType = typeof(BookItem);
                    testBase = (BookBase)CollectableBaseFactory.CreateCollectableItem(typeName);
                    break;
                case "StampBase":
                    itemType = typeof(StampItem);
                    testBase = (StampBase)CollectableBaseFactory.CreateCollectableItem(typeName);
                    break;
                default:
                    break;
            }
            for (int i=0; i<numberOfMembers; i++)
            {
                Mock<ICollectableMember> mockItem = GetMockMember(typeName);
                testBase.AddItem(mockItem.Object);
            }
            return testBase;
        }

        private Mock<ICollectableMember> GetMockMember(string typeName)
        {
            Type itemType = null;
            switch (typeName)
            {
                case "BookBase":
                    itemType = typeof(BookItem);
                    break;
                case "StampBase":
                    itemType = typeof(StampItem);
                    break;
                default:
                    itemType = null;
                    break;
            }
            Mock<ICollectableMember> mockItem = new Mock<ICollectableMember>();
            mockItem.Setup(b => b.ObjectType).Returns(itemType);

            return mockItem;
        }



    }
}