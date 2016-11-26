using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCollector.Interfaces
{
    public enum BookConditionEnum
    {
        // http://www.alibris.com/glossary/condition
        New,
        Fine,       // like new
        VeryGood,   // some signs of wear, can't be ex-library
        Good,       // Average used product
        Fair,       // obviously well-worn product
        Poor,        // extensive external wear, soiled, binding defects
        Undefined
    }
    public interface IBookDefinition: ICollectableDefinition
    {
        string Publisher { get; set; }
        string Author { get; set; }
        DateTime DatePublished { get; set; }
        string Edition { get; set; }
        BookConditionEnum Condition { get; set; }
        string ISBN { get; set; }
        string Series { get; set; }
        int BookNumber { get;  set;}

        bool Equals(IBookDefinition defnToCompare, bool useTitleAuthor);

    }

}
