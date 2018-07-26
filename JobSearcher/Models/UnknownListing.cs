using System;
using HtmlAgilityPack;
using JobSearcher.Abstract;

namespace JobSearcher.Models
{
    internal class UnknownListing : ListingBase
    {
        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {
                    var node = htmlNode.SelectSingleNode("./a");
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

        #endregion
    }
}