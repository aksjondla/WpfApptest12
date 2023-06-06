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
using WpfApptest1.model;

namespace WpfApptest1.Views
{
    public partial class AddEditPage : Page
    {
        private Users _currentusers = new Users();
        public AddEditPage(Users selectedUsers)
        {
            InitializeComponent();
            if (selectedUsers != null)
            {
                _currentusers = selectedUsers;
            }
            else
            {
                _currentusers.Registration_date = DateTime.Now;
            }
            HashIsAdmin.Visibility = Visibility.Hidden;
            //StackPanelRegister.Children.Remove(HashIsAdmin);
            DataContext = _currentusers;
            //this.PasswordBox.Text = md5.
            //DataContext = _currentusers.id as RoleId;
            //CmbBox.ItemsSource = asd1234Entities1.GetContex().RoleId.ToList();
            //var asd1 = asd1234Entities.GetContex().RoleId.ToList();
            CmbBox.ItemsSource = asd1234Entities.GetContex().Role_id.ToList();
            Cmb1Box.ItemsSource = asd1234Entities.GetContex().Sex_id.ToList();
            var index = asd1234Entities.GetContex().Role_id.Find(_currentusers.id_Role);
            var indexsex = asd1234Entities.GetContex().Sex_id.Find(_currentusers.id_Sex);
            //if (!asd) return;
            CmbBox.SelectedItem = index;
            Cmb1Box.SelectedItem = indexsex;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentusers.Login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(_currentusers.Password))
                errors.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(_currentusers.Password))
                errors.AppendLine("Поле фамилия обьязательно");
            if (string.IsNullOrWhiteSpace(_currentusers.Password))
                errors.AppendLine("Поле имя обьязательно");
            if (string.IsNullOrWhiteSpace(_currentusers.Password))
                errors.AppendLine("Поле отчество обьязательно");
            if (HashIsAdmin.Visibility == Visibility.Visible)
            {
                string ishash = md5.HashPassword(HashIsAdmin.Password);
                if (ishash != UserInfo.Hashis)
                    errors.AppendLine("Ошибка в поле хеш");
            }
            var CurrentRile = CmbBox.SelectedItem as Role_id;
            if (/*CmbBox.SelectedItem CurrentRile*/CurrentRile == null) //проверка на выбранную роль
            {
                errors.AppendLine("Выберите роль");
            }
            else
            {
                _currentusers.id_Role = CurrentRile.id_Role;/* (CmbBox.SelectedItem as RoleId).id;*/
            }

            var Current = Cmb1Box.SelectedItem as Sex_id;
            if (Current == null) //проверка выбран ли пол
            {
                errors.AppendLine("не выбрали пол");
            }
            else
            {
                _currentusers.id_Sex = Current.id_Sex;
            }
            
            if ((Datepick.SelectedDate == null) || ((Datepick.SelectedDate < DateTime.MinValue) && (Datepick.SelectedDate > DateTime.MaxValue) && (Datepick.SelectedDate >= DateTime.Now) && (Datepick.SelectedDate <= DateTime.Now)))
            {
                errors.AppendLine("Не ввели дату");
            }
            else
            {
                DateTime date = Datepick.SelectedDate.Value;
                _currentusers.DOB = date.Date;//Datepick.SelectedDate.Value;
            }
            if (errors.Length > 0) //проверка были ли поля
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            if (_currentusers.id_User == 0)
            {
                _currentusers.Password = md5.HashPassword(_currentusers.Password);
                //byte[] bytes = Encoding.Default.GetBytes(_currentusers.Password);
                //_currentusers.Password = Encoding.UTF8.GetString(bytes);
                asd1234Entities.GetContex().Users.Add(_currentusers);
            }
            try
            {
                //_currentusers.Password = md5.HashPassword(_currentusers.Password);
                //byte[] bytes = Encoding.Default.GetBytes(_currentusers.Password);
                //_currentusers.Password = Encoding.UTF8.GetString(bytes);
                asd1234Entities.GetContex().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           //if (_currentusers == null)
                //Datepick.SelectedDate = DateTime.Now.Date;
        }

        private void BtnNavigateGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, 0) && !"!@#$%^&*()_+".Contains(e.Text))
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void CmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((CmbBox.SelectedItem as Role_id).Role_access == 1)
            {
                HashIsAdmin.Visibility= Visibility.Visible;
            }
            else
                HashIsAdmin.Visibility = Visibility.Hidden;
        }

        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordBox.Text.Contains(LoginBox.Text))
            {
                LabelIsLenght.Foreground = Brushes.Red;
            }
            else
            {
                LabelIsLenght.Content = "ненадёжный";
                LabelIsLenght.Foreground = Brushes.Green;
            }

            if (PasswordBox.Text.Length < 8)
                LabelIsLenght.Foreground = Brushes.Red;
            else
            {
                LabelIsLenght.Content = "ненадёжный";
                LabelIsLenght.Foreground = Brushes.Green;
            }

            if (!PasswordBox.Text.Any(Char.IsDigit) || !PasswordBox.Text.Any(Char.IsLower) || !PasswordBox.Text.Any(Char.IsUpper))
            {
                LabelIsLenght.Foreground = Brushes.Red;
            }
            else
            {
                LabelIsLenght.Foreground = Brushes.Green;
                LabelIsLenght.Content = "ненадёжный";
            }
        }
    }
}
