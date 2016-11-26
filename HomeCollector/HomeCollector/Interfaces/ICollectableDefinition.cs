using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This is a base description of a collectable item of a particular type
    //    such as a 1997 Bugs Bunny US Postal Stamp 
    //    or a 2nd edition of Foundation by Isaac Asimov
    public interface ICollectableDefinition
    {
        string Title { get; set; }
        string Description { get; set; }    // description of the generic item

        bool Equals(ICollectableDefinition defnToCompare);
    }

}
