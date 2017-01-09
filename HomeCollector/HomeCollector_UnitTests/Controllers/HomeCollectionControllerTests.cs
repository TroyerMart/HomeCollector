using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Controllers;
using HomeCollector.Interfaces;
using Moq;
using HomeCollector.Exceptions;
using System.Collections.Generic;
using HomeCollector.Repositories;

namespace HomeCollector_UnitTests.Controllers
{
    [TestClass]
    public class HomeCollectionControllerTests
    {
        Mock<ICollectionBase> _mockHomeCollection;
        Mock<IFileIO> _mockFileIO = new Mock<IFileIO>();
        HomeCollectionController _mockController;

        [TestInitialize]
        public void InitializeTest()
        {
            // setup the controller using a mock instance of a collection base object
            _mockHomeCollection = new Mock<ICollectionBase>();
            _mockController = new HomeCollectionController(_mockHomeCollection.Object, _mockFileIO.Object);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void controller_initialized_with_null_collection_base_fails()
        {
            ICollectionBase nullBase = null;

            _mockController = new HomeCollectionController(nullBase, _mockFileIO.Object);

            Assert.IsFalse(true, "Expected the test to fail when initialized with a null object");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void controller_initialized_with_null_fileio_fails()
        {
            ICollectionBase collectionBase = _mockHomeCollection.Object;
            IFileIO nullFileIO = null;

            _mockController = new HomeCollectionController(collectionBase, nullFileIO);

            Assert.IsFalse(true, "Expected the test to fail when initialized with a null object");
        }

        [TestMethod]
        public void controller_initialized_with_collection_base_object_returns_controller_instance()
        {
            try
            {
                ICollectionBase collectionBase = _mockHomeCollection.Object;

                _mockController = new HomeCollectionController(collectionBase, _mockFileIO.Object);

                Assert.IsNotNull(_mockController);
            }
            catch
            {
                Assert.IsFalse(true, "Test should not fail when initialized with an object");
            }
        }

        [TestMethod]
        public void controller_collectiontype_returns_collectable_base_type()
        {
            Type objType = typeof(ICollectionBase);
            ICollectionBase collectableBase = _mockHomeCollection.Object;
            _mockHomeCollection.Setup(b => b.CollectionType).Returns(objType);

            _mockController = new HomeCollectionController(collectableBase, _mockFileIO.Object);
            Type objTestType = _mockController.CollectionType;

            Assert.AreEqual(objType, objTestType);
        }

        [TestMethod]
        public void calling_controller_addtocollection_calls_homecontroller_addtocollection()
        {
            Mock<ICollectableBase> mockHomeCollectionToAdd = new Mock<ICollectableBase>();

            _mockController.AddToCollection(mockHomeCollectionToAdd.Object);

            _mockHomeCollection.Verify(b => b.AddToCollection(It.IsAny<ICollectableBase>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_removefromcollection_calls_homecollection_removefromcollection()
        {
            Mock<ICollectableBase> mockHomeCollectionToRemove = new Mock<ICollectableBase>();

            _mockController.RemoveFromCollection(mockHomeCollectionToRemove.Object);

            _mockHomeCollection.Verify(b => b.RemoveFromCollection(It.IsAny<ICollectableBase>()), Times.Once);
        }

        [TestMethod]
        public void calling_controller_tolist_calls_homecollection_collectables()
        {
            IList<ICollectableBase> items = _mockController.ToList();

            _mockHomeCollection.Verify(b => b.Collectables, Times.Once);
        }

        [TestMethod]
        public void calling_controller_getcollection_returns_current_icollectionbase_instance()
        {
            // controller is initialized with _mockHomeCollection

            ICollectionBase collection = _mockController.GetCollection();

            Assert.AreEqual(_mockHomeCollection.Object, collection);
        }

        [TestMethod]
        public void calling_controller_clearcollection_calls_homecollection_clearcollection()
        {
            _mockController.ClearCollection();

            _mockHomeCollection.Verify(b => b.ClearCollection(), Times.Once);
        }

        [TestMethod]
        public void calling_controller_savecollection_calls_repository_savecollection()
        {
            Mock<IHomeCollectionRepository> mockRepo = new Mock<IHomeCollectionRepository>();
            HomeCollectionController mockController = new HomeCollectionController(_mockHomeCollection.Object, _mockFileIO.Object, mockRepo.Object);
            string fullFilePath = "fullfilepath";

            mockController.SaveCollection(fullFilePath);

            mockRepo.Verify(r => r.SaveCollection(It.IsAny<string>(), false), Times.Once);
        }

        [TestMethod]
        public void calling_controller_savecollection_with_overwrite_calls_repository_savecollection()
        {
            Mock<IHomeCollectionRepository> mockRepo = new Mock<IHomeCollectionRepository>();
            HomeCollectionController mockController = new HomeCollectionController(_mockHomeCollection.Object, _mockFileIO.Object, mockRepo.Object);
            string fullFilePath = "fullfilepath";
            bool overWriteFile = true;

            mockController.SaveCollection(fullFilePath, overWriteFile);

            mockRepo.Verify(r => r.SaveCollection(It.IsAny<string>(), true), Times.Once);
        }

        // test loadcollection
        [TestMethod]
        public void calling_controller_loadcollection_calls_repository_loadcollection()
        {
            Mock<IHomeCollectionRepository> mockRepo = new Mock<IHomeCollectionRepository>();
            HomeCollectionController mockController = new HomeCollectionController(_mockHomeCollection.Object, _mockFileIO.Object, mockRepo.Object);
            string fullFilePath = "fullfilepath";

            mockController.LoadCollection(fullFilePath);

            mockRepo.Verify(r => r.LoadCollection(It.IsAny<string>()), Times.Once);
        }

    }
}
