using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace CodeHubDesktop.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private string _APIUrlText;
        public string APIUrlText
        {
            get => _APIUrlText;
            set
            {
                GlobalData.Config.APIBaseAddress = value;
                GlobalData.Save();
                SetProperty(ref _APIUrlText, value);
            }
        }

        private HorizontalAlignment _ContentAlignment;
        public HorizontalAlignment ContentAlignment
        {
            get => _ContentAlignment;
            set => SetProperty(ref _ContentAlignment, value);
        }

        private bool _GetIsCheckedToggle;
        public bool GetIsCheckedToggle
        {
            get => _GetIsCheckedToggle;
            set => SetProperty(ref _GetIsCheckedToggle, value);
        }

        public DelegateCommand<object> StoreSnippetCommand { get; private set; }

        public SettingsViewModel()
        {
            StoreSnippetCommand = new DelegateCommand<object>(OnStoreSnippet);

            GetIsCheckedToggle = GlobalData.Config.StoreSnippet;
            APIUrlText = GlobalData.Config.APIBaseAddress;
        }

        private void OnStoreSnippet(object isChecked)
        {
            if ((bool)isChecked != GlobalData.Config.StoreSnippet)
            {
                GlobalData.Config.StoreSnippet = (bool)isChecked;
                GlobalData.Save();
            }
        }
    }
}
