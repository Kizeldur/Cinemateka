using KinopoiskAPI;
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

namespace Cinemateka
{
    /// <summary>
    /// Логика взаимодействия для Tab_Movie.xaml
    /// </summary>
    public partial class Tab_Movie : UserControl
    {
        public Tab_Movie()
        {
            InitializeComponent();
        }


        public event EventHandler ShowMovieListEvent;

        private void Button_SearchKinopoisk_Click(object sender, RoutedEventArgs e)
        {
            var argument = Input_SearchKinopoisk.Text;
            if (argument == "")
            {
                Input_SearchKinopoisk.Text = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                
                var movie = KinopoiskApi.GetMovieByTheTitle(argument);
                ShowMovie(movie);
            }

            EventHandler handler = ShowMovieListEvent;
            if (handler != null) handler(sender, e);
            
        }

        public event EventHandler SaveInDBEvent;

        private void btn_SaveInDB_Click(object sender, RoutedEventArgs e)
        {
            var title = label_Title.Content;
            var id = KinopoiskApi.GetKinopoiskId(title.ToString());
            var movie = new CinematekaTable {KpId = Convert.ToInt32(id), Title = title.ToString()};
            EventHandler handler = SaveInDBEvent;
            if (handler != null) handler(sender, e);
            e.Handled = true;
        }

        public void ShowMovie(Movie movie)
        {
            if (movie.Poster != null)
            {
                var path = "https:" + movie.Poster;
                var image = new BitmapImage(new Uri(path, UriKind.Absolute));
                image_Poster.Source = image;
            }
            
            label_Title.Content = movie.Title;
            label_OriginalTitle.Content = movie.Title_Alternative;
            label_Director.Content = movie.Directors[0];
            var actorsString = String.Empty;
            if (movie.Actors.Length > 3)
            {
                actorsString += $"{movie.Actors[0]},\n {movie.Actors[1]},\n {movie.Actors[2]},\n..";
            }
            else
            {
                foreach (var actor in movie.Actors)
                {
                    actorsString += $"{actor},\n";
                }
            }
            actorsString += "..\n";
            label_Actors.Content = actorsString;
            label_Description.Content = movie.Description;
            label_KinopoiskRating.Content = movie.Rating_Kinopoisk;
            label_IMDbRating.Content = movie.Rating_Kinopoisk;
            

        }      
    }
}
