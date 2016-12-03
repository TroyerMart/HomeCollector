using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using Moq;
using HomeCollector.Controllers;
using System.Collections.Generic;

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

        [TestMethod]
        public void calling_controller_additem_calls_collectable_additem()
        {
            Mock<ICollectableMember> mockCollectableBaseToAdd = new Mock<ICollectableMember>();

            controller.AddItem(mockCollectableBaseToAdd.Object);

            mockCollectableBase.Verify(b => b.AddItem(It.IsAny<ICollectableMember>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_removeitem_calls_collectable_removeitem()
        {
            Mock<ICollectableMember> mockCollectableBaseToRemove = new Mock<ICollectableMember>();

            controller.RemoveItem(mockCollectableBaseToRemove.Object);

            mockCollectableBase.Verify(b => b.RemoveItem(It.IsAny<ICollectableMember>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_getitems_calls_collectable_getitems()
        {
            IList<ICollectableMember> items = controller.GetItems();
            
            mockCollectableBase.Verify(b => b.GetItems(), Times.Once);
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
