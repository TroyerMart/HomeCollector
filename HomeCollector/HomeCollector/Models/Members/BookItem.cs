using HomeCollector.Controllers;
using HomeCollector.Exceptions;
using HomeCollector.Factories;
using HomeCollector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Models.Members
{
    public class BookItem : ICollectableItem
    {
        decimal _estimatedValue = 0;

        public BookItem()
        {
        }

        public Type CollectableType  {  get { return CollectableBaseFactory.BookType ; }  }           

        // from ICollectableItem
        public string ItemDetails { get; set; }  // detail information specific to this actual item
        public bool IsFavorite { get; set; } = false;
        public decimal EstimatedValue
        {
            get
            {
                return _estimatedValue;
            }

            set
            {
                if (value < 0)
                {
                    throw new CollectableException("Estimated value must not be less than zero");
                }
                _estimatedValue = value;
            }
        }

        // Book properties
        public BookConditionEnum Condition { get; set; } = BookConditionEnum.Undefined;

    }

}
