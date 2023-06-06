using Microsoft.Win32;
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
using System.IO;
using WpfAnimatedGif;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Animation;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;

namespace WpfApptest1.Views
{
    public partial class AddEditImageSourse : Page
    {
        public id_sourse_image_preview _currentImage = new id_sourse_image_preview();
        public byte[] DataByteImagePage { get; set; }
        public AddEditImageSourse(id_sourse_image_preview selectedImage)
        {
            InitializeComponent();
            if (selectedImage != null)
            {
                _currentImage= selectedImage;
                AddImageBinary.Content = "Обновить";
            }
            else
            {

            }
            DataContext = _currentImage;
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "(*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                if (openDialog.ShowDialog() == true && File.Exists(openDialog.FileName))
                {
                    string filePath = openDialog.FileName;
                    TxtBoxPath.Text = filePath;
                    TxtBoxImageName.Text = System.IO.Path.GetFileNameWithoutExtension(openDialog.SafeFileName);
                    DataByteImagePage = File.ReadAllBytes(filePath);
                    BitmapImage bitmap = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(DataByteImagePage))
                    {
                        bitmap.BeginInit();
                        /*if (System.IO.Path.GetExtension(openDialog.SafeFileName) == ".gif" && false)
                        {
                            //bitmap.UriSource = new Uri(openDialog.SafeFileName, UriKind.Relative);
                            bitmap.UriSource = new Uri(openDialog.SafeFileName, UriKind.RelativeOrAbsolute);
                        }
                        else //if (System.IO.Path.GetExtension(openDialog.SafeFileName) == "")
                        {
                        }*/
                        bitmap.CacheOption= BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                    }
                    if (System.IO.Path.GetExtension(openDialog.SafeFileName) == ".gif")
                    {
                        ImageStackPanel.Source = bitmap;
                        //ImageBehavior.SetAnimatedSource(ImageStackPanel, bitmap);
                        //ImageBehavior.SetRepeatBehavior(ImageStackPanel, new RepeatBehavior(0));
                    }
                    else
                    {
                        ImageStackPanel.Source = bitmap;
                    }
                    /*using (var fileStream = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read))
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        ImageStackPanel.Source
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error file");
            }
        }

        private void AddImageBinary_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();
            if (ImageStackPanel.Source == null)
                err.AppendLine("Недопустимое превью.");
            if (string.IsNullOrWhiteSpace(TxtBoxImageName.Text))
                err.AppendLine("Имя");
            if (err.Length != 0)
            {
                MessageBox.Show(err.ToString());
                return;

            }
            //SqlParameter name = new SqlParameter("@name", TxtBoxImageName.Text);

            var isImage = asd1234Entities.GetContex().id_sourse_image_preview.FirstOrDefault(u => u.name_image == TxtBoxImageName.Text);
            if (isImage != null)
            {
                if (MessageBox.Show($"Имя существует, перейти к изменению фото фото?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //_currentImage.id_Sourse = isImage.id_Sourse;
                    NavigationService.Navigate(new AddEditImageSourse(isImage));
                    return;
                }
                else
                {
                    return;
                }
            }
            if (DataByteImagePage != null)
                _currentImage.Image_sourse = DataByteImagePage;
            
            _currentImage.name_image = TxtBoxImageName.Text;
            if (_currentImage.id_Sourse == 0)
                asd1234Entities.GetContex().id_sourse_image_preview.Add(_currentImage);
            /*else if (_currentImage.id_Sourse != 0)
            {
                try
                {
                    //asd1234Entities.GetContex().id_sourse_image_preview.Remove(_currentImage);
                    asd1234Entities.GetContex().id_sourse_image_preview.AddOrUpdate(_currentImage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }*/

            try
            {
                asd1234Entities.GetContex().SaveChanges();
                MessageBox.Show("Успех");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
