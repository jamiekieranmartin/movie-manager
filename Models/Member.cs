namespace MovieManager.Models
{
    public class Member
    {
        public string FullName { get; }
        public string Address { get; }
        public string PhoneNumber { get; }
        private string Password { get; }
        public string[] Movies = new string[10];
        
        public Member(string fullName, string address, string phoneNumber, string password)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        /// <summary>
        /// Determines if the given username matches the current Member
        /// </summary>
        /// <param name="username">The username to test</param>
        /// <returns>True if matches, False otherwise</returns>
        public bool UserNameMatch(string username)
        {
            // Splits the full name into the username format ie "Jamie Martin" -> "MartinJamie"
            var names = FullName.Split(' ');
            return username == names[1] + names[0];
        }

        /// <summary>
        /// Tries to login given the password
        /// </summary>
        /// <param name="password">The password to test</param>
        /// <returns>True if password matches, False otherwise </returns>
        public bool TryLogin(string password) => password == Password;

        /// <summary>
        /// Tests if the given Movie title is already borrowed by the Member
        /// </summary>
        /// <param name="movieTitle">The title to test</param>
        /// <returns>True if the Movie is borrowed, False otherwise</returns>
        private bool IsBorrowed(string movieTitle)
        {
            foreach (var m in Movies)
                if (m == movieTitle)
                    return true;

            return false;
        }

        /// <summary>
        /// Tries to Insert the Movie title into the Members borrowed list
        /// </summary>
        /// <param name="movieTitle">The movie to add</param>
        /// <returns>True if inserted, False otherwise</returns>
        public bool InsertMovie(string movieTitle)
        {
            if (IsBorrowed(movieTitle)) return false;
            
            for (int i = 0;  i < Movies.Length; i++)
            {
                if (Movies[i] == null)
                {
                    Movies[i] = movieTitle;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to Remove the Movie title from the Members borrowed list
        /// </summary>
        /// <param name="movieTitle">The movie to remove</param>
        /// <returns>True if removed, False otherwise</returns>
        public bool RemoveMovie(string movieTitle)
        {
            if (!IsBorrowed(movieTitle)) return false;

            for (int i = 0; i < Movies.Length; i++)
            {
                if (Movies[i] == movieTitle)
                {
                    Movies[i] = null;
                    return true;
                }
            }

            return false;
        }
    }
}