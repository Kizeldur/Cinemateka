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
using System.Collections.ObjectModel;

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
            using var db = new ShitAssContext();
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
                var movie = KinopoiskApi.GetMovieByTheTitle(argument);
                shitAsscinemateka.Add(movie);
                DataTable.ItemsSource = shitAsscinemateka;
                ShowMovie(movie);
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
            var title = label_Title.Content;
            var id = KinopoiskApi.GetKinopoiskId(title.ToString());
            var movie = new CinematekaTable { KpId = Convert.ToInt32(id), Title = title.ToString()};
            using (var db = new ShitAssContext())
            {
                db.CinematekaTables.Add(movie);
                db.SaveChanges();
            }
            ButtonShowAll_Click(sender, e);
            tabcontrol_Cinemateka.SelectedItem = tab_Cinemateka;
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
                var movieList = db.CinematekaTables;
                DataTable.ItemsSource = movieList.ToList();
            }            
        }

        private void ShowMovie(Movie movie)
        {
            label_Title.Content = movie.Title;
            label_OriginalTitle.Content = movie.Title_Alternative;
            label_Director.Content = movie.Directors[0];
            label_Actors.Content = $"{movie.Actors[0]}, {movie.Actors[1]}, {movie.Actors[2]}";
            label_Description.Content = movie.Description;
            label_KinopoiskRating.Content = movie.Rating_Kinopoisk;
            label_IMDbRating.Content = movie.Rating_Kinopoisk;
            var path = "https:" + movie.Poster;
            var image = new BitmapImage(new Uri(path, UriKind.Absolute));
            image_Poster.Source = image;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<CinematekaTable>();
            DataTable.ItemsSource = list;
        }

        private void DataTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (CinematekaTable)DataTable.SelectedItem as CinematekaTable;
            var title = item.Title;
            var movie = KinopoiskApi.GetMovieByTheTitle(title);
            ShowMovie(movie);
            tabcontrol_Cinemateka.SelectedItem = tab_Movie;
        }
    }
}