using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Utils;
using JobSearcher.ViewModels;

namespace JobSearcher.SiteSearchers
{
    internal class CraigslistSiteSearcher : SiteSearcher
    {
        #region Constructors

        public CraigslistSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
        }

        #endregion

        #region Public Methods

        /// <inheritdoc cref="SiteSearcher" />
        public override async Task<IEnumerable<ListingBase>> SearchAsync(CancellationToken ct)
        {
            try
            {
                var urls = await Fetcher.GetUrlsAsync(ct);

                var tasks = from url in urls select ProcessURL(new Uri(url, SearchString ?? Resources.CraigslistQueryString), ct);
                await Task.WhenAll(tasks);

                return Listings;
            }
            catch (Exception e)
            {
                var vm = new OkViewModel(e.Message, "Search Error");
                DialogService.OpenDialog(vm, null);
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <inheritdoc cref="SiteSearcher" />
        private async Task ProcessURL(Uri url, CancellationToken ct)
        {
            var listings = await Fetcher.GetListingsAsync(url, ct);
            if (listings != null)
            {
                ListingBase tmpListing = null;
                try
                {
                    foreach (var listing in listings)
                    {
                        tmpListing = listing;
                        // get the description and the apply email
                        await Fetcher.GetRemainingListingDataAsync(listing, ct);
                        Listings.Add(listing);
                    }
                }
                catch (Exception e)
                {
                    var vm = new OkViewModel(e.Message + "\n" + url + "\n" + tmpListing, "Search Error");
                    DialogService.OpenDialog(vm, null);
                }
            }
        }

        #endregion
    }
}