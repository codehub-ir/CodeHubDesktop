using CodeHubDesktop.Data.Services;
using CodeHubDesktop.Models;
using HandyControl.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace CodeHubDesktop.ViewModels
{
    public class CreateSnippetViewModel : BindableBase
    {
        private string _Error;
        public string Error
        {
            get => _Error;
            set => SetProperty(ref _Error, value);
        }
        private string _SnippetUrl;
        public string SnippetUrl
        {
            get => _SnippetUrl;
            set => SetProperty(ref _SnippetUrl, value);
        }

        private bool _IsEnabled = true;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetProperty(ref _IsEnabled, value);
        }

        private string _Snippet;
        public string Snippet
        {
            get => _Snippet;
            set => SetProperty(ref _Snippet, value);
        }

        private string _Subject;
        public string Subject
        {
            get => _Subject;
            set => SetProperty(ref _Subject, value);
        }

        private string _Detail;
        public string Detail
        {
            get => _Detail;
            set => SetProperty(ref _Detail, value);
        }

        private LanguageModel _SelectedCode;
        public LanguageModel SelectedCode
        {
            get => _SelectedCode;
            set => SetProperty(ref _SelectedCode, value);
        }

        private ObservableCollection<LanguageModel> _LanguageList = new ObservableCollection<LanguageModel>();
        public ObservableCollection<LanguageModel> LanguageList
        {
            get => _LanguageList;
            set => SetProperty(ref _LanguageList, value);
        }

        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }

        public CreateSnippetViewModel()
        {
            ClearCommand = new DelegateCommand(OnClear);
            CreateCommand = new DelegateCommand(OnCreateSnippet);
            FillComboBox();
        }

        private async void OnCreateSnippet()
        {
            try
            {
                IsEnabled = false;
                CreateSnippetModel snippet = new CreateSnippetModel
                {
                    title = Subject,
                    detail = Detail,
                    language = SelectedCode.Name ?? "csharp",
                    script = Snippet,
                    error = Error
                };

                string json = JsonConvert.SerializeObject(snippet);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                string url = GlobalData.Config.APIBaseAddress;

                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                GetSnippetModel parse = JsonConvert.DeserializeObject<GetSnippetModel>(result);
                SnippetUrl = parse.link;
                if (GlobalData.Config.StoreSnippet)
                {
                    SnippetsModel entity = new SnippetsModel
                    {
                        Title = parse.title,
                        Detail = parse.detail,
                        Language = parse.language,
                        Link = parse.link,
                        PubDate = parse.pub_date,
                        Script = parse.script,
                        SId = parse.SID,
                        Error = parse.error
                    };
                    IDataService<SnippetsModel> dataService = new GenericDataService<SnippetsModel>();
                    await dataService.CreateSnippet(entity);
                }
                IsEnabled = true;
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        private void OnClear()
        {
            Error = Snippet = Subject = Detail = string.Empty;
        }

        internal void FillComboBox()
        {
            string languageResource = Properties.Resources.LanguageList;
            string[] allLines = languageResource.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in allLines)
            {
                string[] line = item.Split(",");
                LanguageList.Add(new LanguageModel { DisplayName = line[0].Replace("_", " "), Name = line[1] });

            }
        }
    }
}
