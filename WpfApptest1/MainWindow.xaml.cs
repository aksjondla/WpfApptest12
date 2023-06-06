using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using WpfApptest1.Views;
using System.IO;
using System.Data.Entity.Migrations;

namespace WpfApptest1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new signInPage();//GamesTableUsers();//
            if (null == asd1234Entities.GetContex().id_sourse_image_preview.Select(u => u.id_Sourse == 1))
            {
                byte[] bytes = File.ReadAllBytes("./isNull.jpg");
                if (bytes != null)
                {
                    id_sourse_image_preview imadeAdd = new id_sourse_image_preview();
                    imadeAdd.id_Sourse = 1;
                    imadeAdd.Image_sourse = bytes;
                    imadeAdd.name_image = "isNull.jpg";
                    asd1234Entities.GetContex().id_sourse_image_preview.AddOrUpdate(imadeAdd);
                    asd1234Entities.GetContex().SaveChanges();
                }
            }
        }

        private void BtnTableUsersList_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin)
            MainFrame.Content = new usersTable();
        }

        private void BtnSignInUser_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new signInPage();
        }

        private void BtnTableGamesList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Views.GameTablePage();
        }

        private void BtnTableImmageSourseGrid_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin)
            MainFrame.Content = new immageSourseTable();
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            /*if (!UserInfo.IsAdmin)
            {
                BtnTableUsersList.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnTableUsersList.Visibility = Visibility.Visible;
            }*/
        }

        private void BtnTableComentsGrid_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ComentsTableAdmin();
        }

        private void BtnSearh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void BtnSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            Page page = MainFrame.Content as Page;
            if (page != null)
            {
                var dataSearch = (DataGrid)page.FindName("DtGrid");
                if (dataSearch != null)
                {
                    dataSearch.ItemsSource = asd1234Entities.GetContex().Users.Where(u => u.Name.Contains(TxtBoxSearh.Text) || u.Familiya.Contains(TxtBoxSearh.Text) || u.Otchestvo.Contains(TxtBoxSearh.Text)).ToList();

                }
                dataSearch = (DataGrid)page.FindName("DataGridImmage");
                if (dataSearch != null)
                {
                    dataSearch.ItemsSource = asd1234Entities.GetContex().id_sourse_image_preview.Where(u => u.name_image.Contains(TxtBoxSearh.Text)).ToArray();

                }
                dataSearch = (DataGrid)page.FindName("DataGridName");
                if (dataSearch != null)
                {
                    List<Games> temp = asd1234Entities.GetContex().Games.Where(u => u.Name.Contains(TxtBoxSearh.Text) || TxtBoxSearh.Text == u.Rating_average.ToString()).ToList();
                    if (temp.Count != 0)
                        dataSearch.ItemsSource = temp;
                    else
                    {
                        var asd1 = asd1234Entities.GetContex().Platform_id.Where(i => i.Name_platform.Contains(TxtBoxSearh.Text)).Select(u => u.id_Platform).ToList();
                        if (asd1.Count != 0)
                            dataSearch.ItemsSource = asd1234Entities.GetContex().Games.Where(u => asd1.Contains(u.id_Platform)).ToList();
                        else
                        {
                            var temp3 = asd1234Entities.GetContex().Developer_id.Where(u => u.Name_developer.Contains(TxtBoxSearh.Text)).Select(i => i.id_Developer).ToList();
                            if (temp3.Count != 0)
                                dataSearch.ItemsSource = asd1234Entities.GetContex().Games.Where(u => temp3.Contains(u.id_Developer)).ToList();
                            else
                            {
                                var temp4 = asd1234Entities.GetContex().Genres_id.Where(u => u.Name_ganre.Contains(TxtBoxSearh.Text)).Select(i => i.id_Genre).ToList();
                                if (temp4.Count != 0)
                                    dataSearch.ItemsSource = asd1234Entities.GetContex().Games.Where(u => temp4.Contains(u.id_Genre)).ToList();
                            }
                        }
                    }
                }

            }
        }

    }
}