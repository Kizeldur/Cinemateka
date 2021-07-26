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

namespace Cinemateka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connection = "Server=mysql60.hostland.ru;Database=host1323541_vrn05;Uid=host1323541_itstep;Pwd=269f43dc;";
        private MySqlConnection db;
        //List<TableCinemateka> shitAsscinemateka = new List<TableCinemateka>();

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


        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
           

            var argument = Input_SearchBar.Text;
            if (argument == "")
            {
                label_progress.Content = "Пожалуйста, введите аргументы поиска";
            }
            else
            {
                /*try
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
                result.Close();*/
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
            var index = DataTable.Items.IndexOf(DataTable.CurrentItem);
            TableCinemateka row = ((FrameworkElement)sender).DataContext as TableCinemateka;
            using (var db = new ShitAssContext())
            {
                var h = db.TableCinematekas.ToList();
                //db.SaveChanges();
                var k = 456;
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
            List<TableCinemateka> shitAsscinemateka = new List<TableCinemateka>();

            using (var db = new ShitAssContext())
            {
                foreach (var movie in db.TableCinematekas)
                {
                        label_progress.Content = movie.MovieTitle;
                        shitAsscinemateka.Add(movie);
                }

                DataTable.ItemsSource = shitAsscinemateka;


            }
        }
    }
}