using System;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class TicketDAL
    {
        private MySqlConnection connection;
        private string query;

        public TicketDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        public bool SellTicket(ShowtimeDetail showtimeDetail)
        {
            bool result = false;
            if (showtimeDetail == null || showtimeDetail.ShowtimedId == null || showtimeDetail.ShowtimedId == 0 ||
                showtimeDetail.ShowtimeRoomSeat == null || showtimeDetail.ShowtimeRoomSeat.Equals(""))
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
            query = $"update ShowtimesDetails set showtimed_roomSeats = '" + showtimeDetail.ShowtimeRoomSeat + "' where showtimed_id = " + showtimeDetail.ShowtimedId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            if (command.ExecuteNonQuery() > 0)
            {
                result = true;
            }

            connection.Close();

            return result;
        }
    }
}