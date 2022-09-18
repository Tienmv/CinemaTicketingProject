using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class UserDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;

        public UserDAL(){
            connection = DBHelper.OpenConnection();
        }
        public User Login(string username, string password)
        {
            if ((username == null) || (password == null))
            {
                return null;  
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUsername = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUsername.Count < username.Length || matchCollectionPassword.Count < password.Length)
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
            query = @"select * from Accounts where acc_username = '" + username + "' and acc_password = '" + password + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            User user = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = GetUser(reader);
                }
            }

            connection.Close();

            if (user != null)
            {
                CinemaDAL cinemaDAL = new CinemaDAL();
                Cinema cine = cinemaDAL.GetCinemaByCineId(user.Cine.CineId);
            }
            return user;
        }
        private User GetUser(MySqlDataReader reader){
            string username = reader.GetString("acc_username");
            string password = reader.GetString("acc_password");
            string type = reader.GetString("acc_type");
            Cinema cine = new Cinema(reader.GetInt32("cine_id"), null, null, null);
            User user = new User(username, password, type, cine);
            return user;
        }
    }
}