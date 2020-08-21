using MovieManager.Models;
using MovieManager.Models.Collections;
using MovieManager.Utils;
using System;
using System.Text.RegularExpressions;

namespace MovieManager.Views
{
    public static class Staff
    {
        private const string Username = "staff";
        private const string Password = "today123";

        /// <summary>
        /// Renders the login page for the staff
        /// </summary>
        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("==========Staff Login============");
            var username = Input.ReadString(" Username: ");
            var password = Input.ReadString(" Password: ", null, true);

            if (username == Username && password == Password)
            {
                Menu();
            }
            else
            {
                Console.WriteLine("=================================");
                Input.PromptEnter(" Error: press enter to return to menu");
                Main.Menu();
            }
        }

        /// <summary>
        /// Renders the Menu
        /// </summary>
        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("===========Staff Menu============");
            Console.WriteLine(" 1. Add a new movie DVD");
            Console.WriteLine(" 2. Remove a movie DVD");
            Console.WriteLine(" 3. Register a new Member");
            Console.WriteLine(" 4. Find a registered member's phone number");
            Console.WriteLine(" 0. Return to main menu");
            Console.WriteLine("=================================");

            var input = Input.ReadInt(" Please make a selection (1-4 or 0 to return to main menu): ", new Regex("1|2|3|4|0"));

            switch (input)
            {
                case 1:
                    AddMovie();
                    break;
                case 2:
                    RemoveMovie();
                    break;
                case 3:
                    RegisterMember();
                    break;
                case 4:
                    GetPhoneNumber();
                    break;
                case 0:
                    Main.Menu();
                    break;
            }
        }

        /// <summary>
        /// Renders the Add Movice functionality
        /// </summary>
        private static void AddMovie()
        {
            Console.Clear();
            Console.WriteLine("==========Create Movie============");
            var title = Input.ReadString(" Title: ");
            var starring = Input.ReadString(" Starring: ");
            var director = Input.ReadString(" Director: ");
            var duration = Input.ReadInt(" Duration (minutes): ");

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($" {i}: {(Genre)i}");
            }
            var genre = (Genre)Input.ReadInt(" Genre [0-7]: ", new Regex("[0-7]"));

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($" {i}: {(Classification)i}");
            }
            var classification = (Classification)Input.ReadInt(" Classification [0-3]: ", new Regex("[0-3]"));
            var releaseDate = Input.ReadString(" Release Date (DD/MM/YYYY): ", new Regex("[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"));
            var copies = Input.ReadInt(" Copies: ");

            MovieCollection.AddMovie(new Movie(title, starring, director, duration, genre, classification, DateTime.Parse(releaseDate), copies));

            Console.WriteLine(" Successfully Entered Movie");

            End();
        }

        /// <summary>
        /// Displays the remove a movie functionality
        /// </summary>
        private static void RemoveMovie()
        {
            Console.Clear();
            Console.WriteLine("==========Delete Movie============");
            var title = Input.ReadString(" Movie Title: ");

            MovieCollection.RemoveMovie(title);
            Console.WriteLine(" Successfully Deleted");

            End();
        }

        /// <summary>
        /// Displays the register member functionality
        /// </summary>
        private static void RegisterMember()
        {
            Console.Clear();
            Console.WriteLine("==========Create Member===========");
            var fullName = Input.ReadString(" Full Name: ", new Regex(".* .*"));
            var address = Input.ReadString(" Address: ");
            var phoneNumber = Input.ReadString(" Phone Number: ");
            var password = Input.ReadString(" Password: ", new Regex("[0-9][0-9][0-9][0-9]"));

            var member = new Models.Member(fullName, address, phoneNumber, password);

            if (MemberCollection.AddMember(member))
                Console.WriteLine($" Successfully Entered User");
            else
                Console.WriteLine(" Error Entering User");

            End();
        }

        /// <summary>
        /// Displays the Get Number function
        /// </summary>
        private static void GetPhoneNumber()
        {
            Console.Clear();
            Console.WriteLine("========Get Member's Phone=========");
            var fullName = Input.ReadString(" Member's Full Name: ");

            if (MemberCollection.GetPhone(fullName, out var number))
                Console.WriteLine($" Phone Number: {number}");
            else
                Console.WriteLine(" No Member With That Name");

            End();
        }

        /// <summary>
        /// Run at the end of a display
        /// </summary>
        private static void End()
        {
            Console.WriteLine("=================================");
            Input.PromptEnter(" Press enter to return to menu");
            Menu();
        }
    }
}
