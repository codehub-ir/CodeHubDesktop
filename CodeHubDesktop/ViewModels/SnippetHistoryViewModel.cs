using CodeHubDesktop.Data.Services;
using CodeHubDesktop.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CodeHubDesktop.ViewModels
{
    public class SnippetHistoryViewModel : BindableBase
    {

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
            set
            {
                SetProperty(ref _LocalSearchText, value);
                ItemsView.Refresh();
            }
        }

        public ICollectionView ItemsView => CollectionViewSource.GetDefaultView(Data);

        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> SelectionChangedCommand { get; set; }
        public DelegateCommand<MouseButtonEventArgs> MouseDoubleClickCommand { get; set; }

        #endregion
        public SnippetHistoryViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            DeleteCommand = new DelegateCommand(OnDelete);
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
    }
}
