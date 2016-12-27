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
    public class StampBase : IStampBase, ICollectableBase
    {
        public const string DISPLAYNAME_DEFAULT = "default name";

        private List<ICollectableItem> _items;
        private string _displayName = DISPLAYNAME_DEFAULT;

        // from ICollectableBase
        public Type CollectableType { get { return GetType(); } }
        public string DisplayName
        {
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
        public IList<ICollectableItem> ItemInstances { get { return _items; } }

        public string Description { get; set; }    // description of the generic item

        // from IStampBase
        public StampCountryEnum Country { get; set; } = StampCountryEnum.USA;
        public bool IsPostageStamp { get; set; } = true;
        public string ScottNumber { get; set; }
        public string AlternateId { get; set; }
        public int YearOfIssue { get; set; }
        public DateTime FirstDayOfIssue { get; set; }

        public StampBase()
        {
            _items = new List<ICollectableItem>();
        }
        
        public void AddItem(ICollectableItem itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new CollectableException("Cannot add a null item");
            }
            Type itemType = itemToAdd.CollectableType;
            if (itemType != this.CollectableType)
            {
                throw new CollectableException($"Invalid type {itemType}, expected type {this.CollectableType}");
            }
            _items.Add(itemToAdd);
        }

        public void RemoveItem(ICollectableItem itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new CollectableException("Cannot remove a null item");
            }
            if (_items.Count == 0)
            {
                throw new CollectableException("List is empty - Cannot remove item from list");
            }
            Type itemType = itemToRemove.CollectableType;
            if (itemType != this.CollectableType)
            {
                throw new CollectableException($"Invalid type {itemType}, expected type {this.CollectableType}");
            }
            try
            {
                int count = _items.Count;
                _items.Remove(itemToRemove);
                if (count == _items.Count)
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
            _items = new List<ICollectableItem>();
        }

        public bool IsSame(ICollectableBase itemToCompare, bool useAlternateId)
        {
            if (itemToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type itemType = itemToCompare.CollectableType;
            if (itemType != this.CollectableType)
            {
                throw new CollectableException($"Invalid type {itemType}, expected type {this.CollectableType}");
            }
            StampBase stampDef = (StampBase)itemToCompare;
            if (!useAlternateId)
            {
                if (Country != stampDef.Country)
                {
                    return false;
                }
                if (ScottNumber != stampDef.ScottNumber)
                {
                    return false;
                }
                return true;
            } 
            else
            {
                if (Country != stampDef.Country)
                {
                    return false;
                }
                if (AlternateId != stampDef.AlternateId)
                {
                    return false;
                }
                return true;
            }
        }


    }

}