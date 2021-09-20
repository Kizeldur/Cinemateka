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
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using KinopoiskAPI;
using Newtonsoft.Json;
using System.IO;

namespace Cinemateka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_SearchKinopoisk_Click(object sender, RoutedEventArgs e)
        {
            var argument = Input_SearchKinopoisk.Text;
            if (argument == "")
            {
                Input_SearchKinopoisk.Text = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                List<Movie> shitAsscinemateka = new List<Movie>();
                var url = KinipoiskApi.GetKinopoiskUrl(argument);
                var json = KinipoiskApi.GetKinopoiskData(url);
                var movie = JsonConvert.DeserializeObject<Movie>(json);
                shitAsscinemateka.Add(movie);
                DataTable.ItemsSource = shitAsscinemateka;
                var path = "https:" + movie.Poster;
                var image = new BitmapImage(new Uri(path, UriKind.Absolute));
                ShowMovie(movie);
                image_Poster.Source = image;
            }
        }


        private void Button_SearchDB_Click(object sender, RoutedEventArgs e)
        {
            var argument = Input_SearchDB.Text;
            if (argument == "")
            {
                label_progress.Content = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                List<CinematekaTable> shitAsscinemateka = new List<CinematekaTable>();
                DataTable.ItemsSource = shitAsscinemateka;
                using (var db = new ShitAssContext())
                {
                    foreach (var movie in db.CinematekaTables)
                    {
                        if (argument == movie.Title)
                        {
                            label_progress.Content = movie.Title;
                            shitAsscinemateka.Add(movie);
                        }
                    } 
                    DataTable.ItemsSource = shitAsscinemateka;         
                }
            }  
        }

        private void btn_SaveInDB_Click(object sender, RoutedEventArgs e)
        {

            //TODO придумать, как передавать текущий фильм

            var movie = new CinematekaTable { KpId = 77443, Title = "Город грехов (2005)"};
            using (var db = new ShitAssContext())
            {
                db.CinematekaTables.Add(movie);
                db.SaveChanges();
            }
            ButtonShowAll_Click(sender, e);
            tab_Cinemateka.SelectedIndex = 1;
        }

        private void DeleteFromDB_Click(object sender, RoutedEventArgs e)
        {
            CinematekaTable row = ((FrameworkElement)sender).DataContext as CinematekaTable;
            using (var db = new ShitAssContext())
            {
                db.CinematekaTables.Remove(row);
                db.SaveChanges();
            }
            ButtonShowAll_Click(sender, e);
        }

        private void ShowCinematekaList()
        {
            var argument = Input_SearchDB.Text;
            List<CinematekaTable> shitAsscinemateka = new List<CinematekaTable>();

            using (var db = new ShitAssContext())
            {
                foreach (var movie in db.CinematekaTables)
                {
                    if (argument == movie.Title)
                    {
                        label_progress.Content = movie.Title;
                        shitAsscinemateka.Add(movie);
                    }
                }

                DataTable.ItemsSource = shitAsscinemateka;
            }
            
        }

        private void ButtonShowAll_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ShitAssContext())
            {
                DataTable.ItemsSource = db.CinematekaTables.ToList();
            }
        }

        private void ShowMovie(Movie movie)
        {
            label_Title.Content = movie.Title;
            label_OriginalTitle.Content = movie.Title_Alternative;
            label_Director.Content = movie.Directors;
            label_Actors.Content = movie.Actors;
            label_Description.Content = movie.Description;
            label_KinopoiskRating.Content = movie.Rating_Kinopoisk;
            label_IMDbRating.Content = movie.Rating_Kinopoisk;
        }

        

        
    }
}