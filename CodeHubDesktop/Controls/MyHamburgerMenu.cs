using ModernWpf.MahApps.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeHubDesktop.Controls
{
    public class MyHamburgerMenu : HamburgerMenuEx
    {
        #region PaneFontFamily

        public static readonly DependencyProperty PaneFontFamilyProperty =
            DependencyProperty.Register(
                nameof(PaneFontFamily),
                typeof(FontFamily),
                typeof(MyHamburgerMenu),
                new PropertyMetadata(OnPaneFontFamilyChanged));

        public FontFamily PaneFontFamily
        {
            get => (FontFamily)GetValue(PaneFontFamilyProperty);
            set => SetValue(PaneFontFamilyProperty, value);
        }

        private static void OnPaneFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MyHamburgerMenu)d).ApplyPaneFontFamily();
        }

        private void ApplyPaneFontFamily()
        {
            if (PaneGrid != null)
            {
                FontFamily paneFontFamily = PaneFontFamily;
                if (paneFontFamily != null)
                {
                    PaneGrid.Resources[SystemFonts.MessageFontFamilyKey] = paneFontFamily;
                    PaneGrid.Resources["ContentControlThemeFontFamily"] = paneFontFamily;
                }
                else
                {
                    PaneGrid.Resources.Remove(SystemFonts.MessageFontFamilyKey);
                    PaneGrid.Resources.Remove("ContentControlThemeFontFamily");
                }
            }
        }

        #endregion

        private Grid PaneGrid { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PaneGrid = GetTemplateChild(nameof(PaneGrid)) as Grid;

            ApplyPaneFontFamily();
        }
    }
}
