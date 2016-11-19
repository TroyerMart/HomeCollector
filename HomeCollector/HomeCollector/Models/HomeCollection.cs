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
        private List<ICollectionMember> _members;
        private Type _itemType;

        public HomeCollection(ICollectableDefinition itemToCollect)
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
            try
            {
                _members.Add(memberToAdd);
            } catch (Exception ex)
            {
                throw;
            }
        }

        public void AddMember(ICollectableDefinition collectableItem, string memberDetails = "",
            decimal estimatedValue = 0, bool isPlaceholder = false, bool isFavorite = false
            )
        {
            ICollectableDefinition newItem = CollectableDefinitionFactory.CreateCollectableItem(_itemType);
            ICollectionMember newMember = new CollectionMember(newItem) {
                MemberDetails = memberDetails,
                EstimatedValue = estimatedValue,
                IsPlaceholder = isPlaceholder,
                IsFavorite = isFavorite
            };
            AddMember(newMember);
        }

        public List<ICollectionMember> GetMembers()
        {
            return _members;
        }

        public void RemoveMember(ICollectionMember memberToRemove)
        {
            try
            {
                _members.Remove(memberToRemove);
            } catch (Exception ex)
            {
                throw;
            }
        }

    }

}
