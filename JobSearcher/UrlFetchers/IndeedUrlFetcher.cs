using System;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class IndeedUrlFetcher : UrlFetcher
    {
        #region Constructors

        public IndeedUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.IndeedBaseUrl, Resources.IndeedQueryString, webClientFactory)
        {
            Parser = new IndeedHtmlParser();
        }

        #endregion
    }
}