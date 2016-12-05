using HomeCollector.Factories;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class HomeCollection : ICollectionBase
    {
        private string _collectionName;
        private List<ICollectableBase> _collection;
        private Type _collectableType;

        public HomeCollection(string collectionName, Type collectableType)
        {
            _collectionName = collectionName;
            _collectableType = collectableType;
            _collection = new List<ICollectableBase>();
        }

        public string CollectionName { get; set; }

        public Type CollectionType  // the actual type of objects allowed to be added
        {
            get
            {
                return _collectableType;
            }
        }

        public void AddToCollection(ICollectableBase collectableToAdd)
        {
            try
            {
                _collection.Add(collectableToAdd);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void AddMember(ICollectableBase collectableItem, string memberDetails = "",
        //    decimal estimatedValue = 0, bool isPlaceholder = false, bool isFavorite = false
        //    )
        //{
        //    ICollectableBase newItem = CollectableBaseFactory.CreateCollectableItem(_itemType);
        //    ICollectionMember newMember = new CollectionMember(newItem) {
        //        ItemDetails = memberDetails,
        //        EstimatedValue = estimatedValue,
        //        IsPlaceholder = isPlaceholder,
        //        IsFavorite = isFavorite
        //    };
        //    AddMember(newMember);
        //}

        public IList<ICollectableBase> GetCollection()
        {
            return _collection;
        }

        public void RemoveFromCollection(ICollectableBase collectableToRemove)
        {
            try
            {
                _collection.Remove(collectableToRemove);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearCollection()
        {
            _collection = new List<ICollectableBase>();
        }

    }

}
