using System;

namespace MovieManager.Models.Collections
{
    public static class MemberCollection
    {
        // Capped at 10 Members as noted in the CAB301 Slack Channel
        private static Member[] _members = new Member[10];

        /// <summary>
        /// Adds a Member to the array given the user doesn't already exist
        /// </summary>
        /// <param name="member">The Member to insert</param>
        /// <returns>True if the Member was added, False otherwise</returns>
        public static bool AddMember(Member member)
        {
            // Ensures we can't find the user already
            if (FindMember(m => m.FullName == member.FullName, out _)) return false;

            for (int i = 0; i < _members.Length; i++)
            {
                if (_members[i] == null)
                {
                    _members[i] = member;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the phone number of the given Member full name
        /// </summary>
        /// <param name="fullname">Full name of the Member</param>
        /// <param name="number">The out string phone number</param>
        /// <returns>True if found, False otherwise</returns>
        public static bool GetPhone(string fullname, out string number) {
            number = null;

            if (FindMember(m => m.FullName == fullname, out var member))
            {
                number = member.PhoneNumber;
                return true;
            }
                 
            return false;
        }

        /// <summary>
        /// Gets the list of Movie titles the Member currently has borrowed
        /// </summary>
        /// <param name="fullname">The Members fullname</param>
        /// <returns>String array of Movie titles</returns>
        public static string[] ListBorrowed(string fullname) => FindMember(m => m.FullName == fullname, out var member) ? member.Movies : new string[0];
        
        /// <summary>
        /// Tries to find the Member given a username
        /// </summary>
        /// <param name="username">Members username</param>
        /// <param name="member">The out Member</param>
        /// <returns>True if found, False otherwise</returns>
        public static bool FindByUsername(string username, out Member member) => FindMember(m => m.UserNameMatch(username), out member);

        /// <summary>
        /// Tries to Borrow a Movie given a Members full name and the movie title. 
        /// Tries to find the Member. 
        /// Then tries to borrow the movie in the Member record. 
        /// Finally updating the Member in the array. 
        /// Returns false if it fails anywhere
        /// </summary>
        /// <param name="fullname">Users full name</param>
        /// <param name="movie">The movie title</param>
        /// <returns>True if successful, False otherwise</returns>
        public static bool BorrowMovie(string fullname, string movie) => FindMember(m => m.FullName == fullname, out var member) && member.InsertMovie(movie) && Update(member);

        /// <summary>
        /// Tries to Return a Movie given a Members full name and the movie title. 
        /// Tries to find the user. 
        /// Then tries to return the movie in the Member record. 
        /// Finally updating the Member in the array. 
        /// Returns false if it fails anywhere
        /// </summary>
        /// <param name="fullname">Members full name</param>
        /// <param name="movie">The movie title</param>
        /// <returns>True if successful, False otherwise</returns>
        public static bool ReturnMovie(string fullname, string movie) => FindMember(m => m.FullName == fullname, out var member) && member.RemoveMovie(movie) && Update(member);
        
        /// <summary>
        /// Tries to Login based on a username and password. 
        /// Tries to find the user. 
        /// Then tries to Login. 
        /// Returns false if it fails anywhere
        /// </summary>
        /// <param name="username">The username string</param>
        /// <param name="password">The password string</param>
        /// <returns>True if the Login was successful, False otherwise</returns>
        public static bool TryLogin(string username, string password) => FindMember(m => m.UserNameMatch(username), out var member) && member.TryLogin(password);

        #region Private Helpers

        /// <summary>
        /// Updates the Member with equal full names
        /// </summary>
        /// <param name="member">The member to update</param>
        /// <returns>True if the member was updated. Otherwise False</returns>
        private static bool Update(Member member)
        {
            for (var i = 0; i < _members.Length; i++)
            {
                if (_members[i].FullName == member.FullName)
                {
                    _members[i] = member;
                    return true;
                }
            }

            return false;
        }
           
        /// <summary>
        /// Attempts to find the Member based on a comparison.
        /// </summary>
        /// <param name="comparison">The comparison. A lambda function which compares a given Member and returns a boolean</param>
        /// <param name="result">The Member found if any</param>
        /// <returns>True if the Member is found, False otherwise</returns>
        public static bool FindMember(Func<Member, bool> comparison, out Member result)
        {
            result = null;
            
            foreach (var member in _members)
                if (member != null && comparison(member))
                {
                    result = member;
                    return true;
                }

            return false;
        }

        #endregion
    }
}