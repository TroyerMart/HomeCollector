﻿using HomeCollector.Exceptions;
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
        private Type _collectionType;

        public HomeCollection(string collectionName, Type collectionType)
        {
            if (!CollectableBaseFactory.IsCollectableType(collectionType))
            {
                throw new CollectionException($"Type {collectionType?.Name} must be of a valid Collectable Type");
            }
            CollectionName = collectionName;
            _collectionType = collectionType;
            _collection = new List<ICollectableBase>();
        }

        public string CollectionName {
            get { return _collectionName; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new CollectionException($"Collection name cannot be empty or null");
                }
                _collectionName = value;
            }
        }

        public Type CollectionType  // the actual type of objects allowed to be added
        {
            get
            {
                return _collectionType;
            }
        }

        public IList<ICollectableBase> Collectables { get { return _collection; } }

        public void AddToCollection(ICollectableBase collectableToAdd)
        {
            if (collectableToAdd == null)
            {
                throw new CollectionException("Cannot add a null collectable to the collection");
            }
            Type collectableType = collectableToAdd.CollectableType;
            if (collectableType != CollectionType)
            {
                throw new CollectionException($"Cannot add a collectable of an invalid type to the collection, Type={collectableType.Name}, but expected {CollectionType.Name}");
            }
            try
            {
                _collection.Add(collectableToAdd);
            } catch (Exception ex)
            {
                throw new CollectionException("Error adding item to collection", ex);
            }
        }

        public void RemoveFromCollection(ICollectableBase collectableToRemove)
        {
            try
            {
                _collection.Remove(collectableToRemove);
            } catch (Exception ex)
            {
                throw new CollectionException("Error removing item from collection", ex);
            }
        }

        public void ClearCollection()
        {
            _collection = new List<ICollectableBase>();
        }

    }

}
