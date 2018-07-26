using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JobSearcher.Abstract;

namespace JobSearcher.Models
{
    internal class MonsterListing : ListingBase
    {
        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {
                    var node = htmlNode.SelectSingleNode("./article/div/div/div[contains(@class,'jobTitle')]/h2/a");
                    if (node != null)
                    {
                        ListingUrl = new Uri(node.Attributes["href"].Value);
                        Heading = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode("./article/div/div/div[contains(@class,'company')]/a/span");
                    if (node != null)
                    {
                        Company = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode("./article/div/div/div[contains(@class,'location')]/p/a");
                    if (node != null)
                    {
                        City = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode("./article/div/div[contains(@class,'date')]/p/time");
                    if (node != null)
                    {
                        if (DateTime.TryParse(CleanString(node.Attributes["datetime"].Value), out var dt))
                        {
                            ListingTime = dt;
                        }
                        else
                        {
                            if (int.TryParse(Regex.Match(node.InnerText, @"\d+").Value, out var days))
                            {
                                ListingTime = DateTime.Now - new TimeSpan(days);
                            }
                        }
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
            var node = htmlNode.SelectSingleNode("./div/div/section/div/span[contains(@id,'TrackingJobBody')]");
            if (node != null)
            {
                Description = CleanString(Regex.Replace(node.InnerText, "<.*?>", Environment.NewLine));
            }

            node = htmlNode.SelectSingleNode("./div/aside/div/div/div/section/div/section/dl/dt[contains(.,'Job type')]").NextSibling.NextSibling;
            if (node != null)
            {
                EmploymentType = CleanString(node.InnerText);
            }

            node = htmlNode.SelectSingleNode("./div/aside/div/div/div/section/div/section/dl/dt[contains(.,'Salary')]").NextSibling.NextSibling;
            if (node != null)
            {
                SalaryRange = CleanString(node.InnerText);
            }
        }

        #endregion
    }
}