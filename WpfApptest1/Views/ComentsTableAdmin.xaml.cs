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
    public partial class ComentsTableAdmin : Page
    {
        public ComentsTableAdmin()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GridComments.ItemsSource = asd1234Entities.GetContex().Comments.ToArray();
        }

        private void BtnEditComment_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditComment((sender as Button).DataContext as Comments));
        }

        private void BtnRemoveComents_Click(object sender, RoutedEventArgs e)
        {
            var comentsDelete = GridComments.SelectedItems.Cast<Comments>().ToList();
            if (comentsDelete.Count > 0 && UserInfo.IsAdmin == true)
            if (MessageBox.Show($"Вы точно хотите удалить {comentsDelete.Count}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    asd1234Entities.GetContex().Comments.RemoveRange(comentsDelete);
                    asd1234Entities.GetContex().SaveChanges();
                    MessageBox.Show("Успех");
                    GridComments.ItemsSource = asd1234Entities.GetContex().Comments.ToList();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnAddComent_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.TrueUserInfo != null)
            {
                NavigationService.Navigate(new AddEditComment(null));
            }
            else
            {
                NavigationService.Navigate(new signInPage());
            }
        }
    }
}
