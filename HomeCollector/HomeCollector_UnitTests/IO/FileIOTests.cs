using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Exceptions;
using HomeCollector.IO;
using HomeCollector.Interfaces;
using System.IO;
using System.Security.AccessControl;

namespace HomeCollector_UnitTests.IO
{
    [TestClass]
    public class FileIOTests
    {
        IFileIO _fileIO = null;

        [TestInitialize]
        public void InitializeTests()
        {
            _fileIO = new FileIO();
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfullfilepath_null_path_throws_exception()
        {
            string path = null;
            string filename = "filename";

            FileIO.GetFullFilePath(path, filename);

            Assert.Fail("Expected GetFullFilePath to fail is the path is null");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfullfilepath_empty_path_throws_exception()
        {
            string path = "";
            string filename = "filename";

            FileIO.GetFullFilePath(path, filename);

            Assert.Fail("Expected GetFullFilePath to fail is the path is null");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfullfilepath_null_filename_throws_exception()
        {
            string path = "path";
            string filename = null;

            FileIO.GetFullFilePath(path, filename);

            Assert.Fail("Expected GetFullFilePath to fail is the path is null");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfullfilepath_blank_filename_throws_exception()
        {
            string path = "path";
            string filename = "";

            FileIO.GetFullFilePath(path, filename);

            Assert.Fail("Expected GetFullFilePath to fail is the path is null");
        }

        [TestMethod]
        public void getfullfilepath_valid_path_and_filename_returns_full_path()
        {
            string path = "path";
            string filename = "filename";
            string expectedFullPath = path + @"\" + filename;

            string fullFilePath = FileIO.GetFullFilePath(path, filename);

            Assert.AreEqual(expectedFullPath, fullFilePath);
        }

        [TestMethod]
        public void getfullfilepath_valid_path_with_trailing_backslash_and_filename_returns_full_path()
        {
            string path = @"path\";
            string filename = "filename";
            string expectedFullPath = path + filename;

            string fullFilePath = FileIO.GetFullFilePath(path, filename);

            Assert.AreEqual(expectedFullPath, fullFilePath);
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfileinfo_null_fullfilepath_throws_exception()
        {
            string fullFilePath = null;

            FileInfo fi = FileIO.GetFileInfo(fullFilePath);

            Assert.Fail("Expected exception to be thrown when null value passed in");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void getfileinfo_empty_fullfilepath_throws_exception()
        {
            string fullFilePath = "";

            FileInfo fi = FileIO.GetFileInfo(fullFilePath);

            Assert.Fail("Expected exception to be thrown when blank value passed in");
        }

        [TestMethod]
        public void getfileinfo_passed_fullfilepath_returns_fileinfo()
        {
            string fullFilePath = "anypath";

            FileInfo fi = FileIO.GetFileInfo(fullFilePath);

            Assert.IsNotNull(fi, "Expected an instance of a FileInfo object to be returned");
            Assert.AreEqual(typeof(FileInfo), fi.GetType());
        }

        [TestMethod]
        public void writefile_when_file_does_not_exist_without_overwrite_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool overwriteFile = false;
            DeleteTestFile(fullFilePath);

            _fileIO.WriteFile(fullFilePath, content, overwriteFile);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsTrue(fileExistsAfter);
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void writefile_when_file_does_exist_without_overwrite_throws_exception()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool overwriteFile = false;
            CreateTestFile(fullFilePath);

            _fileIO.WriteFile(fullFilePath, content, overwriteFile);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.Fail("Expected exception when file exists and not set to overwrite");
        }

        [TestMethod]
        public void writefile_when_file_does_not_exist_with_overwrite_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool overwriteFile = true;
            DeleteTestFile(fullFilePath);

            _fileIO.WriteFile(fullFilePath, content, overwriteFile);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsTrue(fileExistsAfter);
        }

        [TestMethod]
        public void writefile_when_file_does_exist_with_overwrite_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content from overwrite";
            bool overwriteFile = true;
            CreateTestFile(fullFilePath);

            _fileIO.WriteFile(fullFilePath, content, overwriteFile);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsTrue(fileExistsAfter);
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void writefile_when_readonly_file_does_exist_with_overwrite_throws_exception()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool overwriteFile = true;
            CreateTestFile(fullFilePath, content, true);

            _fileIO.WriteFile(fullFilePath, content, overwriteFile);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.Fail("Expected exception when file exists and not set to overwrite");
        }
                
        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void deletefile_when_readonly_file_does_exist_without_force_delete_throws_exception()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool readonlyFile = true;
            bool forceDelete = false;
            CreateTestFile(fullFilePath, content, readonlyFile);

            _fileIO.DeleteFile(fullFilePath, forceDelete);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.Fail("Expected exception when file is readonly and not set to force delete");
        }

        [TestMethod]
        public void deletefile_when_readonly_file_does_exist_with_force_delete_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool readonlyFile = true;
            bool forceDelete = true;
            CreateTestFile(fullFilePath, content, readonlyFile);

            _fileIO.DeleteFile(fullFilePath, forceDelete);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsFalse(fileExistsAfter, "Expected file deletion");
        }

        [TestMethod]
        public void deletefile_when_file_does_exist_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "content";
            bool readonlyFile = false;
            CreateTestFile(fullFilePath, content, readonlyFile);

            _fileIO.DeleteFile(fullFilePath);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsFalse(fileExistsAfter, "Expected file deletion");
        }

        [TestMethod]
        public void deletefile_when_file_does_not_exist_succeeds()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            DeleteTestFile(fullFilePath); // make sure file is not there

            _fileIO.DeleteFile(fullFilePath);
            bool fileExistsAfter = FileIO.GetFileInfo(fullFilePath).Exists;

            Assert.IsFalse(fileExistsAfter, "Expected no exception to be thrown even when file does not exist");
        }

        [TestMethod, ExpectedException(typeof(FileIOException))]
        public void readfile_when_file_does_not_exist_throws_exception()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            DeleteTestFile(fullFilePath);

            string readContent = _fileIO.ReadFile(fullFilePath);

            Assert.Fail("Expected exception when file does not exist");
        }

        [TestMethod]
        public void readfile_when_file_does_exist_returns_file_content()
        {
            string fullFilePath = @"C:\temp\filename.txt";
            string content = "This is stuff\n\r";
            content += "More stuff on a second line";
            CreateTestFile(fullFilePath, content);

            string readContent = _fileIO.ReadFile(fullFilePath);

            Assert.AreEqual(content, readContent);
        }


        /****************************** helper methods ***********************************************/
        private void CreateTestFile(string testFileName, string fileContent = "content", bool setReadonly = false)
        {
            FileInfo fi = new FileInfo(testFileName);
            if (fi.Exists)
            {
                DeleteTestFile(testFileName);
            }
            using (StreamWriter sw = fi.CreateText())
            {
                sw.Write(fileContent);
            }
            fi.IsReadOnly = setReadonly;
        }

        private void DeleteTestFile(string testFileName)
        {
            FileInfo fi = new FileInfo(testFileName);
            if (fi.Exists)
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }
        }

    }
}
