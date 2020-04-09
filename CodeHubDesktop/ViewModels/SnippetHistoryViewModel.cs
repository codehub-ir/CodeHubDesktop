using CodeHubDesktop.Data.Services;
using CodeHubDesktop.Models;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CodeHubDesktop.ViewModels
{
    public class SnippetHistoryViewModel : BindableBase
    {
        #region OnlineSnipet

        private string _Error;
        public string Error
        {
            get => _Error;
            set => SetProperty(ref _Error, value);
        }
        private string _SearchText;
        public string SearchText
        {
            get => _SearchText;
            set => SetProperty(ref _SearchText, value);
        }

        private string _SnippetLanguage;
        public string SnippetLanguage
        {
            get => _SnippetLanguage;
            set => SetProperty(ref _SnippetLanguage, value);
        }

        private string _SnippetDate;
        public string SnippetDate
        {
            get => _SnippetDate;
            set => SetProperty(ref _SnippetDate, value);
        }

        private string _SnippetUrl;
        public string SnippetUrl
        {
            get => _SnippetUrl;
            set => SetProperty(ref _SnippetUrl, value);
        }

        private string _SnippetTitle;
        public string SnippetTitle
        {
            get => _SnippetTitle;
            set => SetProperty(ref _SnippetTitle, value);
        }
        private string _SnippetDetail;
        public string SnippetDetail
        {
            get => _SnippetDetail;
            set => SetProperty(ref _SnippetDetail, value);
        }

        private string _SnippetScript;
        public string SnippetScript
        {
            get => _SnippetScript;
            set => SetProperty(ref _SnippetScript, value);
        }

        private Visibility _PanelVisibility = Visibility.Hidden;
        public Visibility PanelVisibility
        {
            get => _PanelVisibility;
            set => SetProperty(ref _PanelVisibility, value);
        }
        public DelegateCommand<FunctionEventArgs<string>> OnSearchStartedCommand { get; private set; }

        #endregion

        #region LocalSnippet
        private readonly IDialogService _dialogService;
        private int id = 0;
        private string script = string.Empty;
        private string language = string.Empty;

        private bool _IsEnabled = false;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetProperty(ref _IsEnabled, value);
        }
        public ICollectionView ItemsView => CollectionViewSource.GetDefaultView(Data);

        private ObservableCollection<SnippetsModel> _data = new ObservableCollection<SnippetsModel>();
        public ObservableCollection<SnippetsModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }
        private string _LocalSearchText;
        public string LocalSearchText
        {
            get => _LocalSearchText;
            set => SetProperty(ref _LocalSearchText, value);
        }

        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> SelectionChangedCommand { get; set; }
        public DelegateCommand<MouseButtonEventArgs> MouseDoubleClickCommand { get; set; }

        #endregion
        public SnippetHistoryViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            DeleteCommand = new DelegateCommand(OnDelete);
            OnSearchStartedCommand = new DelegateCommand<FunctionEventArgs<string>>(OnSearchStarted);
            SelectionChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(OnSelectionChanged);
            MouseDoubleClickCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseDoubleClick);
            initData();
            ItemsView.Filter = new Predicate<object>(o => Filter(o as SnippetsModel));
        }

        private void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            string message = script + "," + language;
            _dialogService.ShowDialog("DialogService", new DialogParameters($"message={message}"), null);
        }

        private void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is SnippetsModel item)
            {
                IsEnabled = true;
                id = item.Id;
                script = item.Script;
                language = item.Language;
            }
        }

        private bool Filter(SnippetsModel item)
        {
            return LocalSearchText == null
                            || item.Title.IndexOf(LocalSearchText, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private async void initData()
        {
            Data.Clear();
            IDataService<SnippetsModel> dataService = new GenericDataService<SnippetsModel>();
            Data.AddRange(await dataService.GetAllSnippets());
        }

        private async void OnDelete()
        {
            IDataService<SnippetsModel> dataService = new GenericDataService<SnippetsModel>();
            await dataService.DeleteSnippet(id);
            initData();
        }

        private async void OnSearchStarted(FunctionEventArgs<string> e)
        {
            try
            {
                PanelVisibility = Visibility.Hidden;
                if (string.IsNullOrEmpty(SearchText))
                {
                    return;
                }
                string url = string.Empty;
                if (SearchText.StartsWith("http"))
                {
                    url = SearchText.Replace("snippet", "api/v1/snippet");
                }
                else
                {
                    url = GlobalData.Config.APIBaseAddress + SearchText;
                }
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string resp = await response.Content.ReadAsStringAsync();
                GetSnippetModel parse = JsonConvert.DeserializeObject<GetSnippetModel>(resp);
                SnippetTitle = parse.title;
                SnippetDetail = parse.detail;
                SnippetDate = parse.pub_date;
                SnippetScript = parse.script;
                SnippetUrl = parse.link;
                SnippetLanguage = parse.language;
                Error = parse.error;
                PanelVisibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }

        }
    }
}
