using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class BookDefinition : IBookDefinition, ICollectableDefinition
    {
        public const string DISPLAYNAME_DEFAULT = "book name";

        private List<ICollectionMember> _items;
        private string _displayName = DISPLAYNAME_DEFAULT;
        private int _year = 0;

        public Type ObjectType { get { return GetType(); } }

        // from ICollectableDefinition
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

        // from IBookDefinition
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

        public BookDefinition()
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
            if (itemToAdd == null)
            {
                throw new CollectionException("Cannot add a null item");
            }
            Type itemType = itemToAdd.ObjectType;
            if (itemType != typeof(BookItem))
            {
                throw new CollectionException($"Invalid type {itemType}, expected type {typeof(BookItem)}");
            }
            _items.Add(itemToAdd);
        }

        public void RemoveItem(ICollectionMember itemToRemove)
        {
            throw new NotImplementedException();
        }

        public bool IsSame(ICollectableDefinition defnToCompare, bool useTitleAuthor)
        {   
            if (defnToCompare == null)
            {
                throw new CollectableException("Cannot compare to a null item");
            }
            Type defnType = defnToCompare.ObjectType;
            if (defnType != typeof(BookDefinition))
            {
                throw new CollectableException($"Invalid type {defnType}, expected type {typeof(BookDefinition)}");
            }
            BookDefinition bookDef = (BookDefinition)defnToCompare;
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
