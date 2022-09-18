using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class DBHelper
    {
        private static string CONNECTION_STRING = "server=localhost;userid=tienmv;password=250220;port=3306;database=CinemaTicketingDB;SslMode=None;";
        public static MySqlConnection OpenDefaultConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = CONNECTION_STRING
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }

        public static MySqlConnection OpenConnection()
        {
            try
            {
                string ConnectionString;
                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    ConnectionString = reader.ReadLine();
                }
                fileStream.Close();
                return OpenConnection(ConnectionString);
            }
            catch
            {

                return null;
            }
        }
        public static MySqlConnection OpenConnection(string ConnectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = ConnectionString
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }

    }
}