using System;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CinemaDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public Cinema GetCinemaByCineId(int? cineId)
        {
            if (cineId == null)
            {
                return null;
            }
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = $"select * from Cinemas where cine_id = " + cineId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Cinema cinema = null;
            using(reader = command.ExecuteReader()){
                if (reader.Read())
                {
                    cinema = GetCinema(reader);
                }
            }
            connection.Close();
            return cinema;
        }
        private Cinema GetCinema(MySqlDataReader reader){
            int cineId = reader.GetInt32("cine_id");
            string cineAddress = reader.GetString("cine_address");
            string cineName = reader.GetString("cine_name");
            string cinePhone = reader.GetString("cine_phone");

            Cinema cinema = new Cinema(cineId, cineAddress, cineName, cinePhone);
            return cinema;
        }
    }

}