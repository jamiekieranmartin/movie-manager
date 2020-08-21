using System;
using MovieManager.Utils;

namespace MovieManager.Models
{
    public class Movie
    {
        public string Title { get; }
        public string Starring { get; }
        public string Director { get; }
        public int Duration { get;  }
        public Genre Genre { get; }
        public Classification Classification { get; }
        public DateTime ReleaseDate { get; }
        public int Copies { get; }
        private int Borrowed { get; set; }
        private int AllTime { get; set; }
        
        public Movie(string title, string starring, string director, int duration, Genre genre, Classification classification, DateTime releaseDate, int copies, int borrowed = 0, int allTime = 0)
        {
            Title = title;
            Starring = starring;
            Director = director;
            Duration = duration;
            Genre = genre;
            Classification = classification;
            ReleaseDate = releaseDate;
            Copies = copies;
            Borrowed = borrowed;
            AllTime = allTime;
        }

        /// <summary>
        /// Tries to borrow the Movie
        /// </summary>
        /// <returns>True if borrowed. False if no copies available</returns>
        public bool TryBorrow()
        {
            if (Borrowed >= Copies) return false;
            
            // Increased counts of borrows
            Borrowed++;
            AllTime++;
            return true;
        }

        /// <summary>
        /// Tries to return the Movie
        /// </summary>
        /// <returns>True if returned. False if unavailable to return</returns>
        public bool TryReturn()
        {
            if (Copies < Borrowed || Borrowed == 0) return false;

            Borrowed--;
            return true;
        }

        /// <summary>
        /// Gets the borrowed count
        /// </summary>
        /// <returns>Amount borrowed</returns>
        public int GetBorrowed() => Borrowed;

        /// <summary>
        /// Gets the all time borrowed count
        /// </summary>
        /// <returns>Amount borrowed all time</returns>
        public int GetAllTime() => AllTime;

        /// <summary>
        /// Compares the given title against the current Movie using string.Compare()
        /// </summary>
        /// <param name="title">the title of the movie you wish to test</param>
        /// <returns>Returns an integer than indicates the relative position in the sort order</returns>
        public int Compare(string title) => string.Compare(this.Title, title);

        /// <summary>
        /// Makes a random Movie
        /// </summary>
        /// <returns>A Movie with randomly generated variables</returns>
        public static Movie Random() => new Movie(
                RandomFactory.String(),
                RandomFactory.String(),
                RandomFactory.String(),
                RandomFactory.Int(),
                RandomFactory.Enum<Genre>(),
                RandomFactory.Enum<Classification>(),
                RandomFactory.DateTime(),
                RandomFactory.Int(), 
                RandomFactory.Int(), 
                RandomFactory.Int());
    }
}