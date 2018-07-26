using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;

namespace JobSearcher.Interfaces
{
    internal interface IHtmlParser
    {
        #region Public Methods

        /// <summary>
        ///     Parses Salary Range, Description & Employment Type from the given html and assigns them to the appropriate property
        ///     of the given listing
        /// </summary>
        /// <param name="html">The html to parse.</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <param name="listing">The listing to which to assign the values.</param>
        /// <returns>The same listing with the new values populated</returns>
        Task<ListingBase> ParseListingDataAsync(string html, ListingBase listing, CancellationToken ct);

        /// <summary>
        ///     Parses the Reply email information from the given html into the given listing.
        /// </summary>
        /// <param name="html">The html to parse.</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <param name="listing">The listing to which to assign the values.</param>
        /// <returns>The same listing with the new values populated</returns>
        Task<ListingBase> ParseListingReplyAsync(string html, ListingBase listing, CancellationToken ct);

        /// <summary>
        ///     Parses all listings from the given html
        /// </summary>
        /// <param name="html">The html to parse.</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns>A <see cref="IEnumerable{T}" /> containing the parsed <see cref="ListingBase" /></returns>
        Task<IEnumerable<ListingBase>> ParseListingsAsync(string html, CancellationToken ct);

        /// <summary>
        ///     Parses the city urls from the the given html
        /// </summary>
        /// <param name="html">The html to parse.</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns>A <see cref="IEnumerable{T}" /></returns>
        Task<IEnumerable<Uri>> ParseListItemsAsync(string html, CancellationToken ct);

        /// <summary>
        ///     Parses the reply information from the given html
        /// </summary>
        /// <param name="html">The html to parse</param>
        /// <param name="ct">The <see cref="CancellationToken" /> used to cancel the task.</param>
        /// <returns>A string containing the reply information.</returns>
        Task<string> ParseReplyLinkAsync(string html, CancellationToken ct);

        #endregion
    }
}