using System;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class MonsterUrlFetcher : UrlFetcher
    {
        #region Constructors

        public MonsterUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.CraigslistBaseUrl, Resources.CraigslistQueryString, webClientFactory)
        {
            Parser = new MonsterHtmlParser();
        }

        #endregion
    }
}