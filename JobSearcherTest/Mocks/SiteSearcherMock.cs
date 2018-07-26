using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Utils;

namespace JobSearcherTest.Mocks
{
    internal class SiteSearcherMock : SiteSearcher
    {
        #region Constructors

        public SiteSearcherMock(WebSource source, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(source, searchString, dialogService, urlFetcherFactory)
        {
        }

        #endregion

        #region Properties

        public IDialogService MyDialogService => DialogService;

        public IUrlFetcher MyFetcher => Fetcher;

        #endregion

        #region Public Methods

        public override Task<IEnumerable<ListingBase>> SearchAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}