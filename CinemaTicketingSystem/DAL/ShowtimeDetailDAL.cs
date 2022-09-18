using System;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ShowtimeDetailDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;

        public ShowtimeDetailDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public ShowtimeDetail GetShowtimeDetailByShowtimedId(int? showtimedId)
        {
            if (showtimedId == null)
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
            query = $"select * from ShowtimesDetails where showtimed_id = " + showtimedId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            ShowtimeDetail showtimeDetail = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    showtimeDetail = GetShowtimeDetail(reader);
                }
            }
            connection.Close();
            return showtimeDetail;
        }
        public List<ShowtimeDetail> GetShowtimeDetailsByShowtimeId(int? showtimeId)
        {
            if (showtimeId == null)
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
            query = $"select * from ShowtimesDetails where showtime_id = " + showtimeId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<ShowtimeDetail> showtimeDetails = null;
            using (reader = command.ExecuteReader())
            {
                showtimeDetails = new List<ShowtimeDetail>();
                while (reader.Read())
                {
                    showtimeDetails.Add(GetShowtimeDetail(reader));
                }
            }
            connection.Close();
            return showtimeDetails;
        }
        public List<ShowtimeDetail> GetShowtimeDetailsByShowtimeIdAndDateNow(int? showtimeId)
        {
            if (showtimeId == null)
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
            query = $"select * from ShowtimesDetails where showtime_id = " + showtimeId + " and showtimed_timeStart >= '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' and showtimed_timeStart <= '" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59).ToString("yyyy/MM/dd HH:mm:ss") + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<ShowtimeDetail> showtimeDetails = null;
            using (reader = command.ExecuteReader())
            {
                showtimeDetails = new List<ShowtimeDetail>();
                while (reader.Read())
                {
                    showtimeDetails.Add(GetShowtimeDetail(reader));
                }
            }
            connection.Close();
            return showtimeDetails;
        }
        public ShowtimeDetail GetShowtimeDetail(MySqlDataReader reader)
        {
            int showtimedId = reader.GetInt32("showtimed_id");
            int showtimeId = reader.GetInt32("showtime_id");
            DateTime showtimedTimeStart = reader.GetDateTime("showtimed_timeStart");
            DateTime showtimedTimeEnd = reader.GetDateTime("showtimed_timeEnd");
            string showtimeRoomSeat = reader.GetString("showtimed_roomSeats");

            ShowtimeDetail showtimeDetail = new ShowtimeDetail(showtimedId, showtimeId, showtimedTimeStart, showtimedTimeEnd, showtimeRoomSeat);
            return showtimeDetail;
        }
    }
}