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
    public partial class usersTable : Page
    {
        public usersTable()
        {
            InitializeComponent();
            passColl.Visibility = Visibility.Hidden;
            if (!UserInfo.IsAdmin)
            {
                passColl.Visibility= Visibility.Hidden;
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditPage((sender as Button).DataContext as Users));
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Users> usersForRemoving = DtGrid.SelectedItems.Cast<Users>().ToList();
            if (usersForRemoving.Count > 0 && MessageBox.Show($"Вы точно хотите удалить {usersForRemoving.Count}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes && (UserInfo.IsAdmin == true))
            {
                //var temp = asd1234Entities.GetContex().Comments.ToList().Where(u => usersForRemoving.Select(i => i.id_User).Contains(u.id_User));
                try
                {
                    asd1234Entities.GetContex().Users.RemoveRange(usersForRemoving);
                    asd1234Entities.GetContex().SaveChanges();
                    MessageBox.Show("Обьекты удаленны.");
                    DtGrid.ItemsSource = asd1234Entities.GetContex().Users.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                asd1234Entities.GetContex().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DtGrid.ItemsSource = asd1234Entities.GetContex().Users.ToList();
            }
        }

        private void DataGridTextColumn_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
