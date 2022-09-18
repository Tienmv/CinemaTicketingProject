using System;
using System.Collections.Generic;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class RoomDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;

        public RoomDAL(){
            connection = DBHelper.OpenConnection();
        }
        public Room GetRoomByRoomId(int? roomId){
            if (roomId == null)
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
            query = $"select * from Rooms where room_id = " + roomId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Room room = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    room = GetRoom(reader);
                }
            }
            connection.Close();

            return room;
        }
        public List<Room> GetRoomsByCineId(int? cineId){
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
            query = $"select * from Rooms where cine_id = " + cineId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Room> rooms = null;
            using (reader = command.ExecuteReader())
            {
                rooms = new List<Room>();
                while (reader.Read())
                {
                    rooms.Add(GetRoom(reader));
                }
            }
            connection.Close();

            return rooms;
        }

        public Room GetRoom(MySqlDataReader reader){
            int roomId = reader.GetInt32("room_id");
            string roomName = reader.GetString("room_name");
            string roomSeats = reader.GetString("room_seats");
            string rtName = reader.GetString("rt_name");

            Room room = new Room(roomId, roomName, roomSeats, rtName);

            return room;
        }
    }
}