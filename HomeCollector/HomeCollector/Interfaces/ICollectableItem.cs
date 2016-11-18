using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    // This is a base description of a collectable item of a particular type
    // such as a 1997 Bugs Bunny US Postal Stamp
    public interface ICollectableItem
    {
        string Title { get; set; }
        string Description { get; set; }    // description of the generic item

    }

}
