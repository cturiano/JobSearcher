using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;

namespace JobSearcher.HtmlParsers
{
    internal class UnknownHtmlParser : IHtmlParser
    {
        #region Public Methods

        public async Task<ListingBase> ParseListingDataAsync(string html, ListingBase listing, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<ListingBase> ParseListingReplyAsync(string html, ListingBase listing, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListingBase>> ParseListingsAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Uri>> ParseListItemsAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<string> ParseReplyLinkAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}