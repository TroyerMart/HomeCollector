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
            try
            {
                FileInfo fi = GetFileInfo(fullFilePath);
                if (! fi.Exists)
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
                    throw new FileIOException($"Unable to read file: {fullFilePath}", ex);
                }
                return fileContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteFile(string fullFilePath, string fileContent, bool overwrite)
        {        
            try
            {
                FileInfo fi = GetFileInfo(fullFilePath);
                if (fi.Exists)
                {
                    if (overwrite)
                    {
                        try
                        {
                            DeleteFile(fullFilePath);
                        }
                        catch (Exception ex)
                        {
                            throw new FileIOException($"Unable to delete existing file: {fullFilePath}", ex);
                        }
                    }
                    else
                    {
                        throw new FileIOException($"File exists, so file was not overwritten: {fullFilePath}");
                    }
                }
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.Write(fileContent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }

        public void DeleteFile(string fullFilePath, bool forceDeleteIfReadonly = false)
        {
            try
            {
                FileInfo fi = GetFileInfo(fullFilePath);
                if (fi.Exists)
                {
                    fi.IsReadOnly = fi.IsReadOnly && (! forceDeleteIfReadonly);
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        throw new FileIOException($"Unable to delete existing file: {fullFilePath}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFullFilePath(string path, string filename)
        {
            return GetFullFilePathString(path, filename);
        }

        // static methods
        public static string GetFullFilePathString(string path, string filename)
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

        public static FileInfo GetFileInfo(string fullFilePath)
        {
            FileInfo fi = null;
            if (string.IsNullOrWhiteSpace(fullFilePath))
            {
                throw new FileIOException("Full file path cannot be null or blank");
            }
            try
            {
                fi = new FileInfo(fullFilePath);
            } 
            catch (Exception ex)
            {
                throw new FileIOException($"Unable to initialize FileInfo object for: {fullFilePath}", ex);
            }
            return fi;
        }
    }

}
