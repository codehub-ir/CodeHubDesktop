using CodeHubDesktop.Views;
using HandyControl.Data;
using HandyControl.Tools;
using ModernWpf;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Media;

namespace CodeHubDesktop
{
    public partial class App
    {
        public App()
        {
            GlobalData.Init();
            ConfigHelper.Instance.SetLang(GlobalData.Config.Lang);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container.Resolve<IRegionManager>().RequestNavigate("ContentRegion", "CreateSnippet");
        }

        protected override Window CreateShell()
        {
            if (GlobalData.Config.Skin != SkinType.Default)
            {
                UpdateSkin(GlobalData.Config.Skin);
            }
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<About>();
            containerRegistry.RegisterForNavigation<CheckUpdate>();
            containerRegistry.RegisterForNavigation<Settings>();
            containerRegistry.RegisterForNavigation<CreateSnippet>();
            containerRegistry.RegisterForNavigation<SnippetHistory>();
            containerRegistry.RegisterForNavigation<LeftMainContent>();
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

            if (skin.Equals(SkinType.Dark))
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
            else if (skin.Equals(SkinType.Default))
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                ThemeManager.Current.AccentColor = ResourceHelper.GetResource<Color>(ResourceToken.VioletColor);

            }

        }
    }
}
