using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    public partial class immageSourseTable : Page
    {
        public immageSourseTable()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DataGridImmage.ItemsSource = asd1234Entities.GetContex().id_sourse_image_preview.ToList();
            }
        }

        private void BtnEditImageData_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditImageSourse((sender as Button).DataContext as id_sourse_image_preview));
        }

        private void BtnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            /*var*/List<id_sourse_image_preview> _currentimage = DataGridImmage.SelectedItems.Cast<id_sourse_image_preview>().ToList();
            _currentimage.RemoveAll(u => u.id_Sourse == 1);
            if (_currentimage != null && _currentimage.Count != 0 && UserInfo.IsAdmin == true)
            {
                if ((MessageBox.Show($"Вы точно хотите удалить {_currentimage.Count}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
                {
                    //_currentimage.RemoveAll(u => u.id_Sourse == 1);
                    //var arr = _currentimage.Select(i => i.id_Sourse).ToList();
                    //List<int> arr1= arr.ToList();
                    //var temp = asd1234Entities.GetContex().Games.Select(i => i.id_Image_preview).ToList();
                    var temp1 = asd1234Entities.GetContex().Games.ToList().Where(u => _currentimage.Select(i => i.id_Sourse).Contains(u.id_Image_preview));
                    //var temp3 = asd1234Entities.GetContex().Games.ToList();
                    //var tp4 = 
                    List<int> temp = asd1234Entities.GetContex().id_sourse_image_preview.Select(i => i.id_Sourse).ToList();
                    foreach (var item in temp1)
                    {
                        item.id_Image_preview = temp[0];
                        asd1234Entities.GetContex().Games.AddOrUpdate(item);
                    }
                    try
                    {
                        asd1234Entities.GetContex().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    try
                    {
                        asd1234Entities.GetContex().id_sourse_image_preview.RemoveRange(_currentimage);
                        asd1234Entities.GetContex().SaveChanges();
                        MessageBox.Show("Успех");
                        DataGridImmage.ItemsSource = asd1234Entities.GetContex().id_sourse_image_preview.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditImageSourse(null));
        }
    }
}
