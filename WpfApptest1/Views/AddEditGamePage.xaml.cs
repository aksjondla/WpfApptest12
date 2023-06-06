using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class AddEditGamePage : Page
    {
        private Games _currentGames = new Games();
        public AddEditGamePage(Games selectedGames)
        {
            InitializeComponent();
            if (selectedGames != null)
            {
                _currentGames = selectedGames;
            }
            DataContext = _currentGames;
            CmbBoxPlatform.ItemsSource = asd1234Entities.GetContex().Platform_id.ToList();
            CmbBoxDeveloper.ItemsSource = asd1234Entities.GetContex().Developer_id.ToList();
            CmbBoxGenres.ItemsSource = asd1234Entities.GetContex().Genres_id.ToList();
            CmbBoxImageSourse.ItemsSource = asd1234Entities.GetContex().id_sourse_image_preview.ToList();

            CmbBoxImageSourse.SelectedItem = asd1234Entities.GetContex().id_sourse_image_preview.Find(_currentGames.id_Image_preview);
            CmbBoxPlatform.SelectedItem = asd1234Entities.GetContex().Platform_id.Find(_currentGames.id_Platform);
            CmbBoxDeveloper.SelectedItem = asd1234Entities.GetContex().Developer_id.Find(_currentGames.id_Developer);
            CmbBoxGenres.SelectedItem = asd1234Entities.GetContex().Genres_id.Find(_currentGames.id_Genre);
        }

        private void BtnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (CmbBoxDeveloper.SelectedItem == null)
                error.AppendLine("Поле 'издатель' обьязательно");
            if (CmbBoxGenres.SelectedItem == null)
                error.AppendLine("Поле 'жанр' обьязательно");
            if (CmbBoxImageSourse.SelectedItem == null)
                error.AppendLine("Поле 'превью' обьязательно");
            if (CmbBoxPlatform.SelectedItem == null)
                error.AppendLine("Поле 'Платформа' обьязательно");
            if (TxtBxNameGame.Text == "")
                error.AppendLine("Поле 'Имя' обьязательно");
            if ((DataPickCreateGame.SelectedDate == null) || ((DataPickCreateGame.SelectedDate < DateTime.MinValue) && (DataPickCreateGame.SelectedDate > DateTime.MaxValue) && (DataPickCreateGame.SelectedDate >= DateTime.Now) && (DataPickCreateGame.SelectedDate <= DateTime.Now)))
                error.AppendLine("Поле 'Дата' не соответствует");

            if (error.Length != 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            else
            {
                var buff = CmbBoxDeveloper.SelectedItem as Developer_id;
                _currentGames.id_Developer = buff.id_Developer;
                var buff1 = CmbBoxGenres.SelectedItem as Genres_id;
                _currentGames.id_Genre = buff1.id_Genre;
                var buff2 = CmbBoxImageSourse.SelectedItem as id_sourse_image_preview;
                _currentGames.id_Image_preview = buff2.id_Sourse;
                var buff3 = CmbBoxPlatform.SelectedItem as Platform_id;
                _currentGames.id_Platform = buff3.id_Platform;
            }

            if (_currentGames.id_Game == 0)
                asd1234Entities.GetContex().Games.Add(_currentGames);
            try
            {
                asd1234Entities.GetContex().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbBoxImageSourse_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            var idimage = CmbBoxImageSourse.SelectedItem as id_sourse_image_preview;
            idimage = asd1234Entities.GetContex().id_sourse_image_preview.Find(idimage.id_Sourse);
            if (idimage.Image_sourse != null)
            {
                byte[] data = idimage.Image_sourse.ToArray();
                if (data != null && data.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        var imageSourse = new BitmapImage();
                        imageSourse.BeginInit();
                        imageSourse.CacheOption = BitmapCacheOption.OnLoad;
                        imageSourse.StreamSource = ms;
                        imageSourse.EndInit();
                        ImageSelected.Source = imageSourse;
                    }
                }
                else
                {
                    idimage = asd1234Entities.GetContex().id_sourse_image_preview.FirstOrDefault(u => u.id_Sourse == 1);
                    if (idimage != null)
                    {
                        data = idimage.Image_sourse.ToArray();
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            var imageSourse = new BitmapImage();
                            imageSourse.BeginInit();
                            imageSourse.CacheOption= BitmapCacheOption.OnLoad;
                            imageSourse.StreamSource = ms;
                            imageSourse.EndInit();
                            ImageSelected.Source = imageSourse;
                        }
                    }
                }
            }
        }
    }
}
