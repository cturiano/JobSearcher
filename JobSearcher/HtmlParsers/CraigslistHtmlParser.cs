using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Models;

namespace JobSearcher.HtmlParsers
{
    internal class CraigslistHtmlParser : IHtmlParser
    {
        #region Static Fields and Constants

        private const string Href = "href";

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task<ListingBase> ParseListingDataAsync(string html, ListingBase listing, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var node = doc.DocumentNode.SelectSingleNode("//section[contains(@class,'userbody')]");
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

        /// <inheritdoc />
        public async Task<ListingBase> ParseListingReplyAsync(string html, ListingBase listing, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var node = doc.DocumentNode.SelectSingleNode("//p[contains(@class,'reply-email-address')]");
                                          if (node != null)
                                          {
                                              node = node.SelectSingleNode("./a");
                                              if (node != null)
                                              {
                                                  listing.ApplyEmail = new EmailInfo(node.Attributes[Href].Value);
                                              }
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

        /// <inheritdoc />
        public async Task<IEnumerable<ListingBase>> ParseListingsAsync(string html, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var listings = new HashSet<ListingBase>();
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var nodes = doc.DocumentNode.SelectNodes("//ul/li[contains(@class,'result-row')]/p[contains(@class,'result-info')]");
                                          if (nodes != null)
                                          {         
                                              foreach (var node in nodes)
                                              {
                                                  var listing = new CraigslistListing();
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

        /// <inheritdoc />
        public async Task<IEnumerable<Uri>> ParseListItemsAsync(string html, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var urls = new List<Uri>();
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var liNodes = doc.DocumentNode.SelectNodes("//ul/li");
                                          if (liNodes != null)
                                          {
                                              urls.AddRange(from node in liNodes select node.SelectSingleNode("./a")?.Attributes[Href]?.Value into url where !string.IsNullOrEmpty(url) && !url.Contains("www") select new Uri(url));
                                          }
                                      }
                                      catch (Exception e)
                                      {
                                          // do something useful here
                                      }

                                      return urls;
                                  },
                                  ct);
        }

        /// <inheritdoc />
        public async Task<string> ParseReplyLinkAsync(string html, CancellationToken ct)
        {
            return await Task.Run(() =>
                                  {
                                      var doc = new HtmlDocument();
                                      doc.LoadHtml(html);
                                      try
                                      {
                                          var a = doc.DocumentNode.SelectSingleNode("//a[contains(@id,'replylink')]");
                                          if (a != null)
                                          {
                                              return a.Attributes[Href].Value;
                                          }
                                      }
                                      catch (Exception e)
                                      {
                                          // do something useful here
                                      }

                                      return null;
                                  },
                                  ct);
        }

        #endregion
    }
}