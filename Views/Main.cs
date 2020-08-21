using MovieManager.Utils;
using System;
using System.Text.RegularExpressions;

namespace MovieManager.Views
{
    public static class Main
    {
        /// <summary>
        /// Renders the main menu
        /// </summary>
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Community Library");
            Console.WriteLine("============Main Menu============");
            Console.WriteLine(" 1. Staff Login");
            Console.WriteLine(" 2. Member Login");
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("=================================");
            Console.WriteLine();
            var input = Input.ReadInt(" Please make a selection (1-2 or 0 to return to exit): ", new Regex("1|2|0"));

            switch (input)
            {
                case 1:
                    Staff.Login();
                    break;
                case 2:
                    Member.Login();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
