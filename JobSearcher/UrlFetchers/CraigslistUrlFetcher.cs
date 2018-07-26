using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class CraigslistUrlFetcher : UrlFetcher
    {
        #region Constructors

        public CraigslistUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.CraigslistBaseUrl, Resources.CraigslistQueryString, webClientFactory)
        {
            Parser = new CraigslistHtmlParser();
        }

        #endregion

        #region Public Methods

        /// <inheritdoc cref="UrlFetcher" />
        public override async Task<ListingBase> GetRemainingListingDataAsync(ListingBase listing, CancellationToken ct)
        {
            return await Task.Run(async () =>
                                  {
                                      try
                                      {
                                          // get the description, employment type & salary range
                                          using (var wc = WebClientFactory())
                                          {
                                              var html = await wc.DownloadStringTaskAsync(listing.ListingUrl);
                                              listing = await Parser.ParseListingDataAsync(html, listing, ct);

                                              // get the reply information
                                              var replyLink = await Parser.ParseReplyLinkAsync(html, ct);
                                              if (!string.IsNullOrEmpty(replyLink))
                                              {
                                                  html = await wc.DownloadStringTaskAsync(new Uri(listing.ListingUrl.GetLeftPart(UriPartial.Authority) + replyLink));
                                                  listing = await Parser.ParseListingReplyAsync(html, listing, ct);
                                              }
                                          }
                                      }
                                      catch (WebException we)
                                      {
                                          // do useful stuff here
                                      }

                                      return listing;
                                  },
                                  ct);
        }

        #endregion
    }
}