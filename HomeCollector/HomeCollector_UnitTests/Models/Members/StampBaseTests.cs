using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeCollector.Models;
using HomeCollector.Exceptions;
using HomeCollector.Interfaces;
using HomeCollector.Factories;

namespace HomeCollector_UnitTests.Models.Members
{
    [TestClass]
    public class StampBaseTests
    {

        // test equality
        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = "CAN",
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_scottnumber_are_not_equal_scottnumber()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = "CAN",
                ScottNumber = stamp.ScottNumber
            };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_explicitly_by_scottnumber_are_not_equal_scottnumber()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.ScottNumber = "1000";
            StampBase testStamp = new StampBase()
            { ScottNumber = "1001", Country = stamp.Country };

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_equal()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                AlternateId = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_not_equal_country()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = "CAN",
                ScottNumber = stamp.AlternateId
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void compare_stamp_base_instances_by_country_and_alternateid_are_not_equal_alternateid()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            stamp.Country = StampBase.COUNTRY_DEFAULT;
            stamp.AlternateId = "1000";
            StampBase testStamp = new StampBase()
            {
                Country = stamp.Country,
                AlternateId = "1001"
            };

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing to a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_by_alternateid_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, true);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void compare_stamp_base_instances_by_explicitly_by_scottnumber_returns_false_when_null()
        {
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);
            StampBase testStamp = null;

            bool isEqual = stamp.IsSame(testStamp, false);

            Assert.IsFalse(isEqual, "Expected test to throw exception when comparing a null base instance");
        }


        [TestMethod]
        public void validatecountry_returns_false_when_passed_null_country()
        {
            string invalidCountry = null;

            bool isValid = StampBase.ValidateCountry(invalidCountry);

            Assert.IsFalse(isValid, $"Expected ValidateCountry to return false an unexpected condition: {invalidCountry}");
        }

        [TestMethod]
        public void validatecountry_returns_false_when_passed_unexpected_country()
        {
            string invalidCountry = "invalid";

            bool isValid = StampBase.ValidateCountry(invalidCountry);

            Assert.IsFalse(isValid, $"Expected ValidateCountry to return false an unexpected condition: {invalidCountry}");
        }

        [TestMethod]
        public void validatecountry_returns_true_when_passed_valid_country()
        {
            foreach (string country in StampBase.STAMP_COUNTRIES)
            {
                bool isValid = StampBase.ValidateCountry(country);

                Assert.IsTrue(isValid);
            }
        }

        // default values set
        [TestMethod]
        public void display_name_initialized_to_default_value()
        {
            string expectedValue = StampBase.DISPLAYNAME_DEFAULT;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            Assert.AreEqual(expectedValue, stamp.DisplayName);
        }

        [TestMethod]
        public void country_name_initialized_to_default_value()
        {
            string expectedValue = StampBase.COUNTRY_DEFAULT;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            Assert.AreEqual(expectedValue, stamp.Country);
        }

        [TestMethod]
        public void ispostagestamp_initialized_to_default_value()
        {
            bool expectedValue = true;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            Assert.AreEqual(expectedValue, stamp.IsPostageStamp);
        }

        [TestMethod]
        public void iswatermarked_initialized_to_default_value()
        {
            bool expectedValue = false;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            Assert.AreEqual(expectedValue, stamp.IsWatermarked);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void setting_displayname_to_null_throws_exception()
        {
            string invalidDisplayName = null;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.DisplayName = invalidDisplayName;

            Assert.Fail("Expected exception to be thrown when passed a null display name");
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void setting_displayname_to_empty_string_throws_exception()
        {
            string invalidDisplayName = "";
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.DisplayName = invalidDisplayName;

            Assert.Fail("Expected exception to be thrown when passed a null display name");
        }

        [TestMethod]
        public void setting_displayname_to_valid_name_successful()
        {
            string displayName = "Display Name";
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.DisplayName = displayName;

            Assert.AreEqual(displayName, stamp.DisplayName);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void issueyearstart_set_to_negative_throws_exception()
        {
            int invalidStart = -5;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.IssueYearStart = invalidStart;

            Assert.Fail("Expected exception to be thrown when passed a negative issue year start");
        }

        [TestMethod]
        public void issueyearstart_set_to_nonnegative_succeeds()
        {
            int validStart = 2005;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.IssueYearStart = validStart;

            Assert.AreEqual(validStart, stamp.IssueYearStart);
        }

        [TestMethod, ExpectedException(typeof(CollectableException))]
        public void issueyearend_set_to_negative_throws_exception()
        {
            int invalidEnd = -5;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.IssueYearEnd = invalidEnd;

            Assert.Fail("Expected exception to be thrown when passed a negative issue year end");
        }

        [TestMethod]
        public void issueyearend_set_to_nonnegative_succeeds()
        {
            int validEnd = 2005;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.IssueYearEnd = validEnd;

            Assert.AreEqual(validEnd, stamp.IssueYearEnd);
        }

        [TestMethod]
        public void issueyearend_set_to_zero_set_to_issueyearstart_value()
        {
            int endDate = 0;
            int startDate = 2011;
            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.IssueYearStart = startDate;
            stamp.IssueYearEnd = endDate;

            Assert.AreEqual(startDate, stamp.IssueYearEnd);
        }

        // validate firstdayofissue - date only
        [TestMethod]
        public void firstdayofissue_sets_is_set_to_date_part_only_when_passed_date_with_time()
        {
            DateTime firstDateTime = DateTime.UtcNow;
            DateTime firstDateOnly = DateTime.Today;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.FirstDayOfIssue = firstDateTime;

            Assert.AreEqual(firstDateOnly, stamp.FirstDayOfIssue);
        }

        [TestMethod]
        public void firstdayofissue_sets_is_set_to_date_part_only_when_passed_date_only()
        {
            DateTime firstDateOnly = DateTime.Today;

            StampBase stamp = (StampBase)CollectableBaseFactory.CreateCollectableBase(CollectableBaseFactory.StampType);

            stamp.FirstDayOfIssue = firstDateOnly;

            Assert.AreEqual(firstDateOnly, stamp.FirstDayOfIssue);
        }


    }
}
