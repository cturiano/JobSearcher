using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Properties;

namespace JobSearcher.Models
{
    internal class IndeedListing : ListingBase
    {
        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {                                                                                       
                    ListingUrl = new Uri(Resources.IndeedBaseUrl + $"/rc/clk?jk={htmlNode.Attributes["data-jk"].Value}");

                    var node = htmlNode.SelectSingleNode("./div/div/div/table/tr/td/div/h2[contains(@class,'jobTitle')]");
                    if (node != null)
                    {
                        Heading = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode("./div/div/div/table/tr/td/div/pre/span[contains(@class,'companyName')]");
                    if (node != null)
                    {
                        Company = CleanString(node.InnerText);

                        node = node.NextSibling.NextSibling;
                        if (node != null)
                        {
                            City = CleanString(node.InnerText);
                        }
                    }

                    node = htmlNode.SelectSingleNode("./div/div/div/table/tr/td/div[contains(@class, 'tapItem-gutter')]/div[contains(@class,'metadata')]/span");
                    if (node != null)
                    {
                        SalaryRange = CleanString(node.InnerText);
                    }
                }
                catch (Exception e)
                {
                    // do something useful here
                }
            }
        }

        public override void ParseRemainingData(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                var node = htmlNode.SelectSingleNode("./div[contains(@class,'jobsearch-JobMetadataHeader')]/div");
                if (node != null)
                {
                    EmploymentType = CleanString(node.InnerText);
                }
                
                node = htmlNode.SelectSingleNode("./div[contains(@class,'jobsearch-JobComponent-description')]");
                if (node != null)
                {
                    Description = CleanString(Regex.Replace(node.InnerText, "<.*?>", Environment.NewLine));
                }
                                                                       
                node = htmlNode.SelectSingleNode("./div/div[contains(@class,'jobsearch-JobMetadataFooter')]");
                if (node != null)
                {
                    if (DateTime.TryParse(node.InnerText, out var dt))
                    {
                        ListingTime = dt;
                    }
                    else
                    {
                        if (node.InnerText.Equals("today", StringComparison.CurrentCultureIgnoreCase))
                        {
                            ListingTime = DateTime.Now;
                        }
                        else if (int.TryParse(Regex.Match(node.InnerText, @"\d+").Value, out var time))
                        {
                            if (node.InnerText.Contains("days"))
                            {
                                ListingTime = DateTime.Now - new TimeSpan(time, 0, 0, 0);
                            }
                            else if (node.InnerText.Contains("hours"))
                            {
                                ListingTime = DateTime.Now - new TimeSpan(0, time, 0, 0);
                            }
                            else
                            {
                                ListingTime = DateTime.Now;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}