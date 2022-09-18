using System;
using System.Text;
using Persistence;
using System.Text.RegularExpressions;
using BL;

namespace ConsolePL
{
    public class Menus
    {
        string line1 = "=====================================================================";
        string line2 = "---------------------------------------------------------------------";
        public void MenuChoice(string err)
        {
            Console.Clear();
            if (err != null)
            {
                Console.WriteLine(err);
            }
            string[] choice = { "Đăng Nhập", "Thoát Chương Trình" };
            int choose = Menu("HỆ THỐNG BÁN VÉ RẠP CHIẾU PHIM", choice);
            switch (choose)
            {
                case 1:
                    MenuLogin();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
        public void MenuLogin()
        {
            UserBL userBL = new UserBL();
            User user = null;
            string un = null;
            string pw = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(line1);
                Console.WriteLine(" ĐĂNG NHẬP");
                Console.WriteLine(line2);
                Console.Write("Tên đăng nhập: ");
                un = Console.ReadLine();
                Console.Write("Mật khẩu: ");
                pw = Password();
                string choice;

                if (validate(un) == false || validate(pw) == false)
                {
                    Console.Write("Tên đăng nhập / mật khẩu không được chứa kí tự đặc biệt, bạn có muốn đăng nhập lại không? (C/K)");
                    choice = Console.ReadLine().ToUpper();

                    while (true)
                    {
                        if (choice != "C" && choice != "K")
                        {
                            Console.Write("Bạn chỉ được nhập (C/K): ");
                            choice = Console.ReadLine().ToUpper();
                            continue;
                        }
                        break;
                    }
                    switch (choice)
                    {
                        case "C":
                            continue;
                        case "c":
                            continue;
                        case "K":
                            MenuChoice(null);
                            break;
                        case "k":
                            MenuChoice(null);
                            break;
                        default:
                            continue;
                    }
                }
                try
                {
                    user = userBL.Login(un, pw);
                }
                catch (NullReferenceException)
                {
                    Console.Write("Mất kết nối, bạn có muốn đăng nhập lại không? (C/K)");
                    choice = Console.ReadLine().ToUpper();

                    while (true)
                    {
                        if (choice != "C" && choice != "K")
                        {
                            Console.Write("Bạn chỉ được nhập (C/K): ");
                            choice = Console.ReadLine().ToUpper();
                            continue;
                        }
                        break;
                    }
                    switch (choice)
                    {

                        case "C":
                            continue;
                        case "c":
                            continue;
                        case "K":
                            MenuChoice(null);
                            break;
                        case "k":
                            MenuChoice(null);
                            break;
                        default:
                            continue;
                    }
                }

                if (user == null)
                {
                    Console.Write("Tên đăng nhập / mật khẩu không đúng, bạn có muốn đăng nhập lại không? (C/K)");
                    choice = Console.ReadLine().ToUpper();
                    while (true)
                    {
                        if (choice != "C" && choice != "K")
                        {
                            Console.Write("Bạn chỉ được nhập (C/K): ");
                            choice = Console.ReadLine().ToUpper();
                            continue;
                        }
                        break;
                    }

                    switch (choice)
                    {
                        case "C":
                            continue;
                        case "c":
                            continue;
                        case "K":
                            MenuChoice(null);
                            break;
                        case "k":
                            MenuChoice(null);
                            break;
                        default:
                            continue;
                    }
                }
                break;
            }

            if (user.Type == "m")
            {
                menuManager(userBL.Login(un, pw));
            }
            else if (user.Type == "s")
            {
                menuStaff(userBL.Login(un, pw));
            }
        }
        public void menuManager(User user)
        {
            Console.Clear();
            string[] managerMenu = { "Tạo lịch chiếu phim", "Đăng xuất" };
            int choose = Menu("Chào mừng bạn đến với CGV Cinemas", managerMenu);
            switch (choose)
            {
                case 1:
                    Console.Clear();
                    ConsoleManager cm = new ConsoleManager();
                    try
                    {
                        cm.CreateShowtime(user);
                    }
                    catch (System.NullReferenceException)
                    {
                        MenuChoice("MẤT KẾT NỐI, MỜI BẠN ĐĂNG NHẬP LẠI !!!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        MenuChoice("MẤT KẾT NỐI, MỜI BẠN ĐĂNG NHẬP LẠI !!!");
                    }
                    break;
                case 2:
                    Console.Clear();
                    MenuChoice(null);
                    break;
                default:
                    menuManager(user);
                    return;
            }
        }
        public void menuStaff(User user)
        {
            Console.Clear();
            string[] staffmenu = { "Đặt vé.", "Đăng xuất." };
            int staff = Menu("Chào Mừng bạn đến với CGV Cinemas", staffmenu);
            switch (staff)
            {
                case 1:
                    Console.Clear();
                    ConsoleStaff cs = new ConsoleStaff();
                    try
                    {
                        cs.Ticket(user);
                    }
                    catch (NullReferenceException)
                    {
                        MenuChoice("MẤT KẾT NỐI, MỜI BẠN ĐĂNG NHẬP LẠI !!!");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException)
                    {
                        MenuChoice("MẤT KẾT NỐI, MỜI BẠN ĐĂNG NHẬP LẠI !!!");
                    }
                    break;
                case 2:
                    Console.Clear();
                    MenuChoice(null);
                    break;
                default:
                    menuStaff(user);
                    return;
            }
        }
        public bool validate(string str)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionstr = regex.Matches(str);
            if (matchCollectionstr.Count < str.Length)
            {
                return false;
            }
            return true;
        }
        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');
                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
        public short Menu(string title, string[] menuItem)
        {
            short choose = 0;
            Console.WriteLine(line1);
            Console.WriteLine(" " + title);
            Console.WriteLine(line2);
            for (int i = 0; i < menuItem.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuItem[i]);
            }
            Console.WriteLine(line2);
            try
            {
                Console.Write("Chọn: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {
            }

            if (choose == 0 || choose > menuItem.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("Bạn chọn không đúng, mời bạn chọn lại: ");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        continue;
                    }
                } while (choose <= 0 || choose > menuItem.Length);
            }
            return choose;
        }
    }
}