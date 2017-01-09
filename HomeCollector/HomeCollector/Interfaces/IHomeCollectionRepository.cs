using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    public interface IHomeCollectionRepository
    {
        // save collection to disk
        void SaveCollection(string fullFilePath);
        void SaveCollection(string fullFilePath, bool overwriteFile);

        void SaveCollection(string path, string filename);
        void SaveCollection(string path, string filename, bool overwriteFile);

        // load collection from disk
        ICollectionBase LoadCollection(string path, string filename);

        ICollectionBase LoadCollection(string fullFilePath);
    }

}
