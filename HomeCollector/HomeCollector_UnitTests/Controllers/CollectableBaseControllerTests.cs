using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using Moq;
using HomeCollector.Controllers;
using System.Collections.Generic;
using HomeCollector.Exceptions;
using HomeCollector.Models;

namespace HomeCollector_UnitTests.Controllers
{
    [TestClass]
    public class CollectableBaseControllerTests
    {
        Mock<ICollectableBase> mockCollectableBase;
        CollectableBaseController controller;

        [TestInitialize]
        public void InitializeTest()
        {
            // setup the controller using a mock instance of a collectable base object
            mockCollectableBase = new Mock<ICollectableBase>();
            controller = new CollectableBaseController(mockCollectableBase.Object);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void controller_initialized_with_null_collectable_base_object_fails()
        {
            ICollectableBase nullBase = null;

            controller = new CollectableBaseController(nullBase);

            Assert.IsFalse(true, "Expected the test to fail when initialized with a null object");
        }

        [TestMethod]
        public void controller_initialized_with_collectable_base_object_returns_controller_instance()
        {
            try
            {
                ICollectableBase collectableBase = mockCollectableBase.Object;

                controller = new CollectableBaseController(collectableBase);

                Assert.IsNotNull(controller);
            }
            catch 
            {
                Assert.IsFalse(true, "Test should not fail when initialized with an object");
            }          
        }

        [TestMethod]
        public void controller_collectabletype_returns_initial_collectable_base_type()
        {
            Type objType = typeof(ICollectableBase);
            ICollectableBase collectableBase = mockCollectableBase.Object;
            mockCollectableBase.Setup(b => b.CollectableType).Returns(objType);
            
            controller = new CollectableBaseController(collectableBase);
            Type objTestType = controller.CollectableType;

            Assert.AreEqual(objType, objTestType);
        }

        [TestMethod]
        public void calling_controller_additem_calls_collectable_additem()
        {
            Mock<ICollectableItem> mockCollectableBaseToAdd = new Mock<ICollectableItem>();

            controller.AddItem(mockCollectableBaseToAdd.Object);

            mockCollectableBase.Verify(b => b.AddItem(It.IsAny<ICollectableItem>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_removeitem_calls_collectable_removeitem()
        {
            Mock<ICollectableItem> mockCollectableBaseToRemove = new Mock<ICollectableItem>();

            controller.RemoveItem(mockCollectableBaseToRemove.Object);

            mockCollectableBase.Verify(b => b.RemoveItem(It.IsAny<ICollectableItem>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_getitems_calls_collectable_getitems()
        {
            IList<ICollectableItem> items = controller.GetItems();
            
            mockCollectableBase.Verify(b => b.ItemInstances, Times.Once);
        }

        [TestMethod]
        public void calling_controller_issame_calls_collectable_issame()
        {
            bool useAlternateId = false;
            Mock<ICollectableBase> mockCollectableBaseToCompare = new Mock<ICollectableBase>();

            bool isSame = controller.IsSame(mockCollectableBaseToCompare.Object, useAlternateId);

            mockCollectableBase.Verify(b => b.IsSame(It.IsAny<ICollectableBase>(), It.IsAny<bool>()), Times.Once);
        }

    }
}
