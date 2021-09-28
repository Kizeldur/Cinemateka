using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var db = new ShitAssContext();
            db.CinematekaTables.ToList();
            
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

        public void ButtonShowAll_Click(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }

        public void ShowAll()
        {
            using (var db = new ShitAssContext())
            {
                var movieList = db.CinematekaTables;
                DataTable.ItemsSource = movieList.ToList();
            }
        }


        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<CinematekaTable>();
            DataTable.ItemsSource = list;
        }

        public event EventHandler DoubleClickEvent;
        private void DataTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventHandler handler =  DoubleClickEvent;
            if (handler != null) handler(sender, e);
            e.Handled = true;
        }
    }
}
