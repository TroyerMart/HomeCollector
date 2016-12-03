using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
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

        private List<ICollectableMember> _items;
        private string _displayName = DISPLAYNAME_DEFAULT;

        public Type ObjectType { get { return GetType(); } }

        // from ICollectableBase
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

        // from ICollectableBase
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
            _items = new List<ICollectableMember>();
        }

        // from ICollectableBase
        public IList<ICollectableMember> GetItems()
        {
            return _items;
        }

        public void AddItem(ICollectableMember itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(ICollectableMember itemToRemove)
        {
            throw new NotImplementedException();
        }

        public bool IsSame(ICollectableBase itemToCompare, bool useAlternateId)
        {
            if (itemToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type defnType = itemToCompare.ObjectType;
            if (defnType != typeof(StampBase))
            {
                throw new CollectableException($"Invalid type {defnType}, expected type {typeof(StampBase)}");
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