using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using JobSearcher.Abstract;
using JobSearcher.Factories;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Services;
using JobSearcher.Utils;
using Microsoft.Win32;

namespace JobSearcher.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        #region Fields

        private readonly object _lock = new object();
        private RelayCommand _applyCommand;
        private RelayCommand _cancelCommand;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;
        private RelayCommand _clearCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _hyperlinkCommand;
        private bool _isSearching;
        private ConcurrentBag<ListingBase> _listings;
        private RelayCommand _onClosingCommand;
        private RelayCommand _onLoadedCommand;
        private RelayCommand _searchCommand;

        #endregion

        #region Constructors

        public MainViewModel(IDialogService dialogService)
        {
            _listings = new ConcurrentBag<ListingBase>();
            Listings = new ObservableCollection<ListingBase>();
            BindingOperations.CollectionRegistering += (sender, args) => { BindingOperations.EnableCollectionSynchronization(Listings, _lock); };
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            SearchHistory = new ObservableCollection<string>();
            DialogService = dialogService;
            SearchSite = WebSource.All;
        }

        #endregion

        #region Properties

        public ICommand ApplyCommand => _applyCommand ?? (_applyCommand = new RelayCommand(async param => await ApplyAsync(), param => CanApply()));

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(param => Cancel(), param => CanCancel()));

        public ICommand ClearCommand => _clearCommand ?? (_clearCommand = new RelayCommand(param => Clear(), param => CanClear()));

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(async param => await DeleteAsync(), param => CanDelete()));

        public IDialogService DialogService
        {
            get => Get<IDialogService>();
            set => Set(value);
        }

        public ICommand HyperlinkCommand => _hyperlinkCommand ?? (_hyperlinkCommand = new RelayCommand(HyperLink));

        public ObservableCollection<ListingBase> Listings
        {
            get => Get<ObservableCollection<ListingBase>>();
            set => Set(value);
        }

        public string NewSearchString
        {
            set
            {
                if (SearchString != null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(value))
                {
                    SearchHistory.Add(value);
                    SearchString = value;
                }
            }
        }

        public ICommand OnClosingCommand => _onClosingCommand ?? (_onClosingCommand = new RelayCommand(async param => await OnClosing()));

        public ICommand OnLoadedCommand => _onLoadedCommand ?? (_onLoadedCommand = new RelayCommand(async param => await OnLoaded()));

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new RelayCommand(async param => await SearchAsync(), param => CanSearch()));

        public ObservableCollection<string> SearchHistory
        {
            get => Get<ObservableCollection<string>>();
            set => Set(value);
        }

        public WebSource SearchSite
        {
            get => Get<WebSource>();
            set => Set(value);
        }

        public string SearchString
        {
            get => Get<string>();
            set => Set(value);
        }

        #endregion

        #region Private Methods

        private async Task ApplyAsync()
        {
            var vm = new CredentialsViewModel("Enter GMail credentials.", "Credentials");
            var result = DialogService.OpenDialog(vm, null);

            if (result == DialogResult.OK)
            {
                var client = new SmtpClient
                             {
                                 Port = 587,
                                 EnableSsl = true,
                                 DeliveryMethod = SmtpDeliveryMethod.Network,
                                 UseDefaultCredentials = false,
                                 Credentials = new NetworkCredential(vm.Credentials.UserName, vm.Credentials.Password),
                                 Host = "smtp.gmail.com"
                             };

                var ofvm = new OpenFileViewModel("Select Files To Attach");
                var fileResults = DialogService.ShowFileDialog(ofvm, Application.Current.MainWindow);

                await Task.Run(() =>
                               {
                                   Parallel.ForEach(Listings.Where(l => l.Selected && l.ApplyEmail != null),
                                                    listing =>
                                                    {
                                                        var mail = new MailMessage(new MailAddress(vm.Credentials.UserName), listing.ApplyEmail.ToAddress)
                                                                   {
                                                                       Subject = listing.ApplyEmail.Subject,
                                                                       Body = Resources.CoverLetter + Environment.NewLine + Environment.NewLine + listing.ApplyEmail.Body
                                                                   };

                                                        if (fileResults == DialogResult.OK && ofvm.Paths != null && ofvm.Paths.Count > 0)
                                                        {
                                                            foreach (var path in ofvm.Paths)
                                                            {
                                                                mail.Attachments.Add(new Attachment(path));
                                                            }
                                                        }

                                                        try
                                                        {
                                                            client.Send(mail);
                                                        }
                                                        catch (SmtpException se)
                                                        {
                                                            throw new AggregateException(se.Message);
                                                        }
                                                    });
                               },
                               _cancellationToken);
            }
        }

        private bool CanApply()
        {
            return !_isSearching && Listings.Any(l => l.Selected);
        }

        private bool CanCancel()
        {
            return _isSearching && _cancellationTokenSource != null && _cancellationToken.CanBeCanceled;
        }

        private void Cancel()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = new CancellationTokenSource();
                    _cancellationToken = _cancellationTokenSource.Token;
                }
            }
            catch (TaskCanceledException)
            {
                // User told it to cancel, so just bury this exception
            }
        }

        private bool CanClear()
        {
            return !_isSearching && Listings.Count > 0;
        }

        private bool CanDelete()
        {
            return !_isSearching && Listings.Any(l => l.Selected);
        }

        private bool CanSearch()
        {
            return !_isSearching;
        }

        private void Clear()
        {
            Cancel();
            lock (_lock)
            {
                Listings.Clear();
                while (!_listings.IsEmpty)
                {
                    _listings.TryTake(out _);
                }
            }

            GC.Collect();
        }

        private async Task DeleteAsync()
        {
            await Task.Factory.StartNew(() =>
                                        {
                                            foreach (var l in Listings.Reverse())
                                            {
                                                if (l.Selected)
                                                {
                                                    lock (_lock)
                                                    {
                                                        Listings.Remove(l);
                                                        _listings = new ConcurrentBag<ListingBase>(Listings);
                                                    }
                                                }
                                            }
                                        },
                                        _cancellationToken);
        }

        private static void HyperLink(object parameter)
        {
            if (parameter is Uri url)
            {
                using (var userChoiceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
                {
                    var browserType = BrowserType.Unknown;
                    var progIdValue = userChoiceKey?.GetValue("Progid");
                    if (progIdValue != null)
                    {
                        switch (progIdValue.ToString().ToLower())
                        {
                            case "ie.http":
                                browserType = BrowserType.InternetExplorer;
                                break;
                            case "firefoxurl":
                                browserType = BrowserType.Firefox;
                                break;
                            case "chromehtml":
                                browserType = BrowserType.Chrome;
                                break;
                            case "opera.protocol":
                                browserType = BrowserType.Opera;
                                break;
                        }
                    }

                    switch (browserType)
                    {
                        case BrowserType.Firefox:
                            Process.Start("firefox.exe", "-private-window " + url.AbsoluteUri);
                            break;
                        case BrowserType.Opera:
                            Process.Start("opera.exe", "-private " + url.AbsoluteUri);
                            break;
                        case BrowserType.Chrome:
                            Process.Start("chrome.exe", "-incognito " + url.AbsoluteUri);
                            break;
                        default:
                            Process.Start("iexplore.exe", "-private " + url.AbsoluteUri);
                            break;
                    }
                }
            }
        }

        private async Task OnClosing()
        {
            // ReSharper disable once MethodSupportsCancellation
            await Task.Run(() =>
                           {
                               //Write search history to resources
                               using (var resXWriter = new ResXResourceWriter(@".\Resources.resx"))
                               {
                                   using (var writer = new StringWriter())
                                   {
                                       var serializer = new XmlSerializer(typeof(ObservableCollection<string>));
                                       serializer.Serialize(writer, SearchHistory);
                                       resXWriter.AddResource("SearchHistory", writer.ToString());
                                       writer.Close();
                                   }
                               }
                           });
        }

        private async Task OnLoaded()
        {
            await Task.Run(() =>
                           {
                               //Read search history from resources 
                               var hxXml = Resources.ResourceManager.GetString("SearchHistory", CultureInfo.CurrentCulture);
                               if (!string.IsNullOrEmpty(hxXml))
                               {
                                   var serializer = new XmlSerializer(typeof(ObservableCollection<string>));
                                   using (var reader = new StringReader(hxXml))
                                   {
                                       SearchHistory = (ObservableCollection<string>)serializer.Deserialize(reader);
                                   }

                                   if (SearchHistory.Count > 0)
                                   {
                                       SearchString = SearchHistory[0];
                                   }
                               }
                           },
                           _cancellationToken);
        }

        private async Task SearchAsync()
        {
            _isSearching = true;
            try
            {
                if (SearchSite == WebSource.All)
                {
                    Parallel.ForEach(Enum.GetValues(typeof(WebSource)).Cast<WebSource>(), async source => { await SiteSearchAsync(source); });
                }
                else
                {
                    await SiteSearchAsync(SearchSite);
                }

                lock (_lock)
                {
                    foreach (var listing in _listings)
                    {
                        Listings.Add(listing);
                    }
                }
            }
            catch (Exception e)
            {
                var vm = new OkViewModel(e.Message, "Search Error");
                DialogService.OpenDialog(vm, null);
            }
            finally
            {
                _isSearching = false;
            }
        }

        private async Task SiteSearchAsync(WebSource source)
        {
            var searcher = SiteSearcherFactory.MakeSiteSearcher(source, SearchString);
            var listings = await searcher.SearchAsync(_cancellationToken);

            lock (_lock)
            {
                foreach (var listing in listings)
                {
                    _listings.Add(listing);
                }
            }
        }

        #endregion
    }
}