using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persistence;

namespace ConsolePL
{
    public class ConsoleStaff
    {
        TicketBL tbl = new TicketBL();
        Showtime showtime = new Showtime();
        ShowtimeDetail showtimeDetail = new ShowtimeDetail();
        ShowtimeBL sbl = new ShowtimeBL();
        RoomBL rbl = new RoomBL();
        Cinema cine = new Cinema();
        Movie movie = new Movie();
        ShowtimeDetailBL sdbl = new ShowtimeDetailBL();
        CinemaBL cbl = new CinemaBL();
        Menus mn = new Menus();
        PriceSeatOfRoomTypeBL psortbl = new PriceSeatOfRoomTypeBL();
        string line1 = "=====================================================================";
        string line2 = "---------------------------------------------------------------------";
        public void Ticket(User us)
        {
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Đặt vé.");
            Console.WriteLine(line2);
            Console.WriteLine("[Danh sách phim]\n");
            MovieBL mbl = new MovieBL();
            string[] prop = { "MovieId", "MovieName", "MovieCategory", "MovieTime", "MovieDateStart", "MovieDateEnd" };
            string[] cols = { "Mã phim", "Tên phim", "Thể loại", "Thời lượng(Phút)", "Ngày bắt đầu", "Ngày kết thúc" };
            List<Movie> movies = mbl.GetMoviesByCineIdAndDateNow(us.Cine.CineId);
            cine = cbl.GetCinemaByCineId(us.Cine.CineId);
            DisplayTableData(movies, prop, cols, "dd/MM/yyyy");
            Console.WriteLine(line1);
            Console.Write("\nChọn phim(theo mã): ");
            showtime.MovieId = input(Console.ReadLine());
            while (mbl.GetMovieByMovieId(showtime.MovieId) == null)
            {
                Console.Write("Không có mã này, mời bạn nhập lại: ");
                showtime.MovieId = input(Console.ReadLine());
            }
            movie = mbl.GetMovieByMovieId(showtime.MovieId);

            List<Showtime> ls = sbl.GetShowtimesByMovieId(showtime.MovieId);
            List<ShowtimeDetail> lsd = new List<ShowtimeDetail>();
            foreach (var itemListShowtime in ls)
            {
                List<ShowtimeDetail> newlsd = sdbl.GetShowtimeDetailsByShowIdAndTimeNow(itemListShowtime.ShowtimeId);
                foreach (var itemListShowtimeDetail in newlsd)
                {
                    lsd.Add(itemListShowtimeDetail);
                }
            }
            if (lsd.Count == 0)
            {
                while (true)
                {
                    Console.Write("Không còn lịch chiếu cho phim bạn chọn trong ngày hôm nay. Bạn có muốn chọn phim khác ?(C/K)");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "C":
                            Ticket(us);
                            return;
                        case "c":
                            Ticket(us);
                            return;
                        case "K":
                            mn.menuStaff(us);
                            return;
                        case "k":
                            mn.menuStaff(us);
                            return;
                        default:
                            continue;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Đặt vé.");
            Console.WriteLine(line2);
            Console.WriteLine("[Danh sách lịch chiếu]");
            int count = 0;
            ConsoleManager cm = new ConsoleManager();
            for (int i = 0; i < lsd.Count - 1; i++)
            {
                for (int j = i + 1; j < lsd.Count; j++)
                {
                    int time1 = cm.TimeToInt(lsd[i].ShowTimeStart?.ToString("HH:mm"));
                    int time2 = cm.TimeToInt(lsd[j].ShowTimeStart?.ToString("HH:mm"));
                    if (time1 > time2)
                    {
                        ShowtimeDetail newsd = new ShowtimeDetail();
                        newsd = lsd[i];
                        lsd[i] = lsd[j];
                        lsd[j] = newsd;
                    }
                }
            }
            foreach (var itemlsd in lsd)
            {
                count++;
                Showtime showtimez = new Showtime();
                showtimez = sbl.GetShowtimeByShowtimeId(itemlsd.ShowtimeId);
                Room rooms = rbl.GetRoomByRoomId(showtimez.RoomId);
                Console.WriteLine(count + ". Bắt đầu từ: " + itemlsd.ShowTimeStart?.ToString("HH:mm") + " -> " + itemlsd.ShowTimeEnd?.ToString("HH:mm") + " Tại phòng: " + rooms.RoomName);
            }
            Console.WriteLine(line1);
            Console.Write("Chọn lịch chiếu (theo số thứ tự): ");
            int showno = input(Console.ReadLine());
            while (showno > lsd.Count)
            {
                Console.Write("Chọn sai lịch chiếu, mời nhập lại");
                showno = input(Console.ReadLine());
            }
            int? showtimedId = lsd[showno - 1].ShowtimedId;
            showtimeDetail = sdbl.GetShowtimeDetailByShowId(showtimedId);

            string seatStr = null;
            string[] seat;
            Console.Clear();
            while (true)
            {
                Console.WriteLine(line1);
                Console.WriteLine("Đặt vé");
                Console.WriteLine(line2);
                Console.WriteLine("[Bản đồ phòng chiếu]");
                try
                {
                    seatStr = ChoiceSeats(showtimeDetail);
                    seat = seatStr.Split(",");
                }
                catch (System.Exception)
                {
                    seat = null;
                }
                if (seat == null)
                {
                    Console.Clear();
                    Console.WriteLine("KHÔNG TÌM THẤY GHẾ !!!");
                    continue;
                }
                else if (seat[0] == "same")
                {
                    Console.Clear();
                    Console.WriteLine("BẠN NHẬP SỐ GHẾ TRÙNG NHAU !!!");
                    continue;
                }
                else
                {
                    break;
                }
            }
            foreach (var itemls in ls)
            {
                if (itemls.ShowtimeId == showtimeDetail.ShowtimeId)
                {
                    showtime = itemls;
                }
            }
            Room room = new Room();
            room = rbl.GetRoomByRoomId(showtime.RoomId);

            List<PriceSeatOfRoomType> lpsort = psortbl.GetPriceSeatOfRoomTypeByRTName(room.RTName);
            List<PriceSeatOfRoomType> lpsort1 = new List<PriceSeatOfRoomType>();

            foreach (var itemlpsort in lpsort)
            {
                foreach (var item in seat)
                {
                    if (itemlpsort.STType == item[0].ToString())
                    {
                        lpsort1.Add(new PriceSeatOfRoomType(itemlpsort.RTName, itemlpsort.STType, itemlpsort.Price));
                    }
                }
            }
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine("Đặt vé");
            Console.WriteLine(line2);
            Console.WriteLine("[Thông tin vé đã chọn]");
            double price = 0;
            Console.WriteLine("Phim: {0}", movie.MovieName);
            Console.WriteLine("Thời gian chiếu: {0} - {0} -> {1}", showtimeDetail.ShowTimeStart?.ToString("dd/MM/yyyy"), showtimeDetail.ShowTimeStart?.ToString("HH:mm"), showtimeDetail.ShowTimeEnd?.ToString("HH:mm"));
            Console.WriteLine("Các ghế đã chọn: {0}", seatStr);
            for (int i = 0; i < lpsort1.Count; i++)
            {
                price += lpsort1[i].Price;
            }
            Console.WriteLine(line1);
            Console.WriteLine("Tổng tiền vé: {0}", pricevalid(price));
            while (true)
            {
                Console.Write("Xác nhận mua vé ?(C/K)");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "C":
                        tbl.SellTicket(showtimeDetail);
                        break;
                    case "c":
                        tbl.SellTicket(showtimeDetail);
                        break;
                    case "K":
                        mn.menuStaff(us);
                        break;
                    case "k":
                        mn.menuStaff(us);
                        break;
                    default:
                        continue;
                }
                break;
            }
            Console.Write("Nhập số tiền khách hàng trả(đồng): ");
            double cusmoney;
            while (true)
            {
                try
                {
                    cusmoney = Convert.ToDouble(Console.ReadLine());
                    if (cusmoney <= 0)
                    {
                        Console.Write("Số tiền khách trả phải lớn hơn 0, mời bạn nhập lại: ");
                        continue;
                    }
                    if (cusmoney < price)
                    {
                        Console.Write("Số tiền nhập không đủ, mời bạn nhập lại: ");
                        continue;
                    }
                    if (cusmoney > 9999999999)
                    {
                        Console.Write("Số tiền quá lớn, mời bạn nhập lại: ");
                        continue;
                    }
                }
                catch
                {
                    Console.Write("Bạn phải nhập số tiền bằng số, mời bạn nhập lại: ");
                    continue;
                }
                break;
            }
            if ((cusmoney - price) > 0)
            {
                Console.WriteLine("Tiền trả lại: {0}", pricevalid(cusmoney - price));
            }
            Console.WriteLine(line1);
            Console.WriteLine("Thanh toán thành công!!!\nBấm phím bất kì để in vé.");
            Console.ReadKey();
            Console.Clear();
            for (int i = 0; i < lpsort1.Count; i++)
            {
                PrintTicket(showtimeDetail, showtime, room, movie, lpsort1[i], us, cine, seat[i]);
            }
            Console.WriteLine("In vé thành công!!!\nBấm phím bất kì để thoát.");
            Console.ReadKey();
            Console.Clear();

            mn.menuStaff(us);
        }
        public double PrintTicket(ShowtimeDetail sched, Showtime sche, Room room, Movie movie, PriceSeatOfRoomType psort, User user, Cinema cine, string seat)
        {
            string a = "";
            string b = "";
            int inde = (cine.CineAddress.Length) / 2;
            for (int j = inde; j > 0; j--)
            {
                if (cine.CineAddress[j - 1] == ',')
                {
                    a = cine.CineAddress.Substring(0, j);
                    b = cine.CineAddress.Substring(j + 1, cine.CineAddress.Length - inde);
                    break;
                }

            }
            string price = pricevalid(psort.Price);
            string[] left = {cine.CineName,a,b,cine.CinePhone,
            DateTime.Now.ToString("dd/MM/yyyy")+"     "+sched.ShowTimeStart?.ToString("HH:mm")+
            " - "+sched.ShowTimeEnd?.ToString("HH:mm"),movie.MovieName,
            psort.STType+" Ghế"+" Rạp",price+" "+seat+" "+room.RoomName,
            "Gồm"+" Seat"+" Cinema","5% VAT",DateTime.Now.ToString("HH:mm dd/MM/yyyy")+"      "+user.Username};
            string[] right = {cine.CineName,a,b,movie.MovieName,"Time: "+sched.ShowTimeStart?.ToString("HH:mm")+
            " - "+sched.ShowTimeEnd?.ToString("HH:mm"),"Date: "+DateTime.Now.ToString("dd/MM/yyyy"),
            "Hall: "+room.RoomName,"Seat: "+seat,psort.STType,"Price: "+price,DateTime.Now.ToString("HH:mm dd/MM/yyyy")};
            int length = 0;
            int length1 = 0;
            int length2 = 0;

            if ((left[2]).Length > left[5].Length)
            {
                length = (left[2] + left[2]).Length;
                length1 = left[2].Length;
                length2 = left[2].Length;
            }
            else if (true)
            {
                length = (left[5] + left[5]).Length;
                length1 = left[5].Length;
                length2 = left[5].Length;
            }

            for (int i = 0; i < length + 7; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
            for (int i = 0; i < left.Length; i++)
            {
                string lefti;
                if (i > 5 && i < 9)
                {
                    string[] arr = left[i].Split(" ");
                    Console.Write("| " + arr[0]);
                    for (int l = 0; l < 12 - arr[0].ToString().Length; l++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(arr[1]);
                    for (int j = 0; j < 7 - arr[1].ToString().Length; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(arr[2]);
                    for (int o = 0; o < length1 - 12 - 7 - arr[2].Length; o++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {
                    lefti = "| " + left[i];
                    Console.Write(lefti);
                    for (int j = 0; j < length1 - lefti.Length + 2; j++)
                    {
                        Console.Write(" ");
                    }
                }


                string righti = " | " + right[i];
                Console.Write(righti);
                for (int k = 0; k < length2 - righti.Length + 3; k++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(" |");

            }
            for (int i = 0; i < length + 7; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine();
            return psort.Price;
        }
        public string pricevalid(double price)
        {
            string prices = price.ToString();
            string pricez = "";
            int balance = (prices.Length - 1) % 3;
            for (int i = 0; i < prices.Length; i++)
            {
                if (i == prices.Length - 1)
                {
                    pricez = pricez + prices[i];
                }
                else if ((i - balance) % 3 == 0)
                {
                    pricez = pricez + prices[i] + ".";
                }
                else
                {
                    pricez = pricez + prices[i].ToString();
                }
            }
            pricez = pricez + "đ";
            return pricez;
        }
        public static string ChoiceSeats(ShowtimeDetail showtimeDetail)
        {
            string roomSeats = showtimeDetail.ShowtimeRoomSeat;
            string[] seats = roomSeats.Split(" ");

            DrawRoomSeats(seats);

            Console.WriteLine();
            Console.Write("Chọn ghế (VD: A1,A2): ");
            string choice = Console.ReadLine().ToUpper();
            choice = choice.Replace(" ", "");

            if (choice.Substring(choice.Length - 1) == ",")
            {
                choice = choice.Substring(0, choice.Length - 1);
            }
            string[] choiced = choice.Split(",");

            for (int i = 0; i < choiced.Length; i++)
            {
                if (choiced[i] == "XX" || choiced[i] == "__")
                {
                    return null;
                }
                for (int j = i + 1; j < choiced.Length; j++)
                {
                    if (choiced[i] == choiced[j])
                    {
                        return "same";
                    }
                }
            }

            int count = 0;

            for (int i = 0; i < choiced.Length; i++)
            {
                for (int j = 0; j < seats.Length; j++)
                {
                    if (seats[j] != "." && seats[j] != "n")
                    {
                        string seatz = seats[j].Substring(2);
                        if (choiced[i] == seatz)
                        {
                            choiced[i] = seats[j];
                            seats[j] = new String('X', seats[j].Length);
                            count++;
                            break;
                        }
                    }
                }
            }
            if (count == choiced.Length && count > 0)
            {
                roomSeats = "";
                choice = "";
                for (int i = 0; i < seats.Length; i++)
                {
                    roomSeats += seats[i] + " ";
                }
                showtimeDetail.ShowtimeRoomSeat = roomSeats.Trim();
                for (int i = 0; i < choiced.Length; i++)
                {
                    choice += choiced[i] + ",";
                }
                return choice.Substring(0, choice.Length - 1);
            }
            return null;
        }
        public static void DrawRoomSeats(string[] seats)
        {
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i] == "n")
                {
                    Console.WriteLine();
                }
                else if (seats[i] == ".")
                {
                    Console.Write(" | ");
                }
                else
                {
                    Console.Write(" " + seats[i] + " ");
                }
            }
            Console.WriteLine("X: Ghế đã mua --- V: Ghế VIP --- N: Ghế thường --- D: Ghế đôi");
        }
        public int input(string str)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(str);
            while ((matchCollection.Count < str.Length) || (str == ""))
            {
                Console.Write("Dữ liệu nhập vào phải là số tự nhiên, mời bạn nhập lại");
                str = Console.ReadLine();
                matchCollection = regex.Matches(str);
            }
            return Convert.ToInt32(str);
        }
        public bool DisplayTableData<T>(List<T> list, string[] prop, string[] cols, string formatDate)
        {
            if (list == null)
            {
                return false;
            }
            int[] widthCols = new int[cols.Length];
            int width = 0;
            string row = null;
            for (int i = 0; i < cols.Length; i++)
            {
                widthCols[i] = cols[i].Length;
            }
            foreach (var item in list)
            {
                for (int i = 0; i < prop.Length; i++)
                {
                    if (item.GetType().GetProperty(prop[i]).GetValue(item) != null)
                    {
                        int l = 0;
                        if (item.GetType().GetProperty(prop[i]).GetValue(item).GetType() == typeof(DateTime))
                        {
                            DateTime date = DateTime.Parse(item.GetType().GetProperty(prop[i]).GetValue(item).ToString());
                            l = date.ToString(formatDate).Length;
                        }
                        else
                        {
                            l = (item.GetType().GetProperty(prop[i]).GetValue(item).ToString()).Length;
                        }
                        if (l > widthCols[i])
                        {
                            widthCols[i] = l;
                        }
                    }
                }
            }
            for (int i = 0; i < cols.Length; i++)
            {
                row += String.Format("| {0," + -widthCols[i] + "} |", cols[i]);
                width += (widthCols[i] + 4);
            }
            Console.WriteLine(row);
            row = String.Format("{0}", new String('-', width));
            Console.WriteLine(row);

            foreach (var item in list)
            {
                row = null;
                for (int i = 0; i < prop.Length; i++)
                {
                    if (item.GetType().GetProperty(prop[i]).GetValue(item) != null)
                    {
                        if (item.GetType().GetProperty(prop[i]).GetValue(item).GetType() == typeof(DateTime))
                        {
                            DateTime date = DateTime.Parse(item.GetType().GetProperty(prop[i]).GetValue(item).ToString());
                            row += string.Format("| {0," + -widthCols[i] + "} |", date.ToString(formatDate));
                        }
                        else
                        {
                            row += String.Format("| {0," + -widthCols[i] + "} |", item.GetType().GetProperty(prop[i]).GetValue(item).ToString());
                        }
                    }
                    else
                    {
                        row += String.Format("| {0," + -widthCols[i] + "} |", "");
                    }
                }
                Console.WriteLine(row);
            }
            return true;
        }
    }
}