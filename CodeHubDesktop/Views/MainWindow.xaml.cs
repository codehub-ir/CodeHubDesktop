using CodeHubDesktop.DynamicLanguage;
using CodeHubDesktop.MultiLanguage.Language;
using CodeHubDesktop.ViewModels;
using HandyControl.Controls;
using HandyControl.Data;
using System.Windows;
using System.Windows.Controls;

namespace CodeHubDesktop.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Change Skin and Language
        private void ButtonConfig_OnClick(object sender, RoutedEventArgs e)
        {
            PopupConfig.IsOpen = true;
        }

        private void ButtonSkins_OnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.Tag is SkinType tag)
            {
                PopupConfig.IsOpen = false;
                if (tag.Equals(GlobalData.Config.Skin))
                {
                    return;
                }

                GlobalData.Config.Skin = tag;
                GlobalData.Save();
                ((App)Application.Current).UpdateSkin(tag);
            }
        }
        private void ButtonLangs_OnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.Tag is string tag)
            {
                PopupConfig.IsOpen = false;
                if (tag.Equals(GlobalData.Config.Lang))
                {
                    return;
                }

                Growl.Ask(Lang.ResourceManager.GetString("ChangeLanguage"), b =>
                {
                    if (!b)
                    {
                        return true;
                    }

                    GlobalData.Config.Lang = tag;
                    GlobalData.Save();
                    TranslationSource.Instance.Language = tag;
                    ((MainWindowViewModel)(DataContext)).SetFlowDirection();
                    return true;
                });
            }
        }
        #endregion
    }
}
