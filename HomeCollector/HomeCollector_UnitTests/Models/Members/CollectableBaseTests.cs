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
                Mock<ICollectableItem> mockItem;
                ICollectableBase collectable = GetTestBase(collectableType, N);
                if (collectable.CollectableType.Name == "BookBase")
                {
                    mockItem = GetMockItem(CollectableBaseFactory.StampType);
                }
                else
                {
                    mockItem = GetMockItem(CollectableBaseFactory.BookType);
                }

                try
                {
                    collectable.AddItem(mockItem.Object);
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
        public void removeitem_from_existing_list()
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

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_list_is_empty()
        {
            int N = 0;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItem = mockItem = GetMockItem(collectableType);

                collectable.RemoveItem(mockItem.Object);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_null()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                ICollectableItem mockItem = null;

                collectable.RemoveItem(mockItem);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_incorrect_type()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItem;
                if (collectableType == CollectableBaseFactory.BookType)
                {
                    mockItem = GetMockItem(CollectableBaseFactory.StampType);
                }
                else
                {
                    mockItem = GetMockItem(CollectableBaseFactory.BookType);
                }
                collectable.RemoveItem(mockItem.Object);
            }
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void removeitem_from_existing_list_fails_when_item_is_not_in_list()
        {
            int N = 3;
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, N);
                Mock<ICollectableItem> mockItem = GetMockItem(collectableType);

                collectable.RemoveItem(mockItem.Object);
            }
        }


        // test get items list
        [TestMethod]
        public void getitems_list_has_all_items()
        {
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectableBase collectable = GetTestBase(collectableType, 0);
                Mock<ICollectableItem> mockItem1 = GetMockItem(collectableType);
                Mock<ICollectableItem> mockItem2 = GetMockItem(collectableType);
                Mock<ICollectableItem> mockItem3 = GetMockItem(collectableType);

                collectable.AddItem(mockItem1.Object);
                collectable.AddItem(mockItem2.Object);
                collectable.AddItem(mockItem3.Object);

                IList<ICollectableItem> items = collectable.GetItems();

                Assert.AreEqual(mockItem1.Object, items[0]);
                Assert.AreEqual(mockItem2.Object, items[1]);
                Assert.AreEqual(mockItem3.Object, items[2]);
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
        public void clearitems_from_existing_list()
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
        public void clearitems_from_empty_list()
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
                    testBase = (BookBase)CollectableBaseFactory.CreateCollectableItem(baseType);
                    break;
                case "StampBase":
                    testBase = (StampBase)CollectableBaseFactory.CreateCollectableItem(baseType);
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



    }
}
