﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This represents a generic collection of items 

    public interface ICollectionBase
    {
        string CollectionName { get; set; }
        Type CollectionType { get; }   // the actual type of objects allowed to be added

        List<ICollectionMember> GetMembers();
        void AddMember(ICollectionMember memberToAdd);
        void RemoveMember(ICollectionMember memberToRemove);

    }

}