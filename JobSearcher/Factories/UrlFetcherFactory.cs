using System;
using System.Net;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;
using JobSearcher.Utils;

namespace JobSearcher.Factories
{
    internal static class UrlFetcherFactory
    {
        #region Public Methods

        public static IUrlFetcher MakeUrlFetcher(WebSource webSource, string leaveEmpty)
        {
            IWebClient Func() => new SystemWebClient {Headers = {[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Mobile; Windows Phone 8.1; Android 4.0; ARM; Trident/7.0; Touch; rv:11.0; IEMobile/11.0; NOKIA; Lumia 520) like iPhone OS 7_0_3 Mac OS X AppleWebKit/537 (KHTML, like Gecko) Mobile Safari/537"}};

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (webSource)
            {
                case WebSource.Craigslist:
                    return new CraigslistUrlFetcher(Func);
                case WebSource.Indeed:
                    return new IndeedUrlFetcher(Func);
                case WebSource.Monster:
                    return new MonsterUrlFetcher(Func);
                case WebSource.StackOverflow:
                    return new StackOverflowUrlFetcher(Func);
                case WebSource.Dice:
                    return new DiceUrlFetcher(Func);
                case WebSource.None:
                    return new UnknownUrlFetcher(Func);
                default:
                    throw new ArgumentOutOfRangeException(nameof(webSource), webSource, null);
            }
        }

        #endregion
    }
}