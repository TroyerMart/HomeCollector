using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class StampDefinition : IStampDefinition, ICollectableDefinition
    {
        public const string DISPLAYNAME_DEFAULT = "default name";

        private List<ICollectionMember> _items;
        private string _displayName = DISPLAYNAME_DEFAULT;

        public Type ObjectType { get { return GetType(); } }

        // from ICollectableDefinition
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

        // from ICollectableDefinition
        public string Description { get; set; }    // description of the generic item

        // from IStampDefinition
        public StampCountryEnum Country { get; set; } = StampCountryEnum.USA;
        public bool IsPostageStamp { get; set; } = true;
        public string ScottNumber { get; set; }
        public string AlternateId { get; set; }
        public int YearOfIssue { get; set; }
        public DateTime FirstDayOfIssue { get; set; }

        public StampDefinition()
        {
            _items = new List<ICollectionMember>();
        }

        // from ICollectableDefinition
        public IList<ICollectionMember> GetItems()
        {
            return _items;
        }

        public void AddItem(ICollectionMember itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(ICollectionMember itemToRemove)
        {
            throw new NotImplementedException();
        }

        public bool IsSame(ICollectableDefinition defnToCompare, bool useAlternateId)
        {
            if (defnToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type defnType = defnToCompare.ObjectType;
            if (defnType != typeof(StampDefinition))
            {
                throw new CollectableException($"Invalid type {defnType}, expected type {typeof(StampDefinition)}");
            }
            StampDefinition stampDef = (StampDefinition)defnToCompare;
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