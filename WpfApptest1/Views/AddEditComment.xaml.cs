using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class AddEditComment : Page
    {
        private Comments _currentCommet = new Comments();
        public AddEditComment(Comments selectedCommet)
        {
            InitializeComponent();
            if (selectedCommet != null)
            {
                _currentCommet = selectedCommet;
            }
            else
            {
                StackPalenComment.Children.Remove(dateTimePicker);
                //dateTimePicker.Visibility= Visibility.Hidden;
                TxtBlickDate.Text += $"\n{DateTime.Now.ToString("dd.MM.yyyy")}";
                _currentCommet.Date_comment = DateTime.Now;
                _currentCommet.id_User = UserInfo.TrueUserInfo.id_User;
            }
            DataContext = _currentCommet;
            CmbBoxGameName.ItemsSource = asd1234Entities.GetContex().Games.ToArray();
            CmbBoxGameName.SelectedItem = asd1234Entities.GetContex().Games.FirstOrDefault(u => u.id_Game == _currentCommet.id_Game);
            CmbBoxUserSelect.ItemsSource = asd1234Entities.GetContex().Users.ToArray();
            CmbBoxUserSelect.SelectedItem = asd1234Entities.GetContex().Users.FirstOrDefault(u => u.id_User == _currentCommet.id_User);
            CmbRating.ItemsSource = asd1234Entities.GetContex().Rating_Values.ToList();
            CmbRating.SelectedItem = asd1234Entities.GetContex().Rating_Values.FirstOrDefault(u => u.id_Rating == _currentCommet.Rating_id);
            
        }

        private void BtnSaveComment_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();
            if (dateTimePicker.SelectedDate == null && dateTimePicker.Parent != null)
                err.AppendLine("Поле 'Дата' обьязаельно.");
            if (CmbBoxGameName.SelectedItem == null)
                err.AppendLine("Поле 'Имя иры' обьязаельно.");
            if (CmbBoxUserSelect.SelectedItem == null)
                err.AppendLine("Поле 'Пользователь.' обьязаельно.");
            if (CmbRating.SelectedItem == null)
                err.AppendLine("Поле 'Пользователь.' обьязаельно.");

            if (err.Length != 0)
            {
                MessageBox.Show(err.ToString());
                return;
            }
            _currentCommet.id_Game = (CmbBoxGameName.SelectedItem as Games).id_Game;
            _currentCommet.id_User = (CmbBoxUserSelect.SelectedItem as Users).id_User;
            _currentCommet.Rating_id = (CmbRating.SelectedItem as Rating_Values).Rating;

            var _currentCommet1 = asd1234Entities.GetContex().Comments.FirstOrDefault(u => u.id_Game == _currentCommet.id_Game && u.id_User == _currentCommet.id_User);

            if ((_currentCommet1 != null) && (MessageBox.Show($"Вы точно обновить коментарий?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes))
            {
                return;
            }

            if (_currentCommet1 != null)
            {
                int id_comen = _currentCommet1.id_Comment; // и без неё
                _currentCommet1 = _currentCommet;
                _currentCommet.id_Comment = id_comen;
                asd1234Entities.GetContex().Comments.AddOrUpdate(_currentCommet1);
            }

            if (_currentCommet.id_Comment == 0)
                asd1234Entities.GetContex().Comments.Add(_currentCommet);
            try
            {
                asd1234Entities.GetContex().SaveChanges();
                MessageBox.Show("Успех!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
