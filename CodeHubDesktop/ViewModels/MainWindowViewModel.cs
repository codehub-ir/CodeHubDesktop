using Prism.Mvvm;
using System.Windows;

namespace CodeHubDesktop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "CodeHub";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private FlowDirection _MainFlowDirection;
        public FlowDirection MainFlowDirection
        {
            get => _MainFlowDirection;
            set => SetProperty(ref _MainFlowDirection, value);
        }

        public MainWindowViewModel()
        {
            SetFlowDirection();
        }

        public FlowDirection SetFlowDirection()
        {
            return MainFlowDirection = GlobalData.Config.Lang.Equals("fa-IR") ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
    }
}
