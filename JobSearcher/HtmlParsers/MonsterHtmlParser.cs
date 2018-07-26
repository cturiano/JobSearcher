using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Models;

namespace JobSearcher.HtmlParsers
{
    internal class MonsterHtmlParser : HtmlParser
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
                                          var node = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'JobViewContent')]");
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
                                          var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'js_result_container clearfix primary')]");
                                          if (nodes != null)
                                          {
                                              foreach (var node in nodes)
                                              {
                                                  var listing = new MonsterListing();
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