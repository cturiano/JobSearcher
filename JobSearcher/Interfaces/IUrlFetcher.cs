using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;

namespace JobSearcher.Interfaces
{
    internal interface IUrlFetcher
    {
        #region Properties

        string URL { get; }

        string UrlQueryString { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Fetches all the listings for the given searchString contained by the given URI
        /// </summary>
        /// <param name="url">The <see cref="Uri" /> to query.</param>
        /// <param name="searchString">The string for which to query.</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns></returns>
        Task<IEnumerable<ListingBase>> GetListingsAsync(Uri url, CancellationToken ct);

        /// <summary>
        ///     Fetches the data not available in the top level (city-level) listing by fetching the listing itself.
        /// </summary>
        /// <param name="listing">The listing whose remaining data to popululate</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns></returns>
        Task<ListingBase> GetRemainingListingDataAsync(ListingBase listing, CancellationToken ct);

        /// <summary>
        ///     Fetches the urls for all the cities on the main Craigslist page
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns></returns>
        Task<IEnumerable<Uri>> GetUrlsAsync(CancellationToken ct);

        #endregion
    }
}