using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Interfaces;
using JobSearcher.Utils;
using JobSearcher.ViewModels;

namespace JobSearcher.Abstract
{
    internal abstract class SiteSearcher : BindableBase, ISiteSearcher
    {
        #region Fields

        protected IDialogService DialogService;

        #endregion

        #region Constructors

        protected SiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory)
        {
            SearchString = searchString;
            Listings = new HashSet<ListingBase>();
            Fetcher = urlFetcherFactory(webSource, searchString);
            DialogService = dialogService;
        }

        #endregion

        #region Properties

        public IUrlFetcher Fetcher
        {
            get => Get<IUrlFetcher>();
            private set => Set(value);
        }

        public HashSet<ListingBase> Listings
        {
            get => Get<HashSet<ListingBase>>();
            protected set => Set(value);
        }

        public string SearchString
        {
            get => Get<string>();
            protected set => Set(value);
        }

        public string SiteUrl
        {
            get => Get<string>();
            protected set => Set(value);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc cref="SiteSearcher" />
        public virtual async Task<IEnumerable<ListingBase>> SearchAsync(CancellationToken ct)
        {
            try
            {
                await ProcessURL(new Uri(new Uri(SiteUrl), SearchString), ct);
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

        #region Protected Methods

        /// <inheritdoc cref="SiteSearcher" />
        protected virtual async Task ProcessURL(Uri url, CancellationToken ct)
        {
            var listings = await Fetcher.GetListingsAsync(url, ct);

            if (listings != null)
            {
                foreach (var listing in listings)
                {
                    // get the description and the apply email
                    await Fetcher.GetRemainingListingDataAsync(listing, ct);
                    Listings.Add(listing);
                }
            }
        }

        #endregion
    }
}