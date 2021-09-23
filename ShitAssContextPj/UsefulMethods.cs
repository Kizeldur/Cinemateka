using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseApi
{
    static class UsefulMethods
    {
        


        static public MySqlDataReader ExecuteCommand(string argument, string Command)
        {
            const string connection = "Server=mysql60.hostland.ru;Database=host1323541_vrn05;Uid=host1323541_itstep;Pwd=269f43dc;";
            var db = new MySqlConnection(connection);
            db.Open();
            if (db.Ping())
            {
                var select = $"SELECT * FROM table_cinemateka WHERE movie_title='{argument}' OR director = '{argument}' OR lead_actor='{argument}';";
                MySqlDataReader result;
                var query = new MySqlCommand
                {
                    Connection = db,
                    CommandText = select
                };
                result = query.ExecuteReader();
                db.Close();
                return result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
