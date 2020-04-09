using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace CodeHubDesktop.ViewModels
{
    public class DialogServiceViewModel : BindableBase, IDialogAware
    {
        private string _SnippetScript;
        public string SnippetScript
        {
            get => _SnippetScript;
            set => SetProperty(ref _SnippetScript, value);
        }

        private string _Language;
        public string Language
        {
            get => _Language;
            set => SetProperty(ref _Language, value);
        }

        private string _title = "Script";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            string[] Message = parameters.GetValue<string>("message").Split(",");
            SnippetScript = Message[0];
            Language = Message[1];
        }
    }
}
