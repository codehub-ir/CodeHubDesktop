using CodeHubDesktop.Models;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Net.Http;
using System.Windows;

namespace CodeHubDesktop.ViewModels
{
    public class SnippetHistoryViewModel : BindableBase
    {
        #region OnlineSnipet
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { SetProperty(ref _SearchText, value); }
        }

        private string _SnippetLanguage;
        public string SnippetLanguage
        {
            get { return _SnippetLanguage; }
            set { SetProperty(ref _SnippetLanguage, value); }
        }

        private string _SnippetDate;
        public string SnippetDate
        {
            get { return _SnippetDate; }
            set { SetProperty(ref _SnippetDate, value); }
        }

        private string _SnippetUrl;
        public string SnippetUrl
        {
            get { return _SnippetUrl; }
            set { SetProperty(ref _SnippetUrl, value); }
        }

        private string _SnippetTitle;
        public string SnippetTitle
        {
            get { return _SnippetTitle; }
            set { SetProperty(ref _SnippetTitle, value); }
        }
        private string _SnippetDetail;
        public string SnippetDetail
        {
            get { return _SnippetDetail; }
            set { SetProperty(ref _SnippetDetail, value); }
        }

        private string _SnippetScript;
        public string SnippetScript
        {
            get { return _SnippetScript; }
            set { SetProperty(ref _SnippetScript, value); }
        }

        private Visibility _PanelVisibility = Visibility.Hidden;
        public Visibility PanelVisibility
        {
            get { return _PanelVisibility; }
            set { SetProperty(ref _PanelVisibility, value); }
        }
        public DelegateCommand<FunctionEventArgs<string>> OnSearchStartedCommand { get; private set; }

        #endregion
        public SnippetHistoryViewModel()
        {
            OnSearchStartedCommand = new DelegateCommand<FunctionEventArgs<string>>(OnSearchStarted);
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
                    url = $"http://codehub.pythonanywhere.com/api/v1/snippet/{SearchText}";
                }
                using var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                var parse = JsonConvert.DeserializeObject<GetSnippetModel>(resp);
                SnippetTitle = parse.title;
                SnippetDetail = parse.detail;
                SnippetDate = parse.pub_date;
                SnippetScript = parse.script;
                SnippetUrl = parse.link;
                SnippetLanguage = parse.language;
                PanelVisibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }

        }
    }
}
