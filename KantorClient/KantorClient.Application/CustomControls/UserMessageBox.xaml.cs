using System.Drawing;
using System.Windows;

namespace KantorClient.Application.CustomControls
{
    /// <summary>
    /// Interaction logic for UserMessageBox.xaml
    /// </summary>
    public partial class UserMessageBox : Window
    {
        public bool Yes { get; set; }
        public UserMessageBox(string message, MessageBoxButton buttonOption, MessageBoxImage warrningType)
        {
            InitializeComponent();
            TextBlock.Text = message;
            SetButtonOption(buttonOption);
            SetWarrningType(warrningType);
        }

        public bool ShowMessage()
        {
            this.ShowDialog();
            return Yes;
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

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Yes = false;
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Yes = true;
            this.Close();
        }
    }
}
