using System;
using System.Text;
using System.Security;
using BL;
using System.Text.RegularExpressions;
using System.Text.Encodings;
using Persistence;

namespace ConsolePL
{

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Menus m = new Menus();
            m.MenuChoice(null);
        }
    }
}