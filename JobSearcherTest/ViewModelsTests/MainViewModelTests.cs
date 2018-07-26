using JobSearcher.Services;
using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class MainViewModelTests
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {                              
            var main = new MainViewModel(new DialogService());
            Assert.IsNotNull(main.ApplyCommand);
            Assert.IsNotNull(main.CancelCommand);
            Assert.IsNotNull(main.ClearCommand);
            Assert.IsNotNull(main.DeleteCommand);
            Assert.IsNotNull(main.DialogService);
            Assert.IsNotNull(main.HyperlinkCommand);
            Assert.IsNotNull(main.Listings);    
            Assert.AreEqual(0, main.Listings.Count);                  
            Assert.IsNotNull(main.OnClosingCommand);
            Assert.IsNotNull(main.OnLoadedCommand);
            Assert.IsNotNull(main.SearchCommand);
            Assert.IsNotNull(main.SearchHistory);                     
            Assert.AreEqual(0, main.SearchHistory.Count);
            Assert.IsNotNull(main.SearchSite);    
            Assert.IsTrue(main.SearchString == null);

            main.NewSearchString = "hello";
            Assert.AreEqual("hello", main.SearchString);
            Assert.IsTrue(main.SearchHistory.Contains("hello"));
        }
    }
}