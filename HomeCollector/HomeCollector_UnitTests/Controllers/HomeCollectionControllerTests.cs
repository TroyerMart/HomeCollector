using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Controllers;
using HomeCollector.Interfaces;
using Moq;
using HomeCollector.Exceptions;
using System.Collections.Generic;

namespace HomeCollector_UnitTests.Controllers
{
    [TestClass]
    public class HomeCollectionControllerTests
    {
        Mock<ICollectionBase> mockHomeCollection;
        HomeCollectionController controller;

        [TestInitialize]
        public void InitializeTest()
        {
            // setup the controller using a mock instance of a collection base object
            mockHomeCollection = new Mock<ICollectionBase>();
            controller = new HomeCollectionController(mockHomeCollection.Object);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void controller_initialized_with_null_collection_base_object_fails()
        {
            ICollectionBase nullBase = null;

            controller = new HomeCollectionController(nullBase);

            Assert.IsFalse(true, "Expected the test to fail when initialized with a null object");
        }

        [TestMethod]
        public void controller_initialized_with_collection_base_object_returns_controller_instance()
        {
            try
            {
                ICollectionBase collectionBase = mockHomeCollection.Object;

                controller = new HomeCollectionController(collectionBase);

                Assert.IsNotNull(controller);
            }
            catch
            {
                Assert.IsFalse(true, "Test should not fail when initialized with an object");
            }
        }

        [TestMethod]
        public void controller_collectiontype_returns_collection_base_type()
        {
            Type objType = typeof(ICollectionBase);
            ICollectionBase collectableBase = mockHomeCollection.Object;
            mockHomeCollection.Setup(b => b.CollectionType).Returns(objType);

            controller = new HomeCollectionController(collectableBase);
            Type objTestType = controller.CollectionType;

            Assert.AreEqual(objType, objTestType);
        }

        [TestMethod]
        public void calling_controller_addtocollection_calls_homecontroller_addtocollection()
        {
            Mock<ICollectableBase> mockHomeCollectionToAdd = new Mock<ICollectableBase>();

            controller.AddToCollection(mockHomeCollectionToAdd.Object);

            mockHomeCollection.Verify(b => b.AddToCollection(It.IsAny<ICollectableBase>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_removefromcollection_calls_homecollection_removefromcollection()
        {
            Mock<ICollectableBase> mockHomeCollectionToRemove = new Mock<ICollectableBase>();

            controller.RemoveFromCollection(mockHomeCollectionToRemove.Object);

            mockHomeCollection.Verify(b => b.RemoveFromCollection(It.IsAny<ICollectableBase>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_getcollection_calls_homecollection_getcollection()
        {
            IList<ICollectableBase> items = controller.GetCollection();

            mockHomeCollection.Verify(b => b.GetCollection(), Times.Once);
        }

    }
}
