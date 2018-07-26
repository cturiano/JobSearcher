using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Properties;

namespace JobSearcher.Models
{
    internal class DiceListing : ListingBase
    {
        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {
                    var node = htmlNode.SelectSingleNode("./div/ul/li/h3/a[contains(@id,'position')]");
                    if (node != null)
                    {
                        ListingUrl = new Uri(new Uri(Resources.DiceBaseUrl), CleanString(node.Attributes["href"].Value));
                        Heading = CleanString(node.SelectSingleNode("./span[contains(@itemprop, 'title')]")?.InnerText);
                    }

                    htmlNode = htmlNode.SelectSingleNode("./div/ul[contains(@class, 'details row')]");
                    if (htmlNode != null)
                    {
                        node = htmlNode.SelectSingleNode("./li/span/a/span[contains(@class, 'compName')]");
                        if (node != null)
                        {
                            Company = CleanString(node.InnerText);
                        }

                        node = htmlNode.SelectSingleNode("./li/span/span[contains(@class, 'jobLoc')]");
                        if (node != null)
                        {
                            City = CleanString(node.InnerText);
                        }

                        node = htmlNode.SelectSingleNode("./li/span[contains(@itemprop, 'datePosted')]");
                        if (node != null)
                        {
                            ListingTime = DateTime.Parse(CleanString(node.InnerText));
                        }

                        node = htmlNode.SelectSingleNode("./li/span[contains(@itemprop, 'baseSalary')]");
                        if (node != null)
                        {
                            SalaryRange = CleanString(node.InnerText);
                        }

                        node = htmlNode.SelectSingleNode("./li/span[contains(@itemprop, 'employmentType')]");
                        if (node != null)
                        {
                            EmploymentType = CleanString(node.InnerText);
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
            if (htmlNode != null)
            {
                var node = htmlNode.SelectSingleNode("./div/div/div/div/div[contains(@itemprop, 'description')]");
                if (node != null)
                {
                    Description = CleanString(Regex.Replace(node.InnerText, "<.*?>", Environment.NewLine));
                }

                if (string.IsNullOrEmpty(SalaryRange))
                {
                    node = htmlNode.SelectSingleNode("./div/div/div/div/div[contains(@class, 'iconsiblings')]/span[contains(@class, 'mL20')]");
                    if (node != null)
                    {
                        SalaryRange = CleanString(node.InnerText);
                    }
                }
            }
        }

        #endregion
    }
}