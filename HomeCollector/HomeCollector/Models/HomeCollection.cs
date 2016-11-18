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
        private List<ICollectionMember> _members;
        private Type _itemType;

        public HomeCollection(ICollectableItem itemToCollect)
        {
            _itemType = itemToCollect.GetType();
            _members = new List<ICollectionMember>();
        }

        public string CollectionName { get; set; }

        public Type CollectionType  // the actual type of objects allowed to be added
        {
            get
            {
                return _itemType;
            }
        }

        public void AddMember(ICollectionMember memberToAdd)
        {
            throw new NotImplementedException();
        }
        // also want overload to pass in ICollectableItem and other params to create an ICollectionMember

        public List<ICollectionMember> GetMembers()
        {
            throw new NotImplementedException();
        }

        public void RemoveMember(ICollectionMember memberToRemove)
        {
            throw new NotImplementedException();
        }
    }

}
