using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Models;

namespace JobSearcher.HtmlParsers
{
    internal class IndeedHtmlParser : HtmlParser
    {
        #region Public Methods

        public override async Task<ListingBase> ParseListingDataAsync(string html, ListingBase listing, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var node = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'jobsearch-JobComponent')]");
                                          if (node != null)
                                          {
                                              listing.ParseRemainingData(node);
                                          }
                                      }
                                      catch (Exception e)
                                      {
                                          // do something useful here
                                      }

                                      return listing;
                                  },
                                  ct);
        }

        public override async Task<IEnumerable<ListingBase>> ParseListingsAsync(string html, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var listings = new HashSet<ListingBase>();
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var nodes = doc.DocumentNode.SelectNodes("//div[contains(@id,'jobResults')]/a[contains(@class, 'tapItem')]");
                                          if (nodes != null)
                                          {
                                              foreach (var node in nodes)
                                              {
                                                  var listing = new IndeedListing();
                                                  listing.ParseInitialNode(node);
                                                  listings.Add(listing);
                                              }
                                          }
                                      }
                                      catch (Exception e)
                                      {
                                          // do something useful here
                                      }

                                      return listings;
                                  },
                                  ct);
        }

        #endregion
    }
}