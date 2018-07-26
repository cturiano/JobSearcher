using System;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class StackOverflowUrlFetcher : UrlFetcher
    {
        #region Constructors

        public StackOverflowUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.CraigslistBaseUrl, Resources.CraigslistQueryString, webClientFactory)
        {
            Parser = new StackOverflowHtmlParser();
        }

        #endregion
    }
}