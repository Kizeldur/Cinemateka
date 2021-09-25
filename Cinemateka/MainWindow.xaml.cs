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
        const string connection = "Server=mysql60.hostland.ru;Database=host1323541_vrn05;Uid=host1323541_itstep;Pwd=269f43dc;";
        private MySqlConnection db;

        public MainWindow()
        {
            InitializeComponent();
            //tab_content_Movie.ShowAll += tab_content_Cinemateka.ButtonShowAll_Click;
            tab_content_Cinemateka.DoubleClick += DataTable_MouseDoubleClick;
        }

        private void DBConnect()
        {
            db = new MySqlConnection(connection);
            db.Open();
            if (db.Ping())
            {
                tab_content_Cinemateka.label_progress.Foreground = Brushes.Blue;
                tab_content_Cinemateka.label_progress.Content = "Connection is approved";
            }
            else
            {
                tab_content_Cinemateka.label_progress.Foreground = Brushes.Firebrick;
                tab_content_Cinemateka.label_progress.Content = "Connection is disapproved";
            }
        }

        public void SaveToDB(CinematekaTable movie)
        {
            using (var db = new ShitAssContext())
            {
                db.CinematekaTables.Add(movie);
                db.SaveChanges();
            }
            tab_content_Cinemateka.ShowAll();
            tabcontrol_Cinemateka.SelectedItem = tab_Cinemateka;
        }

        public void DataTable_MouseDoubleClick(object sender, EventArgs e)
        {
            var item = (CinematekaTable)tab_content_Cinemateka.DataTable.SelectedItem as CinematekaTable;
            var title = item.Title;
            var movie = KinopoiskApi.GetMovieByTheTitle(title);
            tab_content_Movie.ShowMovie(movie);
            tabcontrol_Cinemateka.SelectedItem = tab_Movie;
            
        }
    }
}