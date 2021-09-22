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
using KinopoiskAPI;

namespace Cinemateka
{
    /// <summary>
    /// Логика взаимодействия для Tab_Cinemateka.xaml
    /// </summary>
    public partial class Tab_Cinemateka : UserControl
    {
        
        public Tab_Cinemateka()
        {
            InitializeComponent();
            
            
        }

        private void Button_SearchDB_Click(object sender, RoutedEventArgs e)
        {
            var movie = new Movie { Id = "798", Title = "qwe" };
            var m = new Tab_Movie();
            m.ShowMovie(movie);
        }

        private void DataTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tab_movie = new Tab_Movie();
            var item = (CinematekaTable)DataTable.SelectedItem as CinematekaTable;
            var title = item.Title;
            var movie = KinopoiskApi.GetMovieByTheTitle(title);
            tab_movie.ShowMovie(movie);
            //tab_movie.tabcontrol_Cinemateka.SelectedItem = tab_movie.tab_Movie;
            e.Handled = true;
        }
    }
}
