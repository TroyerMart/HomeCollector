using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    public interface IFileIO
    {
        void WriteFile(string fullFilePath, string fileContent, bool overwrite);

        string ReadFile(string fullFilePath);
        void DeleteFile(string fullFilePath, bool forceDeleteIfReadonly = false);

        string GetFullFilePath(string path, string filename);

    }

}
