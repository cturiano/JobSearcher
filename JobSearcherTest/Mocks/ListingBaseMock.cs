using System;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Models;

namespace JobSearcherTest.Mocks
{
    internal class ListingBaseMock : ListingBase
    {
        #region Constructors

        public ListingBaseMock()
        {
            ApplyEmail = new EmailInfo();
            City = "Gotham";
            Company = "Wayne Industries";
            Description = "This is the description";
            EmploymentType = "Full Time";
            Heading = "This is the heading";
            ListingTime = DateTime.Now;
            ListingUrl = new Uri("http://www.google.com");
            SalaryRange = "big money";
            Selected = false;
        }

        #endregion

        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            throw new NotImplementedException();
        }

        public override void ParseRemainingData(HtmlNode htmlNode)
        {
            Description = "~~~~This is not the description you are looking for....~~~";
            EmploymentType = "~~~~This is not the employment type you are looking for....~~~";
        }

        #endregion
    }
}