using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class ShowtimeBL
    {
        ShowtimeDAL sdal = new ShowtimeDAL();
        public bool CreateShowtime(Showtime showtime){
            return sdal.CreateShowtime(showtime);
        }
        public List<Showtime> GetShowtimesByMovieId(int? movieId){
            if (movieId == null)
            {
                return null;
            }
            return sdal.GetShowtimesByMovieId(movieId);
        }
        public Showtime GetShowtimeByMovieIdAndRoomId(int? movieId, int? roomId){
            if ((movieId == null) || (roomId == null))
            {
                return null;
            }
            return sdal.GetShowtimeByMovieIdAndRoomId(movieId, roomId);
        }
        public Showtime GetShowtimeByShowtimeId(int? showtimeId){
            if (showtimeId == null)
            {
                return null;
            }
            return sdal.GetShowtimeByShowtimeId(showtimeId);
        }
    }
}