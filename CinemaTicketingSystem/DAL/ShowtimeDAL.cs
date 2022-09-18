using System;
using System.Collections.Generic;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ShowtimeDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public ShowtimeDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public bool CreateShowtime(Showtime showt)
        {
            bool result = false;
            if (showt == null || showt.ShowtimeDetails == null || showt.ShowtimeDetails.Count == 0)
            {
                return result;
            }
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;

            //Lock Tables
            command.CommandText = @"lock tables Showtimes write, ShowtimesDetails write;";
            command.ExecuteNonQuery();

            //Transaction
            MySqlTransaction trans = connection.BeginTransaction();
            command.Transaction = trans;

            try
            {
                int? showtId = 0;
                if (showt.ShowtimeId == null)
                {
                    //Insert Showtime
                    int roomId = showt.RoomId;
                    int movieId = showt.MovieId;
                    int showtimeStatus = showt.ShowtimeStatus;
                    string showtimeWeekdays = showt.ShowtimeWeekdays;
                    string showTimeline = showt.ShowTimeline;
                    query = @"insert into Showtimes(room_id, movie_id, showtime_status, showtime_weekdays, showtime_timeline) values" +
                             "(" + roomId + ", " + movieId + ", " + showtimeStatus + ", '" + showtimeWeekdays + "', '" + showTimeline + "');";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    //Insert ShowtimesDetails
                    command.CommandText = "select LAST_INSERT_ID() as showtime_id;";
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            showtId = reader.GetInt32("showtime_id");
                        }
                    }
                }
               else
                {
                    showtId = showt.ShowtimeId;
                    query = @"update Showtimes set showtime_timeline = '" + showt.ShowTimeline + "' where showtime_id = " + showtId + ";";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }

                query = @"insert into ShowtimesDetails(showtime_id, showtimed_timeStart, showtimed_timeEnd, showtimed_roomSeats) values";
                string showtimeDetailValue;
                string? timeStart;
                string? timeEnd;
                foreach (ShowtimeDetail sdt in showt.ShowtimeDetails)
                {
                    sdt.ShowtimeId = showtId;
                    timeStart = sdt.ShowTimeStart?.ToString("yyyy/MM/dd HH:mm:ss");
                    timeEnd = sdt.ShowTimeEnd?.ToString("yyyy/MM/dd HH:mm:ss");
                    showtimeDetailValue = "(" + sdt.ShowtimeId + ",'" + timeStart + "','" + timeEnd + "','" + sdt.ShowtimeRoomSeat + "'),";

                    query = query + showtimeDetailValue;
                }
                query = query.Substring(0, query.Length - 1) + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();

                trans.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                string m = e.Message;
                trans.Rollback();
            }
            finally
            {
                //Unlock Tables
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
        public List<Showtime> GetShowtimesByMovieId(int? movieId)
        {
            if (movieId == null)
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
            query = $"select * from Showtimes where movie_id = " + movieId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Showtime> showtimes = null;
            using (reader = command.ExecuteReader())
            {
                showtimes = new List<Showtime>();
                while (reader.Read())
                {
                    showtimes.Add(GetShowtime(reader));
                }
            }
            connection.Close();
            return showtimes;
        }
        public Showtime GetShowtimeByShowtimeId(int? showtimeId)
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
            query = $"select * from Showtimes where showtime_id = " + showtimeId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Showtime showtime = null;
            using (reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    showtime = GetShowtime(reader);
                }
            }
            connection.Close();

            return showtime;
        }
        public Showtime GetShowtimeByMovieIdAndRoomId(int? movieId, int? roomId)
        {
            if ((movieId == null) || (roomId == null))
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
            query = $"select * from Showtimes where movie_id = " + movieId + " and room_id = " + roomId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Showtime showtime = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    showtime = GetShowtime(reader);
                }
            }
            connection.Close();
            return showtime;
        }
        public Showtime GetShowtime(MySqlDataReader reader)
        {
            int showtimeId = reader.GetInt32("showtime_id");
            int roomId = reader.GetInt32("room_id");
            int movieId = reader.GetInt32("movie_id");
            int ShowtimeStatus = reader.GetInt32("showtime_status");
            string ShowtimeWeekdays = reader.GetString("showtime_weekdays");
            string ShowTimeline = reader.GetString("showtime_timeline");
            Showtime showtime = new Showtime(showtimeId, ShowtimeStatus, ShowtimeWeekdays, ShowTimeline, roomId, movieId, null);
            return showtime;
        }
    }
}