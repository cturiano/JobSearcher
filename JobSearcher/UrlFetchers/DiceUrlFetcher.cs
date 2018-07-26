using System;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcher.Properties;

namespace JobSearcher.UrlFetchers
{
    internal class DiceUrlFetcher : UrlFetcher
    {
        #region Constructors

        public DiceUrlFetcher(Func<IWebClient> webClientFactory) : base(Resources.DiceBaseUrl, Resources.DiceQueryString, webClientFactory)
        {
            Parser = new DiceHtmlParser();
        }

        #endregion
    }
}