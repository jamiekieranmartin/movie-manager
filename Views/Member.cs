using MovieManager.Models.Collections;
using MovieManager.Utils;
using System;
using System.Text.RegularExpressions;

namespace MovieManager.Views
{
    public static class Member
    {
        public static string full_name;
        
        /// <summary>
        /// Renders the Member Login, this is the entry point to all other displays within this class
        /// </summary>
        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("==========Member Login============");
            var username = Input.ReadString(" Username: ");
            var password = Input.ReadString(" Password: ", new Regex("^[0-9][0-9][0-9][0-9]$"), true);

            if (MemberCollection.FindByUsername(username, out Models.Member member) && MemberCollection.TryLogin(username, password))
            {
                full_name = member.FullName;
                Menu();
            } else
            {
                Console.WriteLine("=================================");
                Input.PromptEnter(" Error: press enter to return to main menu");
                Main.Menu();
            }
        }

        /// <summary>
        /// Renders the main menu
        /// </summary>
        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("==========Member Menu============");
            Console.WriteLine(" 1. Display all movies");
            Console.WriteLine(" 2. Borrow a movie");
            Console.WriteLine(" 3. Return a movie");
            Console.WriteLine(" 4. List current borrowed movie DVDs");
            Console.WriteLine(" 5. Display top 10 most popular movies");
            Console.WriteLine(" 0. Return to main menu");
            Console.WriteLine("=================================");

            var input = Input.ReadInt(" Please make a selection (1-5 or 0 to return to main menu): ", new Regex("1|2|3|4|5|0"));

            switch (input)
            {
                case 1:
                    DisplayAll();
                    break;
                case 2:
                    BorrowMovie();
                    break;
                case 3:
                    ReturnMovie();
                    break;
                case 4:
                    AllBorrowedMovies();
                    break;
                case 5:
                    Top10();
                    break;
                case 0:
                    Main.Menu();
                    break;
            }
        }


        /// <summary>
        /// Renders the display all movies screen
        /// </summary>
        private static void DisplayAll()
        {
            Console.Clear();
            Console.WriteLine("===========All Movies============");

            var movies = MovieCollection.List();

            if (movies.Length == 0)
            {
                Console.WriteLine(" No Movies");
            }

            for (int i = 0; i < movies.Length; i++)
            {
                var movie = movies[i];
                Console.WriteLine($" {i+1}. {movie.Title}");
                Console.WriteLine($"   Starring: {movie.Starring}");
                Console.WriteLine($"   Director: {movie.Director}");
                Console.WriteLine($"   Duration: {movie.Duration} minutes");
                Console.WriteLine($"   Genre: {movie.Genre.ToString()}");
                Console.WriteLine($"   Classification: {movie.Classification.ToString()}");
                Console.WriteLine($"   Release Date: {movie.ReleaseDate.ToString()}");
                Console.WriteLine($"   Copies: {movie.Copies}");
                Console.WriteLine($"   Available Copies: {movie.Copies - movie.GetBorrowed()}");
            }

            End();
        }

        /// <summary>
        /// Renders the borrow movie screen
        /// </summary>
        private static void BorrowMovie()
        {
            Console.Clear();
            Console.WriteLine("==========Borrow Movie===========");

            var movieTitle = Input.ReadString("Movie Title: ", new Regex(".*"));

            if (MovieCollection.TryBorrow(movieTitle) && MemberCollection.BorrowMovie(full_name, movieTitle))
                Console.WriteLine($" Successfully Borrowed");
            else            
                Console.WriteLine(" Error Borrowing Movie");

            End();
        }

        /// <summary>
        /// Renders the return movie screen
        /// </summary>
        private static void ReturnMovie()
        {
            Console.Clear();
            Console.WriteLine("==========Return Movie===========");

            var movieTitle = Input.ReadString("Movie Title: ", new Regex(".*"));

            // OR means it'll always allow Members to return Movie even if the Movie has been deleted.
            if (MemberCollection.ReturnMovie(full_name, movieTitle) || MovieCollection.TryReturn(movieTitle))
                Console.WriteLine($" Successfully Returned");
            else
                Console.WriteLine(" Error Returning Movie");

            End();
        }

        /// <summary>
        /// Renders the users borrowed Movies
        /// </summary>
        private static void AllBorrowedMovies()
        {
            Console.Clear();
            Console.WriteLine("========Your Borrowed Movies=========");

            // Get member and list movie string
            MemberCollection.FindMember(m => m.FullName == full_name, out var member);

            if (member.Movies.Length == 0)
            {
                Console.WriteLine(" No Borrowed Movies");
            }

            for (int i = 0; i < member.Movies.Length; i++)
            {
                Console.WriteLine($" {i+1}. {member.Movies[i]}");
            }

            End();
        }

        /// <summary>
        /// Renders the Top 10 borrowed Movies
        /// </summary>
        private static void Top10()
        {
            Console.Clear();
            Console.WriteLine("=========Top 10 Borrowed Movies=========");

            var top10 = MovieCollection.Top10();

            if (top10.Length == 0)
            {
                Console.WriteLine(" No Movies");
            }

            for (int i = 0; i < top10.Length; i++)
            {
                Console.WriteLine($" {i+1}. {top10[i].Title} borrowed {top10[i].GetAllTime()} times");
            }

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
