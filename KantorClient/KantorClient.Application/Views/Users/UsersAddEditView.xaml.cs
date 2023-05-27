using KantorClient.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KantorClient.Application.Views.Users
{
    /// <summary>
    /// Interaction logic for UsersAddEditView.xaml
    /// </summary>
    public partial class UsersAddEditView : UserControl
    {
        public UsersAddEditView()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as UsersAddEditViewModel;
            if (dataContext == null)
            {
                return;
            }

            if (dataContext.Editing && PassBox.Password.Length == 0)
            {
                dataContext.SaveCommand.Execute(PassBox.Password);
                PassBox.Password = PassBox2.Password = string.Empty;
                return;
            }

            if (PassBox.Password.Length  < 5)
            {
                MessageBox.Show("Hasło jest za krótkie");
                return;
            }

            if (PassBox.Password != PassBox2.Password)
            {
                MessageBox.Show("Hasła do siebie nie pasują");
                return;
            }

            dataContext.SaveCommand.Execute(PassBox.Password);
            PassBox.Password = PassBox2.Password = string.Empty;
        }
    }
}
