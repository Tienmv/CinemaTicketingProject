using System;
using System.Collections.Generic;
using DAL;
using Persistence;
using MySql.Data.MySqlClient;
using Xunit;

namespace DAL_Test
{
    public class ShowtimeTest
    {
        private ShowtimeDAL showtimeDAL = new ShowtimeDAL();
        private MySqlConnection connection = DBHelper.OpenConnection();
        private MySqlDataReader reader;
        private string query;

        [Fact]
        public void CreateShowtimeTest1()
        {
            List<ShowtimeDetail> showtimeDALs = new List<ShowtimeDetail>();
            DateTime timeStart = new DateTime(2022, 8, 2, 7, 0, 0);
            DateTime timeEnd = new DateTime(2022, 8, 2, 9, 12, 0);
            showtimeDALs.Add(new ShowtimeDetail(null, null, timeStart, timeEnd, "Room Seat"));
            showtimeDALs.Add(new ShowtimeDetail(null, null, timeStart, timeEnd, "Room Seat"));
            Showtime showtime = new Showtime(null, 0, null, "07:00, 08:00", 1, 3, showtimeDALs);

            Assert.True(showtimeDAL.CreateShowtime(showtime));
        }
    }
}