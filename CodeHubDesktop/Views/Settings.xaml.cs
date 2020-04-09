using System.Windows.Controls;

namespace CodeHubDesktop.Views
{
    /// <summary>
    /// Interaction logic for Settings
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();

            if (GlobalData.Config.Lang.Equals("fa-IR"))
            {
                tg.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            }
            else
            {
                tg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            }
        }
    }
}
