using JobSearcher.Factories;
using JobSearcher.UrlFetchers;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.FactoriesTests
{
    [TestFixture]
    internal class UrlFetcherFactoryTests
    {
        [TestCase(WebSource.Craigslist)]
        [TestCase(WebSource.Dice)]
        [TestCase(WebSource.Indeed)]
        [TestCase(WebSource.All)]
        [TestCase(WebSource.Monster)]
        [TestCase(WebSource.None)]
        [TestCase(WebSource.StackOverflow)]
        public void MakeUrlFetcherTest(WebSource source)
        {
            try
            {
                var fetcher = UrlFetcherFactory.MakeUrlFetcher(source, "");

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (source)
                {
                    case WebSource.Craigslist:
                        Assert.IsInstanceOf<CraigslistUrlFetcher>(fetcher);
                        break;
                    case WebSource.Indeed:
                        Assert.IsInstanceOf<IndeedUrlFetcher>(fetcher);
                        break;
                    case WebSource.Monster:
                        Assert.IsInstanceOf<MonsterUrlFetcher>(fetcher);
                        break;
                    case WebSource.StackOverflow:
                        Assert.IsInstanceOf<StackOverflowUrlFetcher>(fetcher);
                        break;
                    case WebSource.Dice:
                        Assert.IsInstanceOf<DiceUrlFetcher>(fetcher);
                        break;
                    case WebSource.None:
                        Assert.IsInstanceOf<UnknownUrlFetcher>(fetcher);
                        break;
                }
            }
            catch
            {
                Assert.AreEqual(WebSource.All, source);
            }
        }
    }
}