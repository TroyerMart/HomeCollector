using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models
{
    public class BookDefinition : IBookDefinition
    {
        // from ICollectableItem
        public string Title { get; set; }
        public string Description { get; set; }    // description of the generic item

        // from IBook
        public string Publisher { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public string Edition { get; set; }
        public BookConditionEnum Condition { get; set; } = BookConditionEnum.Undefined;
        public string ISBN { get; set; }
        public string Series { get; set; }
        public int BookNumber { get; set; }

        public BookDefinition()
        {
        }

        public bool Equals(ICollectableDefinition itemToCompare)
        {
            throw new NotImplementedException();
        }
    }

}
