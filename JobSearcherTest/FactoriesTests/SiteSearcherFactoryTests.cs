using System.Collections.Generic;
using JobSearcher.Abstract;
using JobSearcher.Factories;
using JobSearcher.SiteSearchers;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.FactoriesTests
{
    [TestFixture]
    internal class SiteSearcherFactoryTests
    {
        [TestCase(WebSource.Craigslist)]
        [TestCase(WebSource.Dice)]
        [TestCase(WebSource.Indeed)]
        [TestCase(WebSource.All)]
        [TestCase(WebSource.Monster)]
        [TestCase(WebSource.None)]
        [TestCase(WebSource.StackOverflow)]
        public void MakeSiteSearcherTest(WebSource source)
        {
            try
            {
                const string ss = "searchString";
                var fetcher = SiteSearcherFactory.MakeSiteSearcher(source, ss);

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (source)
                {
                    case WebSource.Craigslist:
                        Assert.IsInstanceOf<CraigslistSiteSearcher>(fetcher);
                        break;
                    case WebSource.Indeed:
                        Assert.IsInstanceOf<IndeedSiteSearcher>(fetcher);
                        break;
                    case WebSource.Monster:
                        Assert.IsInstanceOf<MonsterSiteSearcher>(fetcher);
                        break;
                    case WebSource.StackOverflow:
                        Assert.IsInstanceOf<StackOverflowSiteSearcher>(fetcher);
                        break;
                    case WebSource.Dice:
                        Assert.IsInstanceOf<DiceSiteSearcher>(fetcher);
                        break;
                    case WebSource.None:
                        Assert.IsInstanceOf<UnknownSiteSearcher>(fetcher);
                        break;
                }

                Assert.AreEqual(ss, fetcher.SearchString);
                CollectionAssert.AreEqual(new List<ListingBase>(), fetcher.Listings);
            }
            catch
            {
                Assert.AreEqual(WebSource.All, source);
            }
        }
    }
}