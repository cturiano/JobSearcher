using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Interfaces;

namespace JobSearcher.Abstract
{
    internal abstract class UrlFetcher : BindableBase, IUrlFetcher
    {
        #region Fields

        protected IHtmlParser Parser;

        #endregion

        #region Constructors

        protected UrlFetcher(string url, string urlQueryString, Func<IWebClient> webClientFactoryMethod)
        {
            URL = url;
            UrlQueryString = urlQueryString;
            WebClientFactory = webClientFactoryMethod;
        }

        #endregion

        #region Properties

        public string URL
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string UrlQueryString
        {
            get => Get<string>();
            private set => Set(value);
        }

        public Func<IWebClient> WebClientFactory
        {
            get => Get<Func<IWebClient>>();
            private set => Set(value);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc cref="UrlFetcher" />
        public virtual async Task<IEnumerable<ListingBase>> GetListingsAsync(Uri url, CancellationToken ct)
        {
            return await Task.Run(async () =>
                                  {
                                      try
                                      {
                                          using (var wc = WebClientFactory())
                                          {
                                              var html = await wc.DownloadStringTaskAsync(url);
                                              return await Parser.ParseListingsAsync(html, ct);
                                          }
                                      }
                                      catch (WebException we)
                                      {
                                          // do useful stuff here
                                      }

                                      return null;
                                  },
                                  ct);
        }

        /// <inheritdoc cref="UrlFetcher" />
        public virtual async Task<ListingBase> GetRemainingListingDataAsync(ListingBase listing, CancellationToken ct)
        {
            return await Task.Run(async () =>
                                  {
                                      try
                                      {
                                          // get the description
                                          using (var wc = WebClientFactory())
                                          {
                                              var html = await wc.DownloadStringTaskAsync(listing.ListingUrl);
                                              listing = await Parser.ParseListingDataAsync(html, listing, ct);
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

        /// <inheritdoc cref="UrlFetcher" />
        public virtual async Task<IEnumerable<Uri>> GetUrlsAsync(CancellationToken ct)
        {
            return await Task.Run(async () =>
                                  {
                                      using (var wc = WebClientFactory())
                                      {
                                          var html = await wc.DownloadStringTaskAsync(new Uri(URL));
                                          return await Parser.ParseListItemsAsync(html, ct);
                                      }
                                  },
                                  ct);
        }

        #endregion
    }
}