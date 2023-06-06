using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using WpfApptest1.model;

namespace WpfApptest1.Views
{
    public partial class signInPage : Page
    {
        public signInPage()
        {
            InitializeComponent();
        }

        private void Btnavto_Click(object sender, RoutedEventArgs e)
        {
            if ((!(string.IsNullOrWhiteSpace(Login.Text))) && (!(String.IsNullOrEmpty(Password.Password))))
            {
                try
                {
                    var pass = md5.HashPassword(Password.Password);
                    byte[] bytes = Encoding.Default.GetBytes(pass);
                    pass = Encoding.UTF8.GetString(bytes);
                    var CurrentUser = asd1234Entities.GetContex().Users.FirstOrDefault(u => u.Login == Login.Text && u.Password == pass);
                    if (CurrentUser == null) { return; }
                    var NotUser = asd1234Entities.GetContex().Role_id.FirstOrDefault(u => u.id_Role == CurrentUser.id_Role);
                    if (CurrentUser != null)
                    {
                        if (NotUser.Role_access == 1)
                        {
                            UserInfo.IsAdmin = true;
                            UserInfo.TrueUserInfo = CurrentUser;
                            NavigationService.Navigate(new usersTable());
                        }
                        else
                        {
                            UserInfo.IsAdmin = false;
                            UserInfo.TrueUserInfo = CurrentUser;
                            NavigationService.Navigate(new GameTablePage());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(null));
        }

        private void Login_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, 0) && !"!@#$%^&*()_+".Contains(e.Text))
                e.Handled = true;
        }

        private void Login_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
