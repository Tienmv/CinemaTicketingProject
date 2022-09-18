using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace DAL
{
    public class PriceSeatOfRoomTypeDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;

        public PriceSeatOfRoomTypeDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public List<PriceSeatOfRoomType> GetPriceSeatOfRoomTypesByRTName(string rtName){
            if (rtName == null)
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

            query = @"select * from PriceSeatsOfRoomTypes where rt_name = '" + rtName + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<PriceSeatOfRoomType> priceSeatOfRoomTypes = null;
            using(reader = command.ExecuteReader())
            {
                priceSeatOfRoomTypes = new List<PriceSeatOfRoomType>();
                while (reader.Read())
                {
                    priceSeatOfRoomTypes.Add(GetPriceSeatOfRoomTypes(reader));
                }
            }
            connection.Close();
            return priceSeatOfRoomTypes;

        }
        public PriceSeatOfRoomType GetPriceSeatOfRoomTypes(MySqlDataReader reader){
            string rtName = reader.GetString("rt_name");
            string stType = reader.GetString("st_type");
            double price = reader.GetDouble("price");

            PriceSeatOfRoomType priceSeatOfRoomType = new PriceSeatOfRoomType(rtName, stType, price);
            return priceSeatOfRoomType;
        }
    }
}