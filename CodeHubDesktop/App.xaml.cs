using CodeHubDesktop.ViewModels;
using CodeHubDesktop.Views;
using HandyControl.Data;
using HandyControl.Tools;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;

namespace CodeHubDesktop
{
    public partial class App
    {
        public App()
        {
            GlobalData.Init();
            ConfigHelper.Instance.SetLang(GlobalData.Config.Lang);
            if (GlobalData.Config.Skin != SkinType.Default)
            {
                UpdateSkin(GlobalData.Config.Skin);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container.Resolve<IRegionManager>().RequestNavigate("ContentRegion", "CreateSnippet");
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DialogService, DialogServiceViewModel>();

            containerRegistry.RegisterForNavigation<About>();
            containerRegistry.RegisterForNavigation<CheckUpdate>();
            containerRegistry.RegisterForNavigation<Settings>();
            containerRegistry.RegisterForNavigation<CreateSnippet>();
            containerRegistry.RegisterForNavigation<SnippetHistory>();
            containerRegistry.RegisterForNavigation<SnippetOnline>();
            containerRegistry.RegisterForNavigation<Admin>();
        }
        internal void UpdateSkin(SkinType skin)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/HandyControl;component/Themes/Skin{skin.ToString()}.xaml")
            });
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
        }
    }
}
