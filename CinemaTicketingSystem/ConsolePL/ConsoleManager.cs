using System;
using Persistence;
using BL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;

namespace ConsolePL
{
    public class ConsoleManager
    {
        Showtime showtime = new Showtime();
        Movie movie = new Movie();
        List<Room> lr = new List<Room>();
        Cinema cine = new Cinema();
        RoomBL rbl = new RoomBL();
        Room room = new Room();
        ConsoleStaff cs = new ConsoleStaff();
        CinemaBL cbl = new CinemaBL();
        Menus mn = new Menus();
        string line1 = "=====================================================================";
        string line2 = "---------------------------------------------------------------------";
        public void CreateShowtime(User user)
        {
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Tạo lịch chiếu phim.");
            Console.WriteLine(line2);
            Console.WriteLine("[Danh sách phim]\n");
            MovieBL mbl = new MovieBL();
            string[] prop = { "MovieId", "MovieName", "MovieCategory", "MovieTime", "MovieDateStart", "MovieDateEnd" };
            string[] cols = { "Mã phim", "Tên phim", "Thể loại", "Thời lượng(Phút)", "Ngày bắt đầu", "Ngày kết thúc" };
            List<Movie> movies = mbl.GetMoviesByCineId(user.Cine.CineId);
            cs.DisplayTableData(movies, prop, cols, "dd/MM/yyyy");
            Console.WriteLine(line1);
            Console.Write("\nChọn phim(theo mã): ");
            showtime.MovieId = cs.input(Console.ReadLine());
            while (mbl.GetMovieByMovieId(showtime.MovieId) == null)
            {
                Console.Write("Không có mã này, mời bạn nhập lại: ");
                showtime.MovieId = cs.input(Console.ReadLine());
            }
            cine = user.Cine;
            Console.Clear();
            List<ShowtimeDetail> lsd = null;
            int? showtimeNewId;
            ShowtimeBL sbl = new ShowtimeBL();
            Showtime showtimeNew = null;
            while (true)
            {
                Console.WriteLine(line1);
                Console.WriteLine("Tạo lịch chiếu phim.");
                Console.WriteLine(line2);
                Console.WriteLine("[Danh sách phòng]\n");
                string[] proper = { "RoomId", "RoomName", "RTName" };
                string[] col = { "Mã phòng", "Tên phòng", "Loại phòng" };
                lr = rbl.GetRoomsByCineId(cine.CineId);
                cs.DisplayTableData(lr, proper, col, null);
                Console.WriteLine(line1);
                Console.Write("\nChọn phòng(theo mã): ");
                showtime.RoomId = cs.input(Console.ReadLine());
                while (rbl.GetRoomByRoomId(showtime.RoomId) == null)
                {
                    Console.Write("Không có mã này, mời bạn nhập lại: ");
                    showtime.RoomId = cs.input(Console.ReadLine());
                }
                showtimeNew = sbl.GetShowtimeByMovieIdAndRoomId(showtime.MovieId, showtime.RoomId);
                if (showtimeNew == null)
                {
                    showtimeNewId = null;
                }
                else
                {
                    showtimeNewId = showtimeNew.ShowtimeId;
                }
                movie = mbl.GetMovieByMovieId(showtime.MovieId);
                room = rbl.GetRoomByRoomId(showtime.RoomId);

                lsd = DisplayTime(movie, room, showtimeNew);
                if (lsd != null)
                {
                    break;
                }
                Console.Clear();
                Console.WriteLine("Phim: {0} tại phòng: {1} đã đủ xuất chiếu !", movie.MovieName, room.RoomName);
            }
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Tạo lịch chiếu phim.");
            Console.WriteLine(line2);
            Console.WriteLine("[Chi tiết lịch chiếu]\n");
            Console.WriteLine("Bộ phim: " + movie.MovieName);
            Console.WriteLine("Chiếu tại rạp: " + cine.CineName);
            Console.WriteLine("Chiếu tại phòng: {0} - Loại phòng: {1}", room.RoomName, room.RTName);
            Console.WriteLine("Trong các khung giờ từ ngày {0} - {1} cụ thể như sau: ", movie.MovieDateStart.ToString("dd/MM/yyyy"), movie.MovieDateEnd.ToString("dd/MM/yyyy"));
            int count = 1;
            string timeline = "";
            if (showtimeNew != null)
            {
                timeline = showtimeNew.ShowTimeline;
            }
            for (int i = 1; i < lsd.Count - 1; i++)
            {
                if (lsd[0].ShowTimeStart?.ToString("HH:mm") == lsd[i].ShowTimeStart?.ToString("HH:mm"))
                {
                    break;
                }
                count++;
            }
            for (int i = 0; i < count; i++)
            {
                try
                {
                    Console.WriteLine("{0}.{1} -> {2}", i + 1, lsd[i].ShowTimeStart?.ToString("HH:mm"), lsd[i].ShowTimeEnd?.ToString("HH:mm"));
                    if (timeline == "")
                    {
                        timeline = timeline + lsd[i].ShowTimeStart?.ToString("HH:mm");
                    }
                    else
                    {
                        timeline = timeline + ", " + lsd[i].ShowTimeStart?.ToString("HH:mm");
                    }
                }
                catch (System.Exception)
                {

                }
            }
            showtime = new Showtime(showtimeNewId, 0, null, timeline, showtime.RoomId, showtime.MovieId, lsd);
            Console.WriteLine(line1);
            while (true)
            {
                Console.Write("Xác nhận tạo lịch chiếu? (C/K)");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "C":
                        sbl.CreateShowtime(showtime);
                        break;
                    case "c":
                        sbl.CreateShowtime(showtime);
                        Console.ReadKey();
                        break;
                    case "K":
                        break;
                    case "k":
                        break;
                    default:
                        continue;
                }
                break;
            }
            Console.Clear();
            mn.menuManager(user);
        }
        public List<ShowtimeDetail> DisplayTime(Movie movie, Room room, Showtime showtime)
        {
            List<ShowtimeDetail> lsd = new List<ShowtimeDetail>();
            ShowtimeDetail showtimeDetail = null;
            Console.Clear();
            string row1 = "=====================================================================";
            string row2 = "---------------------------------------------------------------------";
            Console.WriteLine(row1);
            Console.WriteLine("Tạo lịch chiếu phim.");
            Console.WriteLine(row2);
            Console.WriteLine("[Danh sách khung giờ có thể chiếu]\n");
            // Console
            List<string> start = new List<string>();
            List<string> end = new List<string>();
            start.Add("07:00");
            int i = 0;
            int? convert = TimeToInt(start[0]);
            end.Add(IntToTime(convert + movie.MovieTime));
            while (TimeToInt(start[i]) < 23 * 60)
            {
                convert = TimeToInt(start[i]);
                start.Add(IntToTime(convert + movie.MovieTime + 10));
                i++;
                convert = TimeToInt(start[i]);
                end.Add(IntToTime(convert + movie.MovieTime));
            }
            start.RemoveAt(i);
            end.RemoveAt(i);

            if (showtime != null)
            {
                string timeline = showtime.ShowTimeline.Replace(" ", "");
                string[] timelineArr = timeline.Split(",");

                for (int j = 0; j < timelineArr.Length; j++)
                {
                    for (int k = 0; k < start.Count; k++)
                    {
                        if (timelineArr[j] == start[k])
                        {
                            start.RemoveAt(k);
                            end.RemoveAt(k);
                        }
                    }
                }

                if (start.Count == 0)
                {
                    return null;
                }
            }
            // start.Sort();
            for (int k = 0; k < start.Count; k++)
            {
                Console.WriteLine("{0}: {1} -> {2}", k + 1, start[k], end[k]);
            }
            Console.WriteLine(row1);

            Console.Write("Chọn các khung giờ (theo các số thứ tự): ");
            string time = Console.ReadLine();

            time = time.Replace(" ", "");
            if (time.Substring(time.Length - 1) == ",")
            {
                time = time.Substring(0, time.Length - 1);
            }
            // Console.WriteLine(time);
            Regex regex = new Regex("[0-9,]");
            // Regex regex1 = new Regex("[1-9][0-9]");

            // MatchCollection matchcollection1 = regex1.Matches(time);
            string[] timeArr;

            while (true)
            {
                MatchCollection matchcollection = regex.Matches(time);
                timeArr = time.Split(",");
                if (matchcollection.Count != time.Length)
                {
                    Console.Write("Nhập các khung giờ sai định dạng, mời bạn nhập lại (ví dụ: 1,2...): ");
                    time = Console.ReadLine();
                    time = time.Replace(" ", "");
                    if (time.Substring(time.Length - 1) == ",")
                    {
                        time = time.Substring(0, time.Length - 1);
                    }
                    matchcollection = regex.Matches(time);
                    continue;
                }

                int check = 0;
                for (i = 0; i < timeArr.Length - 1; i++)
                {
                    for (int j = i + 1; j < timeArr.Length; j++)
                    {
                        if (timeArr[i] == timeArr[j])
                        {
                            check = 1;
                            break;
                        }
                    }
                    if (check == 1)
                    {
                        break;
                    }
                }
                if (check == 1)
                {
                    Console.Write("Nhập trùng khung giờ, mời bạn nhập lại: ");
                    time = Console.ReadLine();
                    time = time.Replace(" ", "");
                    if (time.Substring(time.Length - 1) == ",")
                    {
                        time = time.Substring(0, time.Length - 1);
                    }
                    continue;
                }

                int count = 0;
                for (i = 0; i < timeArr.Length; i++)
                {
                    // Console.WriteLine(timeArr[i]);
                    for (int k = 0; k < start.Count; k++)
                    {
                        if (Convert.ToInt32(timeArr[i].Trim()) == k + 1)
                        {
                            count++;
                            break;
                        }
                    }
                }
                for (int j = 0; j < timeArr.Length - 1; j++)
                {
                    for (int k = j + 1; k < timeArr.Length; k++)
                    {
                        if (Convert.ToInt16(timeArr[k]) < Convert.ToInt16(timeArr[j]))
                        {
                            string temp = timeArr[j];
                            timeArr[j] = timeArr[k];
                            timeArr[k] = temp;
                        }
                    }
                }
                DateTime daystart = movie.MovieDateStart;
                DateTime dayend = movie.MovieDateEnd;
                int dem = 0;
                if (count == timeArr.Length && count > 0)
                {
                    while (DateTime.Compare(daystart, dayend) <= 0)
                    {
                        dem++;
                        for (i = 0; i < timeArr.Length; i++)
                        {
                            showtimeDetail = new ShowtimeDetail();
                            int index = Convert.ToInt32(timeArr[i]);
                            int[] hours = { TimeToInt(start[index - 1]) / 60, TimeToInt(end[index - 1]) / 60 };
                            int[] minutes = { TimeToInt(start[index - 1]) % 60, TimeToInt(end[index - 1]) % 60 };
                            showtimeDetail.ShowtimeRoomSeat = room.RoomSeats;
                            showtimeDetail.ShowTimeStart = new DateTime(daystart.Year, daystart.Month, daystart.Day, hours[0], minutes[0], 0);
                            showtimeDetail.ShowTimeEnd = new DateTime(daystart.Year, daystart.Month, daystart.Day, hours[1], minutes[1], 0);
                            showtimeDetail.ShowtimedId = null;
                            showtimeDetail.ShowtimeId = null;
                            lsd.Add(showtimeDetail);
                        }
                        daystart = daystart.AddDays(1);
                    }
                    break;
                }
                else
                {
                    Console.Write("Không có khung giờ, mời bạn nhập lại: ");
                    time = Console.ReadLine();
                    continue;
                }
            }
            return lsd;
        }
        public int RoomDisplay(User user)
        {
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Tạo lịch chiếu phim.");
            Console.WriteLine(line2);
            Console.WriteLine("[Danh sách phòng]\n");
            string[] proper = { "RoomId", "RoomName", "RTName" };
            string[] col = { "ID", "Tên phòng", "Loại phòng" };
            lr = rbl.GetRoomsByCineId(user.Cine.CineId);
            cs.DisplayTableData(lr, proper, col, null);
            Console.WriteLine(line1);
            Console.WriteLine("\nChọn phòng(Theo ID): ");
            showtime.RoomId = cs.input(Console.ReadLine());
            while (rbl.GetRoomByRoomId(showtime.RoomId) == null)
            {
                Console.Write("Không có ID này, mời nhập lại: ");
                showtime.RoomId = cs.input(Console.ReadLine());
            }
            return showtime.RoomId;
        }
        public int TimeToInt(string timeStr)
        {
            string[] timeStrA = timeStr.Split(":");
            int time = 0;
            if (timeStrA.Length == 2)
            {
                int h = Convert.ToInt32(timeStrA[0]);
                int m = Convert.ToInt32(timeStrA[1]);
                time = h * 60 + m;
            }
            return time;
        }
        public string IntToTime(int? timeInt)
        {
            string strH = (timeInt / 60).ToString();
            if (strH.Length == 1)
            {
                strH = "0" + strH;
            }
            string strM = (timeInt % 60).ToString();
            if (strM.Length == 1)
            {
                strM = "0" + strM;
            }
            string timeStr = strH + ":" + strM;
            return timeStr;
        }
    }
}