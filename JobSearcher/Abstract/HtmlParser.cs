using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Interfaces;

namespace JobSearcher.Abstract
{
    internal abstract class HtmlParser : IHtmlParser
    {
        #region Public Methods

        public virtual Task<ListingBase> ParseListingDataAsync(string html, ListingBase listing, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ListingBase> ParseListingReplyAsync(string html, ListingBase listing, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<ListingBase>> ParseListingsAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<Uri>> ParseListItemsAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> ParseReplyLinkAsync(string html, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}