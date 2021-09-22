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



        public void ShowMovie(Movie movie)
        {
            //var path = "https:" + movie.Poster;
            //var image = new BitmapImage(new Uri(path, UriKind.Absolute));
            //image_Poster.Source = image;
            label_Title.Content = movie.Title;
            //label_OriginalTitle.Content = movie.Title_Alternative;
            //label_Director.Content = movie.Directors[0];
            //label_Actors.Content = $"{movie.Actors[0]}, {movie.Actors[1]}, {movie.Actors[2]}";
            //label_Description.Content = movie.Description;
            //label_KinopoiskRating.Content = movie.Rating_Kinopoisk;
            //label_IMDbRating.Content = movie.Rating_Kinopoisk;

        }
    }
}
