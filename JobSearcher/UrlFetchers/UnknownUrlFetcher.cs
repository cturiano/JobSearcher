using System;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class UnknownUrlFetcher : UrlFetcher
    {
        #region Constructors

        public UnknownUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.CraigslistBaseUrl, Resources.CraigslistQueryString, webClientFactory)
        {
            Parser = new UnknownHtmlParser();
        }

        #endregion
    }
}