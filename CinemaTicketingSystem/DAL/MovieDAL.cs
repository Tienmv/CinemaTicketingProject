using System;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class MovieDAL
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public MovieDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public Movie GetMovieByMovieId(int? movieId)
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

            query = $"select * from Movies where movie_id = " + movieId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Movie movie = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    movie = GetMovie(reader);
                }
                connection.Close();

                return movie;
            }
        }
        public List<Movie> GetMoviesByCineId(int? cineId)
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

            query = $"select * from Shows inner join Movies on Shows.movie_id = Movies.movie_id where cine_id = " + cineId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Movie> movies = null;
            using (reader = command.ExecuteReader())
            {
                movies = new List<Movie>();
                while (reader.Read())
                {
                    movies.Add(GetMovie(reader));
                }
            }
            connection.Close();
            return movies;
        }
        public List<Movie> GetMoviesByCineIdAndDateNow(int? cineId)
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
            query = $"select * from Shows inner join Movies on Shows.movie_id = Movies.movie_id where cine_id = " + cineId + " and  movie_dateStart <= '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and movie_dateEnd >= '" + DateTime.Now.ToString("yyyy/MM/dd") + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Movie> movies = null;
            using (reader = command.ExecuteReader())
            {
                movies = new List<Movie>();
                while (reader.Read())
                {
                    movies.Add(GetMovie(reader));
                }
            }
            connection.Close();
            return movies;
        }
        public Movie GetMovie(MySqlDataReader reader)
        {
            int movieId = reader.GetInt32("movie_id");
            string movieName = reader.GetString("movie_name");
            string movieDescription = reader.GetString("movie_description");
            string movieAuthor = reader.GetString("movie_author");
            string movieActor = reader.GetString("movie_actor");
            string movieCategory = reader.GetString("movie_category");
            int movieTime = reader.GetInt32("movie_time");
            DateTime movieDateStart = reader.GetDateTime("movie_dateStart");
            DateTime movieDateEnd = reader.GetDateTime("movie_dateEnd");

            Movie movie = new Movie(movieId, movieName, movieDescription, movieAuthor, movieActor,
                                    movieCategory, movieTime, movieDateStart, movieDateEnd);
            return movie;
        }
    }
}