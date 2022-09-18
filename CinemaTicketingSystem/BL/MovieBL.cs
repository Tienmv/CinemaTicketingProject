using System;
using Persistence;
using DAL;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BL
{
    public class MovieBL
    {
        private MovieDAL mdal = new MovieDAL();
        public Movie GetMovieByMovieId(int? movieId)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(movieId.ToString());
            if (movieId == null)
            {
                return null;
            }
            else if (matchCollection.Count < movieId.ToString().Length)
            {
                return null;
            }
            return mdal.GetMovieByMovieId(movieId);
        }
        public List<Movie> GetMoviesByCineId(int? cineId){
            if (cineId == null)
            {
                return null;
            }

            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(cineId.ToString());
            if (matchCollection.Count < cineId.ToString().Length)
            {
                return null;
            }
            return mdal.GetMoviesByCineId(cineId);
        }
        public List<Movie> GetMoviesByCineIdAndDateNow(int? cineId){
            if (cineId == null)
            {
                return null;
            }
            return mdal.GetMoviesByCineIdAndDateNow(cineId);
        }
    }
}