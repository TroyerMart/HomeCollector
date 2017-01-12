using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Factories;
using HomeCollector.Models.Members;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using HomeCollector.Repositories;
using HomeCollector.IO;

namespace HomeCollector_IntegrationTests
{
    [TestClass]
    public class HomeCollectionRepositoryIntTests
    {
        [TestMethod]
        public void create_and_save_test_collection_to_disk()
        {
            int numberOfCollectables = 2;
            int numberOfItemsPerCollectable = 3;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                // Initialize the collection
                string collectionName = $"Test {collectionType.Name} Collection";
                ICollectionBase testCollection = GetTestCollection(collectionName, collectionType, numberOfCollectables, numberOfItemsPerCollectable);

                // Initialize a repository
                IFileIO fileIO = new FileIO();
                HomeCollectionRepository repo = new HomeCollectionRepository(testCollection, fileIO);

                // Write file to disk
                string fullFilePath = Environment.CurrentDirectory + @"\";
                fullFilePath += collectionName + @"." + HomeCollectionRepository.FILE_EXTENSION;
                try
                {
                    repo.SaveCollection(fullFilePath, true);
                    Assert.IsTrue(true);
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Should not have failed to write test collection file to disk: {collectionType.Name}", ex.Message);
                }
            }
            
        }

        [TestMethod]
        public void load_test_book_collection_from_disk()
        {
            // I really don't like having to initialize this.  Should just be able pass variable, or not require it.  Expose instead.
            string collectionName = "Star Trek Books - 1";
            ICollectionBase testCollection = new HomeCollection(collectionName, CollectableBaseFactory.BookType);

            // Initialize a repository
            IFileIO fileIO = new FileIO();
            HomeCollectionRepository repo = new HomeCollectionRepository(testCollection, fileIO);

            string fullFilePath = Environment.CurrentDirectory + @"\";
            fullFilePath += collectionName + @"." + HomeCollectionRepository.FILE_EXTENSION;

            ICollectionBase books = repo.LoadCollection(fullFilePath);

        }

        /****** helper methods ***********************************************************************/
        private BookItem GetTestBookItem(int i)
        {
            BookItem item = new BookItem()
            {
                EstimatedValue = i * 0.75M,
                IsFavorite = false,
                ItemDetails = $"Test{i}",
                Condition = BookConditionEnum.Fine
            };
            return item;
        }
        private BookBase GetTestBookBase(int i)
        {
            BookBase collectable = new BookBase()
            {
                Author = $"Author{i}",
                BookCode = $"ABC{i}",
                DatePublished = DateTime.Today.AddDays(-i),
                Description = $"description{i}",
                DisplayName = $"display{i}",
                Edition = $"edition{i}",
                ISBN = $"123-4442111-{i}",
                Publisher = $"publisher{i}",
                Series = null,
                Title = $"title{i}",
                Year = 2000 + i
            };
            return collectable;
        }
        private StampItem GetTestStampItem(int i)
        {
            StampItem item = new StampItem()
            {
                EstimatedValue = i * 0.50M,
                IsFavorite = true,
                ItemDetails = $"Test{i}",
                Condition = StampConditionEnum.VeryFine,
                IsMintCondition = true
            };
            return item;
        }
        private StampBase GetTestStampBase(int i)
        {
            StampBase collectable = new StampBase()
            {
                AlternateId = $"alternateid{i}",
                Country = StampCountryEnum.USA,
                Description = $"description{i}",
                DisplayName = $"displayname{i}",
                FirstDayOfIssue = DateTime.Today.AddDays(-i * 100),
                IsPostageStamp = true,
                ScottNumber = $"scottnumber{i}",
                YearOfIssue = 2000 + i
            };
            return collectable;
        }

        private ICollectionBase GetTestCollection(string collectionName, Type collectableType, int numberOfCollectables, int numberOfItemsPerCollectable = 0)
        {
            ICollectableBase collectable = null;
            ICollectableItem item = null;
            Type collectionType = collectableType;
            ICollectionBase testCollection = new HomeCollection(collectionName, collectionType);
            for (int i = 0; i < numberOfCollectables; i++)
            {
                if (collectionType == CollectableBaseFactory.BookType)
                {
                    collectable = GetTestBookBase(i);
                }
                else if (collectionType == CollectableBaseFactory.StampType)
                {
                    collectable = GetTestStampBase(i);
                }
                testCollection.AddToCollection(collectable);

                for (int j = 0; j < numberOfItemsPerCollectable; j++)
                {
                    if (collectionType == CollectableBaseFactory.BookType)
                    {
                        item = GetTestBookItem(i);
                    }
                    else if (collectionType == CollectableBaseFactory.StampType)
                    {
                        item = GetTestStampItem(i);
                    }
                    collectable.AddItem(item);
                }
            }
            return testCollection;
        }


    }
}
