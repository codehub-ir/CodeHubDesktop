using MahApps.Metro.Controls;
using ModernWpf.MahApps.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CodeHubDesktop.ViewModels
{
    public class LeftMainContentViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<HamburgerMenuEx> SwitchItemCmd { get; private set; }
        public DelegateCommand<HamburgerMenuEx> SwitchItemOptionCmd { get; private set; }
        public LeftMainContentViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SwitchItemCmd = new DelegateCommand<HamburgerMenuEx>(Switch);
            SwitchItemOptionCmd = new DelegateCommand<HamburgerMenuEx>(SwitchOption);
        }

        private void SwitchOption(HamburgerMenuEx e)
        {
            if (e.SelectedOptionsItem is HamburgerMenuIconItem item)
            {
                NavigateToRegion(item.Tag.ToString());
            }
        }

        private void Switch(HamburgerMenuEx e)
        {
            if (e.SelectedItem is HamburgerMenuIconItem item)
            {
                NavigateToRegion(item.Tag.ToString());
            }
        }

        internal void NavigateToRegion(string tag)
        {
            if (tag != null)
            {
                _regionManager.RequestNavigate("ContentRegion", tag);
            }
        }
    }
}
