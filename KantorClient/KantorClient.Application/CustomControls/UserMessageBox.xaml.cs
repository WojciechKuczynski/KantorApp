using System.Drawing;
using System.Windows;

namespace KantorClient.Application.CustomControls
{
    /// <summary>
    /// Interaction logic for UserMessageBox.xaml
    /// </summary>
    public partial class UserMessageBox : Window
    {
        public UserMessageBox(string message, MessageBoxButton buttonOption, MessageBoxImage warrningType)
        {
            InitializeComponent();
            TextBlock.Text = message;
            SetButtonOption(buttonOption);
            SetWarrningType(warrningType);
        }

        private void SetButtonOption(MessageBoxButton option)
        {
            switch(option)
            {
                case MessageBoxButton.OK:
                    OkBtn.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SetWarrningType(MessageBoxImage type)
        {
            switch (type)
            {
                case MessageBoxImage.Information:
                    WarningBrush.Color = System.Windows.Media.Color.FromRgb(0,255,0);
                    break;
                case MessageBoxImage.Warning:
                    WarningBrush.Color = System.Windows.Media.Color.FromRgb(255, 255, 0);
                    break;
                case MessageBoxImage.Error:
                    WarningBrush.Color = System.Windows.Media.Color.FromRgb(255, 0, 0);
                    break;
            }
        }
    }
}
