using System;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Properties;

namespace JobSearcher.Models
{
    internal class StackOverflowListing : ListingBase
    {
        #region Public Methods

        public override void ParseInitialNode(HtmlNode htmlNode)
        {
            if (htmlNode != null)
            {
                try
                {
                    var node = htmlNode.SelectSingleNode("./div/h2/a[contains(@class,'job-link')]");
                    if (node != null)
                    {
                        ListingUrl = new Uri(new Uri(Resources.StackOverFlowBaseUrl), node.Attributes["href"].Value);
                        Heading = CleanString(node.Attributes["title"].Value.Trim('\n', ' '));
                    }

                    node = htmlNode.SelectSingleNode("./div/p[contains(@class,'-posted-date')]");
                    if (node != null)
                    {
                        var ago = CleanString(node.InnerText.Replace("ago", string.Empty));
                        var format = ago[ago.Length - 1];
                        ago = ago.TrimEnd(format);

                        if (format == 'w')
                        {
                            format = 'd';
                            ago = (int.Parse(ago) * 7).ToString();
                        }

                        if (TimeSpan.TryParseExact(ago, "%" + format, CultureInfo.CurrentCulture, out var duration))
                        {
                            ListingTime = DateTime.Now - duration;
                        }
                    }

                    node = htmlNode.SelectSingleNode("./div[contains(@class, '-company')]");
                    if (node != null)
                    {
                        var cNode = node.SelectSingleNode(".//*[contains(@class, '-name')]");
                        if (cNode != null)
                        {
                            Company = CleanString(cNode.InnerText);
                        }

                        cNode = node.SelectSingleNode(".//*[contains(@class, '-location')]");
                        if (cNode != null)
                        {
                            City = CleanString(cNode.InnerText);
                        }
                    }

                    node = htmlNode.SelectSingleNode(".//*[contains(@class,'-salary')]");
                    if (node != null)
                    {
                        SalaryRange = Regex.Replace(CleanString(node.InnerText), "[\n\r* *]", "");
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
                try
                {
                    var node = htmlNode.SelectSingleNode(".//*[@class='-meta']").SelectSingleNode(".//span[@class='-text']");
                    if (node != null)
                    {
                        EmploymentType = CleanString(node.InnerText);
                    }

                    node = htmlNode.SelectSingleNode(".//*[@class='post-text _job-text']");
                    if (node != null)
                    {
                        Description = CleanString(Regex.Replace(node.InnerText, "<.*?>", Environment.NewLine));
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