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
        public const string CONDITION_DEFAULT = "UNDEFINED";
        public static readonly List<string> BOOK_CONDITIONS = new List<string>
        {   // http://www.alibris.com/glossary/condition
            CONDITION_DEFAULT,
            "N",    // New
            "F",    // Fine - like new
            "VG",   // Very Good - some signs of wear, can't be ex-library
            "G",    // Good - Average used product
            "F",    // Fair - obviously well-worn product
            "P"     // Poor - extensive external wear, soiled, binding defects
        };

        private decimal _estimatedValue = 0;
        private string _condition = CONDITION_DEFAULT;
        
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
        public string Condition
        {
            get { return _condition; }
            set
            {
                if (ValidateCondition(value))
                {
                    _condition = value;
                }
                else
                {
                    throw new CollectableException($"Invalid book condition: {value}");
                }
            }
        }

        /************************** helper methods **********************************************************/
        internal static bool ValidateCondition(string condition)
        {
            if (BOOK_CONDITIONS.Contains(condition))
            {
                return true;
            }
            return false;
        }
    }

}
