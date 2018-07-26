using System;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;

namespace JobSearcherTest.Mocks
{
    internal class DiceUrlFetcherMock : DiceUrlFetcher
    {
        #region Constructors

        public DiceUrlFetcherMock(Func<IWebClient> webClientFactory) : base(webClientFactory)
        {
        }

        #endregion

        #region Properties

        public IHtmlParser MyParser => Parser;

        #endregion
    }
}