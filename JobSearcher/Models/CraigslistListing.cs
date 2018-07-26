using System;
using System.Linq;
using HtmlAgilityPack;
using JobSearcher.Abstract;

namespace JobSearcher.Models
{
    internal class CraigslistListing : ListingBase
    {
        #region Static Fields and Constants

        public const string Compensation = "compensation:";
        public const string Employment = "employment type:";

        #endregion

        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {
                    var node = htmlNode.SelectSingleNode("./p/a[contains(@class,'result-title hdrlnk')]") ?? htmlNode.SelectSingleNode("./a[contains(@class,'result-title hdrlnk')]");
                    if (node != null)
                    {
                        ListingUrl = new Uri(node.Attributes["href"].Value);
                        Heading = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode("./time");
                    if (node != null)
                    {
                        ListingTime = DateTime.Parse(node.Attributes["datetime"].Value);
                    }

                    node = htmlNode.SelectSingleNode("./span/span[contains(@class,'nearby')]");
                    if (node != null)
                    {
                        City = CleanString(node.Attributes["title"].Value);
                    }
                    else
                    {
                        node = htmlNode.SelectSingleNode("./span/span[contains(@class,'result-hood')]");
                        if (node != null)
                        {
                            City = CleanString(node.InnerText);
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
                var node = htmlNode.SelectSingleNode("//p[contains(@class,'attrgroup')]");
                if (node != null)
                {
                    var c = node.ChildNodes.FirstOrDefault(n => n.InnerText.Contains(Compensation));
                    if (c != null)
                    {
                        SalaryRange = CleanString(c.InnerText.Replace(Compensation, string.Empty));
                    }

                    var e = node.ChildNodes.FirstOrDefault(n => n.InnerText.Contains(Employment));
                    if (e != null)
                    {
                        EmploymentType = CleanString(e.InnerText.Replace(Employment, string.Empty));
                    }
                }

                node = htmlNode.SelectSingleNode("//section[contains(@id, 'postingbody')]");
                if (node != null)
                {
                    Description = CleanString(node.InnerText);
                }
            }
        }

        #endregion
    }
}