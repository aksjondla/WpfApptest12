using System;
using System.Collections;
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
    public partial class GamesTableUsers : Page
    {
        public GamesTableUsers()
        {
            InitializeComponent();
            ListViewImmage.ItemsSource = asd1234Entities.GetContex().id_sourse_image_preview.ToList();
        }
    }
}