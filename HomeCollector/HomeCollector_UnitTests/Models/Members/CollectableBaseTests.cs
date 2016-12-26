using System;
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

        [TestInitialize]
        public void Initialize()
        {
        }

        // test adding items
        [TestMethod]
        public void additem_inserts_new_item_into_empty_list()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                Mock<ICollectableItem> mockItem = GetMockItem(collectableType);

                collectable.AddItem(mockItem.Object);

                IList<ICollectableItem> list = collectable.GetItems();
                Assert.AreEqual(1, list.Count);
            }
        }

        [TestMethod]
        public void additem_inserts_new_item_into_existing_list()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItem = GetMockItem(collectableType);

                collectable.AddItem(mockItem.Object);
                IList<ICollectableItem> list = collectable.GetItems();
                Assert.AreEqual(N + 1, list.Count);
            }
        }

        [TestMethod]
        public void additem_insert_into_empty_list_fails_for_null()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                try
                {
                    collectable.AddItem(null);
                    Assert.IsFalse(true, "Expected to fail adding a null member");
                }
                catch (CollectableException)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        [TestMethod]
        public void additem_fails_when_inserting_incorrect_type()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                Mock<ICollectableItem> mockWrongItemType;
                ICollectableBase collectable = GetTestBase(collectableType, N);
                mockWrongItemType = GetDifferentTypeMockItem(collectableType);

                try
                {
                    collectable.AddItem(mockWrongItemType.Object);

                    Assert.IsFalse(true, "Expected to fail adding a member of the wrong type");
                }
                catch (CollectableException)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        // test removing items  
        [TestMethod]
        public void removeitem_from_existing_list_success()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                ICollectableItem mockItem = collectable.GetItems()[N - 1];

                collectable.RemoveItem(mockItem);

                IList<ICollectableItem> list = collectable.GetItems();
                Assert.AreEqual(N - 1, list.Count);
            }
        }

        [TestMethod]
        public void removeitem_from_existing_list_fails_when_list_is_empty()
        {
            int N = 0;
            bool threwError = false;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItem =  GetMockItem(collectableType);
                threwError = false;

                try
                {
                    collectable.RemoveItem(mockItem.Object);
                }
                catch (CollectableException)
                {
                    threwError = true;
                }

                Assert.IsTrue(threwError, "Expected RemoveItem to throw a CollectableException error when removing an item from an empty item list");
            }
        }

        [TestMethod]
        public void removeitem_from_existing_list_fails_when_item_is_null()
        {
            int N = 3;
            bool threwError = false;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                ICollectableItem mockItem = null;

                try
                {
                    collectable.RemoveItem(mockItem);
                }
                catch (CollectableException)
                {
                    threwError = true;
                }

                Assert.IsTrue(threwError);
            }
        }

        [TestMethod]
        public void removeitem_from_existing_list_fails_when_item_is_incorrect_type()
        {
            int N = 3;
            bool threwError = false;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockWrongItemType;
                mockWrongItemType = GetDifferentTypeMockItem(collectableType);

                try
                {
                    collectable.RemoveItem(mockWrongItemType.Object);
                }
                catch (CollectableException)
                {
                    threwError = true;
                }

                Assert.IsTrue(threwError);
            }
        }

        [TestMethod]
        public void removeitem_from_existing_list_fails_when_item_is_not_in_list()
        {
            int N = 3;
            bool threwError = false;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                threwError = false;
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItemNotInList = GetMockItem(collectableType);

                try
                {
                    collectable.RemoveItem(mockItemNotInList.Object);
                }
                catch (CollectableException)
                {
                    threwError = true;
                }

                Assert.IsTrue(threwError);
            }
        }


        // test get items list
        [TestMethod]
        public void getitems_list_has_all_items()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                int N = 3;
                List<Mock<ICollectableItem>> mockItems = new List<Mock<ICollectableItem>>();
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                for (int i=0; i<N; i++)
                {
                    Mock<ICollectableItem> mockItem = GetMockItem(collectableType);
                    mockItems.Add(mockItem);
                    collectable.AddItem(mockItem.Object);
                }

                IList<ICollectableItem> items = collectable.GetItems();

                for (int i = 0; i < N; i++)
                {
                    Assert.AreEqual(mockItems[i].Object, items[i]);
                }
            }
        }

        [TestMethod]
        public void getitems_returns_empty_list_when_there_are_no_members()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);

                IList<ICollectableItem> items = collectable.GetItems();

                Assert.AreEqual(0, items.Count);
            }
        }

        // clear members
        [TestMethod]
        public void clearitems_from_existing_list_success()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);

                collectable.ClearItems();

                IList<ICollectableItem> list = collectable.GetItems();
                Assert.AreEqual(0, list.Count);
            }
        }

        [TestMethod]
        public void clearitems_from_empty_list_success()
        {
            int N = 0;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);

                collectable.ClearItems();

                IList<ICollectableItem> list = collectable.GetItems();
                Assert.AreEqual(0, list.Count);
            }
        }

        /****** helpers **********************************************************************************/
        private ICollectableBase GetTestBase(Type baseType, int numberOfMembers)
        {
            ICollectableBase testBase = null;
            switch (baseType.Name)
            {
                case "BookBase":
                    testBase = (BookBase)CollectableBaseFactory.CreateCollectableBase(baseType);
                    break;
                case "StampBase":
                    testBase = (StampBase)CollectableBaseFactory.CreateCollectableBase(baseType);
                    break;
                default:
                    break;
            }
            for (int i=0; i<numberOfMembers; i++)
            {
                Mock<ICollectableItem> mockItem = GetMockItem(baseType);
                testBase.AddItem(mockItem.Object);
            }
            return testBase;
        }

        private Mock<ICollectableItem> GetMockItem(Type baseType)
        {
            Type itemType = null;
            switch (baseType.Name)
            {
                case "BookBase":
                    itemType = baseType; //CollectableBaseFactory.BookType;
                    break;
                case "StampBase":
                    itemType = baseType; // CollectableBaseFactory.StampType;
                    break;
                default:
                    itemType = null;
                    break;
            }
            Mock<ICollectableItem> mockItem = new Mock<ICollectableItem>();
            mockItem.Setup(b => b.CollectableType).Returns(itemType);

            return mockItem;
        }

        private Mock<ICollectableItem> GetDifferentTypeMockItem(Type collectableType)
        {
            Mock<ICollectableItem> mockItem;
            if (collectableType == CollectableBaseFactory.CollectableTypes[0])
            {
                mockItem = GetMockItem(CollectableBaseFactory.CollectableTypes[1]);
            }
            else
            {
                mockItem = GetMockItem(CollectableBaseFactory.CollectableTypes[0]);
            }

            return mockItem;
        }



    }
}
