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
    public class SnippetOnlineViewModel : BindableBase
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

        public SnippetOnlineViewModel()
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
