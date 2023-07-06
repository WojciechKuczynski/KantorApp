using System.Windows;

namespace KantorClient.Application.Views
{
    /// <summary>
    /// Interaction logic for ClosingWindow.xaml
    /// </summary>
    public partial class ClosingWindow : Window
    {
        public ClosingWindow()
        {
            InitializeComponent();
            Action = 0;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Action = 0;
            this.Close();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Action = 1;
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Action = 2;
            this.Close();
        }
        /// <summary>
        /// 1 - logout, 2- exit, 0 - cancel
        /// </summary>
        public int Action { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Action == 0)
                e.Cancel = true;
        }
    }
}
