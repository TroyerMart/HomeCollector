using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.IO
{
    public class FileIO : IFileIO
    {
        public string ReadFile(string fullFilePath)
        {
            string fileContent = null;
            System.IO.FileInfo fi = new System.IO.FileInfo(fullFilePath);
            if (!fi.Exists)
            {
                throw new FileIOException($"Unable to locate file: {fullFilePath}");
            }
            try
            {
                using (StreamReader sr = fi.OpenText())
                {
                    fileContent = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new FileIOException($"Unable to read file: {fullFilePath}",ex);
            }
            return fileContent;
        }

        public void WriteFile(string fullFilePath, string fileContent, bool overwrite)
        {
            FileInfo fi = new FileInfo(fullFilePath);
            if (fi.Exists)
            {
                if (overwrite)
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        throw new FileIOException($"Unable to delete existing file: {fullFilePath}", ex);
                    }                    
                }
                else
                {
                    throw new FileIOException($"Unable to overwrite file: {fullFilePath}");
                }                
            }
            using (StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine(fileContent);
            }
        }

        public static string GetFullFilePath(string path, string filename)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new FileIOException("File path cannot be null or blank");
            }
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new FileIOException("Filename cannot be null or blank");
            }
            string fullFilePath = path;
            if (fullFilePath.EndsWith(@"\"))
            {
                fullFilePath += filename;
            }
            else
            {
                fullFilePath += @"\" + filename;
            }
            return fullFilePath;
        }

    }

}
