using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
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
    public partial class GameTablePage : Page
    {
        public GameTablePage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridName.ItemsSource = asd1234Entities.GetContex().Games.ToList();
                var games = asd1234Entities.GetContex().Games.ToList();
                foreach (var game in games)
                {
                    var ratings = asd1234Entities.GetContex().Comments.Where(c => c.id_Game == game.id_Game).Select(c => c.Rating_id);
                    if (ratings.Any())
                    {
                        double avg = ratings.Average();
                        game.Rating_average = (int)avg;
                    }
                }
                asd1234Entities.GetContex().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEditGames_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
            {
                Games currentGames_ = DataGridName.SelectedItem as Games;
                NavigationService.Navigate(new AddEditGamePage(currentGames_));
            }
        }

        private void BtnAddGame_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin == true)
                NavigationService.Navigate(new AddEditGamePage(null));
        }

        private void BtnRemoveGame_Click(object sender, RoutedEventArgs e)
        {
            var GameForDelete = DataGridName.SelectedItems.Cast<Games>().ToList();
            //var GameForDelete = DataGridName.SelectedItem as Games;
            if (UserInfo.IsAdmin == true)
            if (MessageBox.Show($"Вы точно хотите удалить {GameForDelete.Count}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    asd1234Entities.GetContex().Games.RemoveRange(GameForDelete);
                    asd1234Entities.GetContex().SaveChanges();
                    MessageBox.Show("Обьекты удаленны.");
                    DataGridName.ItemsSource = asd1234Entities.GetContex().Games.ToList();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void BtnAddComentGame_Click(object sender, RoutedEventArgs e)
        {
            var games = asd1234Entities.GetContex().Games.OrderBy(u => u.Rating_average);
            if (games != null)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {

                    var worksheet = excelPackage.Workbook.Worksheets.Add("Games");

                    worksheet.Cells[1, 1].Value = "Game";
                    worksheet.Cells[1, 2].Value = "Rating";
                    int row = 2;
                    foreach (var game in games)
                    {
                        worksheet.Cells[row, 1].Value = game.Name;
                        worksheet.Cells[row, 2].Value = game.Rating_average;
                        row++;
                    }

                    var chart = worksheet.Drawings.AddChart("RatingChart", eChartType.ColumnClustered);
                    chart.SetPosition(0, 0, 0, 0);
                    chart.SetSize(600, 400);
                    chart.Title.Text = "Games Rating";
                    chart.Series.Add(worksheet.Cells["B2:B" + (row - 1)], worksheet.Cells["A2:A" + (row - 1)]);

                    var filePath = @".\GamesRating.xlsx";//@"C:\Users\456\Desktop\bd\GamesRating.xlsx";
                    
                    MessageBox.Show("Файл сохранён/перезаписан в директории текущего проекта \n WpfApptest1\\WpfApptest1\\bin\\Debug\\GamesRating.xlsx");
                    File.WriteAllBytes(filePath, excelPackage.GetAsByteArray());
                }
            }
            
        }

        private void BtnListComentGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В разработке");
        }
    }
}
