using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Interfaces;
using HomeCollector.Models;
using HomeCollector.Factories;
using HomeCollector.Repositories;
using HomeCollector.Exceptions;
using HomeCollector.Models.Members;
using System.Linq;
using Newtonsoft.Json;
using Moq;
using HomeCollector.IO;

namespace HomeCollector_UnitTests.Repositories
{
    [TestClass]
    public class HomeCollectionRepositoryTests
    {
        Mock<IFileIO> _mockFileIO = new Mock<IFileIO>();

        [TestInitialize]
        public void InitializeTest()
        {
            _mockFileIO = new Mock<IFileIO>();
        }

        [TestMethod]
        public void initialize_homecollectionrepository_with_null_fails()
        {
            ICollectionBase nullCollection = null;
            try
            {
                IHomeCollectionRepository repo = new HomeCollectionRepository(nullCollection, _mockFileIO.Object);
                Assert.Fail("HomeCollectionRepository initialization was expected to fail when passed a null value");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void initialize_homecollectionrepository_with_collection_success()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            try
            {
                IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("HomeCollectionRepository was not successfully initialized");
            }
        }

        [TestMethod]
        public void initialize_homecollectionrepository_collectionless_constructor_succeeds()
        {
            try
            {
                IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);

                Assert.IsInstanceOfType(repo, typeof(HomeCollectionRepository));
            }
            catch
            {
                Assert.Fail("HomeCollectionRepository initialization was expected to successfully initialize");
            }
        }

        // Test JSON serialization
        [TestMethod]
        public void convertcollectiontojson_serializes_new_collection_successfully()
        {
            string collectionName = "test";
            Type collectableType = CollectableBaseFactory.CollectableTypes[0];
            ICollectionBase collection = new HomeCollection(collectionName, collectableType);
            string expected = @"{""CollectionName"":""test"",""CollectionType"":""HomeCollector.Models.BookBase, HomeCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",""Collectables"":[]}";

            string json = HomeCollectionRepository.ConvertCollectionToJson(collection);
            // This is a very fragile test.  Should look for expected tokens instead.
            Assert.AreEqual(expected, json);
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void convertcollectiontojson_throws_exception_when_collection_is_null()
        {
            ICollectionBase collection = null;

            string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

            Assert.Fail("Should have thrown exception when collection is null");
        }

        [TestMethod]
        public void convertcollectiontojson_collection_with_collectables_only_success()
        {
            int N = 3;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase collection = GetTestCollection("initial", collectionType, N, 0);

                string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

                int countCollectionNameTag = CountOccurences(json, "CollectionName");
                int countCollectionTypeTag = CountOccurences(json, "CollectionType");
                int countDisplayNameTag = CountOccurences(json, "DisplayName");
                int countDescriptionTag = CountOccurences(json, "Description");
                int countItemInstanceTag = CountOccurences(json, "ItemInstances");

                Assert.AreEqual(1, countCollectionNameTag);
                Assert.AreEqual(1, countCollectionTypeTag);
                Assert.AreEqual(N, countDisplayNameTag);
                Assert.AreEqual(N, countDescriptionTag);
                Assert.AreEqual(N, countItemInstanceTag);
            }
        }

        [TestMethod]
        public void converttojson_collection_with_collectables_and_instances_success()
        {
            int N = 3;
            int M = 2;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {  
                ICollectionBase collection = GetTestCollection("initial", collectionType, N, M);

                string json = HomeCollectionRepository.ConvertCollectionToJson(collection);

                int countCollectionNameTag = CountOccurences(json, "CollectionName");
                int countCollectionTypeTag = CountOccurences(json, "CollectionType");
                int countDisplayNameTag = CountOccurences(json, "DisplayName");
                int countDescriptionTag = CountOccurences(json, "Description");
                int countItemInstanceTag = CountOccurences(json, "ItemInstances");
                int countItemDetailsTag = CountOccurences(json, "ItemDetails");
                int countIsFavoriteTag = CountOccurences(json, "IsFavorite");

                Assert.AreEqual(1, countCollectionNameTag);
                Assert.AreEqual(1, countCollectionTypeTag);
                Assert.AreEqual(N, countDisplayNameTag);
                Assert.AreEqual(N, countDescriptionTag);
                Assert.AreEqual(N, countItemInstanceTag);
                Assert.AreEqual(N * M, countItemDetailsTag);
                Assert.AreEqual(N * M, countIsFavoriteTag);                
            }
        }

        // Test JSON de-serialization back to objects
        [TestMethod]
        public void convertjsontocollection_deserializes_to_collection_success()
        {
            string expectedCollectionName = "test";
            
            string jsonBookCollection = @"{""CollectionName"":""test"",""CollectionType"":""HomeCollector.Models.BookBase, HomeCollector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",""Collectables"":[]}";

            ICollectionBase collection = HomeCollectionRepository.ConvertJsonToCollection(jsonBookCollection);
            // This is a very fragile test.  Should look for expected tokens instead.

            Assert.AreEqual(expectedCollectionName, collection.CollectionName);
            Assert.AreEqual(0, collection.Collectables.Count);
            Assert.AreEqual(CollectableBaseFactory.BookType, collection.CollectionType);
        }

        [TestMethod]
        public void collection_serialize_deserialize_success()
        {
            string collectionName = "test";
            int numberOfCollectables = 2;
            int numberOfItemsPerCollectable = 2;
            foreach(Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetTestCollection(collectionName, collectionType, numberOfCollectables, numberOfItemsPerCollectable);
                string jsonCollection = HomeCollectionRepository.ConvertCollectionToJson(testCollection);

                ICollectionBase resultCollection = HomeCollectionRepository.ConvertJsonToCollection(jsonCollection);
                
                Assert.AreEqual(collectionName, resultCollection.CollectionName);
                Assert.AreEqual(collectionType, resultCollection.CollectionType);
                Assert.AreEqual(numberOfCollectables, resultCollection.Collectables.Count);
                Assert.AreEqual(numberOfCollectables * numberOfItemsPerCollectable, resultCollection.Collectables.Count * resultCollection.Collectables[0].ItemInstances.Count);
            }
        }

        [TestMethod]
        public void collectable_serialize_deserialize_book_success()
        {
            BookBase collectable = GetTestBookBase(2);
            collectable.AddItem(GetTestBookItem(0));
            collectable.AddItem(GetTestBookItem(1));

            string jsonCollectable = JsonConvert.SerializeObject(collectable);
            BookBase newCollectable = (BookBase)HomeCollectionRepository.GetCollectableFromJson(jsonCollectable, collectable.CollectableType);  //JsonConvert.DeserializeObject<BookBase>(jsonCollectable);

            Assert.AreEqual(collectable.CollectableType, newCollectable.CollectableType);
            Assert.AreEqual(collectable.Author, newCollectable.Author);
            Assert.AreEqual(collectable.BookCode, newCollectable.BookCode);
            Assert.AreEqual(collectable.Month, newCollectable.Month);
            Assert.AreEqual(collectable.Description, newCollectable.Description);
            Assert.AreEqual(collectable.DisplayName, newCollectable.DisplayName);
            Assert.AreEqual(collectable.Edition, newCollectable.Edition);
            Assert.AreEqual(collectable.ISBN, newCollectable.ISBN);
            Assert.AreEqual(collectable.Publisher, newCollectable.Publisher);
            Assert.AreEqual(collectable.Series, newCollectable.Series);
            Assert.AreEqual(collectable.Title, newCollectable.Title);
            Assert.AreEqual(collectable.Year, newCollectable.Year);
        }

        [TestMethod]
        public void collectable_serialize_deserialize_stamp_success()
        {
            StampBase collectable = GetTestStampBase(5);
            collectable.AddItem(GetTestStampItem(0));
            collectable.AddItem(GetTestStampItem(1));
            collectable.AddItem(GetTestStampItem(2));

            string jsonItem = JsonConvert.SerializeObject(collectable);
            StampBase newCollectable = (StampBase)HomeCollectionRepository.GetCollectableFromJson(jsonItem, collectable.CollectableType); //JsonConvert.DeserializeObject<StampBase>(jsonItem);

            Assert.AreEqual(collectable.CollectableType, newCollectable.CollectableType);
            Assert.AreEqual(collectable.AlternateId, newCollectable.AlternateId);
            Assert.AreEqual(collectable.Country, newCollectable.Country);
            Assert.AreEqual(collectable.Description, newCollectable.Description);
            Assert.AreEqual(collectable.DisplayName, newCollectable.DisplayName);
            Assert.AreEqual(collectable.FirstDayOfIssue, newCollectable.FirstDayOfIssue);
            Assert.AreEqual(collectable.IsPostageStamp, newCollectable.IsPostageStamp);
            Assert.AreEqual(collectable.ScottNumber, newCollectable.ScottNumber);
            Assert.AreEqual(collectable.YearOfIssue, newCollectable.YearOfIssue);
        }

        [TestMethod]
        public void instanceitem_serialize_deserialize_book_type_success()
        {  
            BookItem item = GetTestBookItem(7);
            string jsonItem = JsonConvert.SerializeObject(item);

            BookItem newItem = (BookItem)HomeCollectionRepository.GetCollectableItemFromJson(jsonItem, item.CollectableType);

            Assert.AreEqual(item.CollectableType, newItem.CollectableType);
            Assert.AreEqual(item.EstimatedValue, newItem.EstimatedValue);
            Assert.AreEqual(item.ItemDetails, newItem.ItemDetails);
            Assert.AreEqual(item.IsFavorite, newItem.IsFavorite);
            Assert.AreEqual(item.Condition, newItem.Condition);
        }

        [TestMethod]
        public void instanceitem_serialize_deserialize_stamp_type_success()
        {
            StampItem item = GetTestStampItem(4);
            string jsonItem = JsonConvert.SerializeObject(item);

            StampItem newItem = (StampItem)HomeCollectionRepository.GetCollectableItemFromJson(jsonItem, item.CollectableType);

            Assert.AreEqual(item.CollectableType, newItem.CollectableType);
            Assert.AreEqual(item.EstimatedValue, newItem.EstimatedValue);
            Assert.AreEqual(item.ItemDetails, newItem.ItemDetails);
            Assert.AreEqual(item.IsFavorite, newItem.IsFavorite);
            Assert.AreEqual(item.Condition, newItem.Condition);
        }

        // test bad json
        [TestMethod]
        public void getcollectableitemfromjson_deserialize_bad_format_throws_exception()
        {
            string jsonItem = "invalid JSON";
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                bool fail = false;
                try
                {
                    ICollectableItem newItem = HomeCollectionRepository.GetCollectableItemFromJson(jsonItem, collectableType);
                }
                catch (CollectableItemInstanceParseException)
                {
                    fail = true;
                    Assert.IsTrue(fail, "Expected exception to be thrown when JSON is invalid and cannot be parsed to an object");
                }
            }
        }

        [TestMethod]
        public void getcollectablefromjson_deserialize_bad_format_throws_exception()
        {
            string jsonCollectable = "invalid JSON";
            foreach (Type collectableType in CollectableBaseFactory.CollectableTypes)
            {
                bool fail = false;
                try
                {
                    ICollectableBase newCollectable = HomeCollectionRepository.GetCollectableFromJson(jsonCollectable, collectableType);
                }
                catch (CollectableParseException)
                {
                    fail = true;
                    Assert.IsTrue(fail, "Expected exception to be thrown when JSON is invalid and cannot be parsed to an object");
                }
            }
        }

        [TestMethod]
        public void convertjsontocollection_deserialize_format_missing_bracket_throws_exception()
        {
            string collectionName = "test";
            int numberOfCollectables = 2;
            int numberOfItemsPerCollectable = 2;
            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetTestCollection(collectionName, collectionType, numberOfCollectables, numberOfItemsPerCollectable);
                string jsonCollectionMissingTrailingBracket = HomeCollectionRepository.ConvertCollectionToJson(testCollection);
                string jsonCollectionMissingLeadingBracket = jsonCollectionMissingTrailingBracket;
                jsonCollectionMissingTrailingBracket = jsonCollectionMissingTrailingBracket.TrimEnd('}'); // remove bracket
                jsonCollectionMissingLeadingBracket = jsonCollectionMissingLeadingBracket.TrimStart('{'); // remove bracket

                bool failTrailing = false;
                bool failLeading = false;
                try
                {
                    ICollectionBase resultCollection = HomeCollectionRepository.ConvertJsonToCollection(jsonCollectionMissingTrailingBracket);
                }
                catch (CollectionParseException)
                {
                    failTrailing = true;
                }
                try
                {
                    ICollectionBase resultCollection = HomeCollectionRepository.ConvertJsonToCollection(jsonCollectionMissingTrailingBracket);
                }
                catch (CollectionParseException)
                {
                    failLeading = true;
                }

                Assert.IsTrue(failTrailing, "Expect exception to be thrown when missing JSON bracket(s)");
                Assert.IsTrue(failLeading, "Expect exception to be thrown when missing JSON bracket(s)");
            }
        }


        // save - validate JSON before writing (make sure it can be deserialized)
        [TestMethod]
        public void savecollection_fails_when_file_path_is_null()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);

            string path = null;
            string filename = "filename";

            try
            {
                repo.SaveCollection(path, filename, false);
                Assert.Fail("Expected save to fail when the path is null");
            }
            catch (CollectionException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void savecollection_fails_when_file_path_is_blank()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);
            
            string path = "";
            string filename = "filename";
            
            try
            {
                repo.SaveCollection(path, filename, false);
                Assert.Fail("Expected save to fail when the path is blank");
            }
            catch (CollectionException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void savecollection_fails_when_filename_is_null()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);

            string path = "filepath";
            string filename = null;

            try
            {
                repo.SaveCollection(path, filename, false);
                Assert.Fail("Expected save to fail when the filename is null");
            }
            catch (CollectionException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void savecollection_fails_when_filename_is_blank()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);

            string path = "filepath";
            string filename = "";

            try
            {
                repo.SaveCollection(path, filename, false);
                Assert.Fail("Expected save to fail when the filename is blank");
            }
            catch (CollectionException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void savecollection_fails_when_collection_is_null()
        {
            // initalize repository without a collection
            IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);
            string fullFilePath = "fullfilepath";

            try
            {
                repo.SaveCollection(fullFilePath, false);
                Assert.Fail("Expected save to fail when the collection is null");
            }
            catch (CollectionException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void savecollection_calls_writefile_with_fullpath_set()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);
            _mockFileIO.Setup(f => f.GetFullFilePath(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string f) => p + @"\" + f);
            //_mockFileIO.Setup(f => f.GetFullFilePath(It.IsAny<string>(), It.IsAny<string>())).Returns(@"filepath\filename");

            string path = "filepath";
            string filename = "filename";
            string fullFilePath = FileIO.GetFullFilePathString(path, filename);
            bool overwrite = false;

            repo.SaveCollection(path, filename, overwrite);

            _mockFileIO.Verify(r => r.WriteFile(fullFilePath, It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void savecollection_calls_writefile_with_overwrite_flag_set()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);

            string path = "filepath";
            string filename = "filename";
            bool overwrite = false;

            repo.SaveCollection(path, filename, overwrite);

            _mockFileIO.Verify(r => r.WriteFile(It.IsAny<string>(), It.IsAny<string>(), overwrite), Times.Once);
        }

        [TestMethod]
        public void savecollection_calls_writefile_with_serialize_json_set()
        {
            Mock<ICollectionBase> mockCollection = new Mock<ICollectionBase>();
            IHomeCollectionRepository repo = new HomeCollectionRepository(mockCollection.Object, _mockFileIO.Object);

            string path = "filepath";
            string filename = "filename";
            string jsonCollection = HomeCollectionRepository.ConvertCollectionToJson(mockCollection.Object);
            bool overwrite = false;

            repo.SaveCollection(path, filename, overwrite);

            _mockFileIO.Verify(r => r.WriteFile(It.IsAny<string>(), jsonCollection, It.IsAny<bool>()), Times.Once);
        }

        // file read tests 
        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void loadcollection_fileio_not_found_throws_exception()
        {
            IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);
            _mockFileIO.Setup(i => i.ReadFile(It.IsAny<string>())).Throws(new CollectionException());
            string fullFilePath = "badfilepath";

            ICollectionBase collection = repo.LoadCollection(fullFilePath);

            Assert.Fail($"Expected exception to be thrown when the file is not found: {fullFilePath}");
        }

        [TestMethod, ExpectedException(typeof(CollectionException))]
        public void loadcollection_file_invalid_json_content_throws_exception()
        {
            IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);
            string mockFileContent = "this is some invalid Json content";
            _mockFileIO.Setup(i => i.ReadFile(It.IsAny<string>())).Returns(mockFileContent);
            string fullFilePath = "filepath";

            ICollectionBase collection = repo.LoadCollection(fullFilePath);

            Assert.Fail($"Expected exception to be thrown when the file content cannot be parsed into a collection: {fullFilePath}");
        }

        [TestMethod]
        public void loadcollection_file_valid_json_content_returns_collection()
        {
            IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);

            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetTestCollection("test collection", collectionType, 1);
                string testFileContent = HomeCollectionRepository.ConvertCollectionToJson(testCollection);
                _mockFileIO.Setup(i => i.ReadFile(It.IsAny<string>())).Returns(testFileContent);
                string fullFilePath = "filepath";

                ICollectionBase collection = repo.LoadCollection(fullFilePath);

                Assert.IsNotNull(collection);
            }
        }

        [TestMethod]
        public void loadcollection_with_path_filename_calls_getfullfilepath()
        {
            IHomeCollectionRepository repo = new HomeCollectionRepository(_mockFileIO.Object);
            _mockFileIO.Setup(f => f.GetFullFilePath(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string p, string f) => p + @"\" + f);

            foreach (Type collectionType in CollectableBaseFactory.CollectableTypes)
            {
                ICollectionBase testCollection = GetTestCollection("test collection", collectionType, 1);
                string testFileContent = HomeCollectionRepository.ConvertCollectionToJson(testCollection);
                _mockFileIO.Setup(i => i.ReadFile(It.IsAny<string>())).Returns(testFileContent);
                string path = "path";
                string filename = "filename";

                ICollectionBase collection = repo.LoadCollection(path, filename);
            }
            _mockFileIO.Verify(r => r.GetFullFilePath(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(CollectableBaseFactory.CollectableTypes.Count));
        }


        // StripInvalidJsonChars - handle null, invalid characters, multiple instances
        // test calls to strip before saving as JSON
        [TestMethod, ExpectedException(typeof(CollectableParseException))]
        public void stripinvalidjson_throws_exception_when_passed_null()
        {
            char padding = 'X';
            string test = null;

            string output = HomeCollectionRepository.StripInvalidJson(test, padding);

            Assert.Fail("Expected exception to be thrown when passed null");
        }

        [TestMethod]
        public void stripinvalidjson_replaces_single_invalid_character()
        {
            char padding = 'X';
            foreach (char c in HomeCollectionRepository.INVALID_JSON_CHARS)
            {
                string test = $"test{c}test";
                string expected = $"test{padding}test";

                string output = HomeCollectionRepository.StripInvalidJson(test, padding);

                Assert.AreEqual(expected, output);
            }
        }

        [TestMethod]
        public void stripinvalidjson_replaces_multiple_invalid_characters()
        {
            char padding = 'v';
            foreach (char c in HomeCollectionRepository.INVALID_JSON_CHARS)
            {
                string test = $"{c}test{c}test{c}";
                string expected = $"{padding}test{padding}test{padding}";

                string output = HomeCollectionRepository.StripInvalidJson(test, padding);

                Assert.AreEqual(expected, output);
            }
        }

        [TestMethod]
        public void stripinvalidjson_replaces_multiple_mixed_invalid_characters()
        {
            char padding = 'X';
            string test = "test";
            string expected = "test";
            string output = "";
            foreach (char c in HomeCollectionRepository.INVALID_JSON_CHARS)
            {
                test += $"{c}test";
                expected += $"{padding}test";
            }
            output = test;
            foreach (char c in HomeCollectionRepository.INVALID_JSON_CHARS)
            {
                output = HomeCollectionRepository.StripInvalidJson(output, padding);
            }

            Assert.AreEqual(expected, output);
        }


        /****** helper methods ***********************************************************************/
        private BookItem GetTestBookItem(int i)
        {
            BookItem item = new BookItem()
            {
                EstimatedValue = i * 0.75M,
                IsFavorite = false,
                ItemDetails = $"Test{i}",
                Condition = "F"
            };
            return item;
        }
        private BookBase GetTestBookBase(int i)
        {
            BookBase collectable = new BookBase()
            {
                Author = $"Author{i}",
                BookCode = $"ABC{i}",
                Month = (i+1) % 12,
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
                Condition = "VF",
                IsMintCondition = true
            };
            return item;
        }
        private StampBase GetTestStampBase(int i)
        {
            StampBase collectable = new StampBase()
            {
                AlternateId = $"alternateid{i}",
                Country = StampBase.COUNTRY_DEFAULT,
                Description = $"description{i}",
                DisplayName = $"displayname{i}",
                FirstDayOfIssue = DateTime.Today.AddDays(- i * 100),
                IsPostageStamp = true,
                ScottNumber = $"scottnumber{i}",
                YearOfIssue =2000 + i
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
                } else if (collectionType == CollectableBaseFactory.StampType)
                {
                    collectable = GetTestStampBase(i);
                }
                testCollection.AddToCollection(collectable);

                for (int j=0; j<numberOfItemsPerCollectable; j++)
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

        private int CountOccurences(string body, string findThis)
        {
            int count = 0;
            int position = 0;
            if (String.IsNullOrEmpty(findThis)) return 0;
            while ((position = body.IndexOf(findThis, position)) != -1)
            {
                position += findThis.Length;
                count += 1;
            }
            return count;
        }

    }

}