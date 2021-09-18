﻿using System;
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
using System.Drawing;


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
            /*using (var db = new ShitAssContext())
            {
                db.TableCinematekas.Add(new TableCinemateka
                {
                    MovieTitle = "Jurrasic_Park",
                    LeadActor = "ThisGuy",
                    Director = "Spielberg",
                    Year = 123,

                });
                db.SaveChanges();
            }*/
            //DBConnect();
        }

        private void DBConnect()
        {
            db = new MySqlConnection(connection);
            db.Open();
            if (db.Ping())
            {
                label_progress.Foreground = Brushes.Blue;
                label_progress.Content = "Connection is approved";
            }
            else
            {
                label_progress.Foreground = Brushes.Firebrick;
                label_progress.Content = "Connection is disapproved";
            }
        }

        private void DBEFConnect()
        {
            var db = new ShitAssContext();
            
        }

        /*private void Button_Search_Click(object sender, RoutedEventArgs e)
        {


            var argument = Input_SearchBar.Text;
            if (argument == "")
            {
                label_progress.Content = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                try
                {
                    var resulte = ExecuteCommand(argument);
                    resulte.Read();
                    label_progress.Content = resulte.GetString("movie_title");
                    resulte.Close();
                    resulte = ExecuteCommand(argument);
                    resulte.Read();
                    label_progress.Content = resulte.GetString("movie_title");
                    resulte.Close();
                }
                catch (MySqlException)
                {
                    label_progress.Content = "Na";
                }
                var result = ExecuteCommand(argument);
                result.Read();
                label_progress.Content = result.GetString("movie_title");
                result.Close();
                List<TableCinemateka> shitAsscinemateka = new List<TableCinemateka>();
            }
        }*/


        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
           

            var argument = Input_SearchBar.Text;
            if (argument == "")
            {
                label_progress.Content = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                //List<TableCinemateka> shitAsscinemateka = new List<TableCinemateka>();
                List<Movie> shitAsscinemateka = new List<Movie>();

                var url = KinipoiskApi.GetKinopoiskUrl(argument);
                var json = KinipoiskApi.GetKinopoiskData(url);
                var movie = JsonConvert.DeserializeObject<Movie>(json);
                //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"d:\movie.json"));
                shitAsscinemateka.Add(movie);
                DataTable.ItemsSource = shitAsscinemateka;
                //image_Poster.Source = new BitmapImage(new Uri(movie.Poster, UriKind.Relative));
                var image = new BitmapImage();
                image.BeginInit();
                var path = "https:" + movie.Poster;
                image.UriSource = new Uri(path, UriKind.Absolute);
                poster.Source = image;
                /*using (var db = new ShitAssContext())
                {
                    foreach (var movie in db.TableCinematekas)
                    {
                        if (argument == movie.MovieTitle || argument == movie.Director || argument == movie.LeadActor)
                        {
                            label_progress.Content = movie.MovieTitle;
                            shitAsscinemateka.Add(movie);
                        }
                    }
                    
                    DataTable.ItemsSource = shitAsscinemateka;
                  
                            
                }*/

            }  
        }
        
        private MySqlDataReader ExecuteCommand(string argument)
        {
            var select = $"SELECT * FROM table_cinemateka WHERE movie_title='{argument}' OR director = '{argument}' OR lead_actor='{argument}';";
            MySqlDataReader result;
            var query = new MySqlCommand
            {
                Connection = db,
                CommandText = select
            };
            result = query.ExecuteReader();
    
            return result;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {           
            // Current Solution
            TableCinemateka row = ((FrameworkElement)sender).DataContext as TableCinemateka;
            using (var db = new ShitAssContext())
            {
                db.TableCinematekas.Remove(row);
                db.SaveChanges();
            }
            ShowCinematekaList();

            //Alternate Solution
            /*var index = DataTable.Items.IndexOf(DataTable.CurrentItem);
            DataTable.Items.RemoveAt(index);
            ShowCinematekaList();*/
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            //var index = DataTable.Items.IndexOf(DataTable.CurrentItem);
            TableCinemateka row = ((FrameworkElement)sender).DataContext as TableCinemateka;
           
            using (var db = new ShitAssContext())
            {
                var h = db.TableCinematekas.ToList();
                //DataGrid.
                foreach (var el in db.TableCinematekas.ToList())
                {
                    if (el.Id == row.Id)
                    {
                        el.Director = row.Director;
                        //el.Director = "Spielberg";
                        el.MovieTitle = row.MovieTitle;
                        el.LeadActor = row.LeadActor;
                        el.Year = row.Year;
                    }
                }

                var row_2 = 

                db.SaveChanges();
                ShowCinematekaList();
            }
        }

        private string GiveMeSearcgArgument()
        {
            return Input_SearchBar.Text;
        }

        private void ShowCinematekaList()
        {
            var argument = Input_SearchBar.Text;
            List<TableCinemateka> shitAsscinemateka = new List<TableCinemateka>();

            using (var db = new ShitAssContext())
            {
                foreach (var movie in db.TableCinematekas)
                {
                    if (argument == movie.MovieTitle || argument == movie.Director || argument == movie.LeadActor)
                    {
                        label_progress.Content = movie.MovieTitle;
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
                DataTable.ItemsSource = db.TableCinematekas.ToList();
            }
        }
    }
}