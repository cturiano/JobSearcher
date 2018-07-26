using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;

namespace JobSearcher.Interfaces
{
    internal interface ISiteSearcher
    {
        #region Properties

        IUrlFetcher Fetcher { get; }

        HashSet<ListingBase> Listings { get; }

        string SearchString { get; }

        #endregion

        #region Public Methods

        Task<IEnumerable<ListingBase>> SearchAsync(CancellationToken ct);

        #endregion
    }
}