﻿using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class BookBase : IBookBase, ICollectableBase
    {
        public const string DISPLAYNAME_DEFAULT = "book name";

        private List<ICollectableMember> _items;
        private string _displayName = DISPLAYNAME_DEFAULT;
        private int _year = 0;

        public Type ObjectType { get { return GetType(); } }

        // from ICollectableBase
        public string DisplayName {
            get { return _displayName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CollectableException("Display name cannot be null or empty");
                }
                _displayName = value;
            }
        }

        public string Description { get; set; }    // description of the generic item

        // from IBookBase
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int Year {
            get { return _year; }
            set {
                    if (value >= 0)
                        _year = value;
                    else
                    {
                        throw new CollectableException($"Unable to set book year {value}.  Year must not be negative.");
                    }
                }
            }
        public DateTime DatePublished { get; set; } = DateTime.MinValue;
        public string Edition { get; set; }
        public BookConditionEnum Condition { get; set; } = BookConditionEnum.Undefined;
        public string Series { get; set; }
        public string BookCode { get; set; }

        public BookBase()
        {
            _items = new List<ICollectableMember>();
        }

        // from ICollectableBase
        public IList<ICollectableMember> GetItems()
        {
            return _items;
        }

        public void AddItem(ICollectableMember itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new CollectableException("Cannot add a null item");
            }
            Type itemType = itemToAdd.ObjectType;
            if (itemType != typeof(BookItem))
            {
                throw new CollectableException($"Invalid type {itemType}, expected type {typeof(BookItem)}");
            }
            _items.Add(itemToAdd);
        }

        public void RemoveItem(ICollectableMember itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new CollectableException("Cannot remove a null item");
            }
            if (_items.Count == 0)
            {
                throw new CollectableException("List is empty - Cannot remove item from list");
            }
            Type itemType = itemToRemove.ObjectType;
            if (itemType != typeof(BookItem))
            {
                throw new CollectableException($"Invalid type {itemType}, expected type {typeof(BookItem)}");
            }
            try
            {
                int count = _items.Count;
                _items.Remove(itemToRemove);
                if (count == _items.Count )
                {
                    throw new CollectableException("Item was not removed from list");
                }
            }
            catch (Exception ex)
            {
                throw new CollectableException("Unable to remove item", ex);
            }            
        }

        public void ClearItems()
        {
            _items = new List<ICollectableMember>();
        }

        public bool IsSame(ICollectableBase itemToCompare, bool useTitleAuthor)
        {   
            if (itemToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type defnType = itemToCompare.ObjectType;
            if (defnType != typeof(BookBase))
            {
                throw new CollectableException($"Invalid type {defnType}, expected type {typeof(BookBase)}");
            }
            BookBase bookDef = (BookBase)itemToCompare;
            if (!useTitleAuthor)
            {
                if (ISBN != bookDef.ISBN)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (Title != bookDef.Title)
                {
                    return false;
                }
                if (Author != bookDef.Author)
                {
                    return false;
                }
                return true;
            }
        }

    }

}
