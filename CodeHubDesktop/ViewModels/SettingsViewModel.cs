using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace CodeHubDesktop.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public static SettingsViewModel Instance;
        private HorizontalAlignment _ContentAlignment;
        public HorizontalAlignment ContentAlignment
        {
            get { return _ContentAlignment; }
            set { SetProperty(ref _ContentAlignment, value); }
        }

        private bool _GetIsCheckedToggle;
        public bool GetIsCheckedToggle
        {
            get { return _GetIsCheckedToggle; }
            set { SetProperty(ref _GetIsCheckedToggle, value); }
        }

        public DelegateCommand<object> StoreSnippetCommand { get; private set; }

        public SettingsViewModel()
        {
            Instance = this;
            StoreSnippetCommand = new DelegateCommand<object>(OnStoreSnippet);

            GetIsCheckedToggle = GlobalData.Config.StoreSnippet;
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
