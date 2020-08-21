namespace MovieManager.Models.Collections
{
    public static class MovieCollection
    {
        private static Node _root;
        
        /// <summary>
        /// Adds a Movie to the BST
        /// </summary>
        /// <param name="movie">The movie you wish to insert</param>
        public static void AddMovie(Movie movie) => _root = Insert(_root, movie);
        /// <summary>
        /// Removes a given Movie via its title
        /// </summary>
        /// <param name="movie">The title of the movie you wish to delete</param>
        public static void RemoveMovie(string movie) => _root = Delete(_root, movie);
        /// <summary>
        /// Gets all movies in alphabetical order
        /// </summary>
        /// <returns>The Movie array of the entire list of movies. This is ordered alphabetically</returns>
        public static Movie[] List() => List(_root, new Movie[0]);

        /// <summary>
        /// Attempts to borrow the movie given the title
        /// </summary>
        /// <param name="movie">The title of the movie</param>
        /// <returns>Returns true if the movie was borrowed. False if impossible due to various circumstances.</returns>
        public static bool TryBorrow(string movie)
        {
            _root = Borrow(_root, movie, out var wasBorrowed);
            return wasBorrowed;
        }

        /// <summary>
        /// Attempts to return the movie given the title
        /// </summary>
        /// <param name="movie">The title of the movie</param>
        /// <returns>Returns true if the movie was returned. False if impossible due to various circumstances.</returns>
        public static bool TryReturn(string movie)
        {
            _root = Return(_root, movie, out var wasReturned);
            return wasReturned;
        }

        /// <summary>
        /// Recursive function for the borrowing process, traverses through nodes and updates the movie in its location
        /// </summary>
        /// <param name="parent">The parent node</param>
        /// <param name="movie">The title of the movie wished to borrow</param>
        /// <param name="borrowed">Whether the movie was borrowed or not</param>
        /// <returns>The parent including any updates made</returns>
        private static Node Borrow(Node parent, string movie, out bool borrowed)
        {
            borrowed = false;
            if (parent == null) return parent;

            //  Determines what to do and where to go based on the titles
            if (parent.Movie.Title == movie)
            {
                // Tries to borrow the movie
                borrowed = parent.Movie.TryBorrow();
                return parent;
            }
            else if (parent.Movie.Compare(movie) < 0)
            {
                parent.Left = Borrow(parent.Left, movie, out borrowed);
            }
            else if (parent.Movie.Compare(movie) >= 0)
            {
                parent.Right = Borrow(parent.Right, movie, out borrowed);
            }

            return parent;
        }

        /// <summary>
        /// Recursive function for the returning process, traverses through nodes and updates the movie in its location
        /// </summary>
        /// <param name="parent">The parent node</param>
        /// <param name="movie">The title of the movie wished to return</param>
        /// <param name="returned">Whether the movie was returned or not</param>
        /// <returns>The parent including any updates made</returns>
        private static Node Return(Node parent, string movie, out bool returned)
        {
            returned = false;
            if (parent == null) return parent;

            // Determines where to go and what do to based on titles
            if (parent.Movie.Title == movie)
            {
                // Tries to return the movie
                returned = parent.Movie.TryReturn();
                return parent;
            }
            else if (parent.Movie.Compare(movie) < 0)
            {
                parent.Left = Return(parent.Left, movie, out returned);
            }
            else if (parent.Movie.Compare(movie) >= 0)
            {
                parent.Right = Return(parent.Right, movie, out returned);
            }

            return parent;
        }

        #region QuickSort

        /// <summary>
        /// Quick Sort Partition Algorithm. Orders items based on a pivot between the lower and upper bounds
        /// </summary>
        /// <param name="A">The movie array</param>
        /// <param name="l">The lower bound</param>
        /// <param name="r">The upper bound</param>
        /// <returns>The int between the pivot and upper bound that is larger than the pivot</returns>
        private static int Partition(Movie[] A, int l, int r)
        {
            // pivot is the first element of the array
            int p = A[l].GetAllTime();
            // initialise i to be the lower bound, j to be the upper bound
            int i = l - 1, j = r + 1;

            while (true)
            {
                // Increase the lower bound until an item is smaller than pivot
                do i++; while (A[i].GetAllTime() > p);
                // Decrease the upper bound util an item is larger than the pivot
                do j--; while (A[j].GetAllTime() < p);

                // If they've passed each other then return the upper bound value
                if (i >= j) return j;
                // swap movies at j and i
                (A[i], A[j]) = (A[j], A[i]);
            }
        }

        /// <summary>
        /// Recursive QuickSort function to organise the segments to be sorted.
        /// </summary>
        /// <param name="A">The movie array</param>
        /// <param name="l">The lower bound</param>
        /// <param name="r">The upper bound</param>
        public static void QuickSort(Movie[] A, int l, int r)
        {
            if (l < r)
            {
                int s = Partition(A, l, r);

                QuickSort(A, l, s);
                QuickSort(A, s + 1, r);
            }
        }
        
        /// <summary>
        /// Gets the QuickSorted BST
        /// </summary>
        /// <returns>The movie array sorted largest to smallest</returns>
        public static Movie[] GetQuickSort()
        {
            var allMovies = List(_root, new Movie[0]);

            QuickSort(allMovies, 0, (allMovies.Length - 1));

            return allMovies;
        }

        #endregion

        /// <summary>
        /// Get the Top10 borrowed movies
        /// </summary>
        /// <returns>The movie array of top 10 movies (Not necessarily 10)</returns>
        public static Movie[] Top10()
        {
            var allMovies = GetQuickSort();

            var length = allMovies.Length < 10 ? allMovies.Length : 10;
            var movies = new Movie[length];

            for (int i = 0; i < length; i++)
            {
                movies[i] = allMovies[i];
            }
            
            return movies;
        }

        /// <summary>
        /// Recursive function to get the Movies in alphabetical order
        /// </summary>
        /// <param name="parent">The parent Node</param>
        /// <param name="collection">The collection that is passed down when listing</param>
        /// <returns>The movies in alphabetical order</returns>
        private static Movie[] List(Node parent, Movie[] collection)
        {
            if (collection == null) collection = new Movie[0];
            if (parent == null) return collection;
            
            collection = List(parent.Left, collection);
            collection = AddToArray(collection, parent.Movie);
            collection = List(parent.Right, collection);

            return collection;
        }

        /// <summary>
        /// Recursive function to insert a Movie
        /// </summary>
        /// <param name="parent">The parent Node</param>
        /// <param name="movie">The movie to be inserted</param>
        /// <returns>The parent Node with any edits required</returns>
        private static Node Insert(Node parent, Movie movie)
        {
            if (parent == null) return new Node(movie);
            // If title already exists, do nothing and return the parent edited.
            if (parent.Movie.Title == movie.Title) return parent;

            // Compares the titles and determines where to go
            if (parent.Movie.Compare(movie.Title) < 0)
            {
                parent.Left = Insert(parent.Left, movie);
            }
            else if (parent.Movie.Compare(movie.Title) >= 0)
            {
                parent.Right = Insert(parent.Right, movie);
            }

            return parent;
        }

        /// <summary>
        /// Recursive function to delete a Movie given its title
        /// </summary>
        /// <param name="parent">The parent Node</param>
        /// <param name="movie">The title of the Movie to delete</param>
        /// <returns>The parent Node with any edits required</returns>
        private static Node Delete(Node parent, string movie)
        {
            if (parent == null) return null;

            // Compares the titles and determines where to go 
            if (parent.Movie.Title == movie)
            {
                // Updates Node with the Left or Right value if present
                if (parent.Left == null) return parent.Right;
                if (parent.Right == null) return parent.Left;

                // Otherwise gets the Min value
                parent = Min(parent);

                parent.Right = Delete(parent.Right, movie);
            }
            else if (parent.Movie.Compare(movie) < 0)
            {
                parent.Left = Delete(parent.Left, movie);
            }
            else if (parent.Movie.Compare(movie) >= 0)
            {
                parent.Right = Delete(parent.Right, movie);
            }

            return parent;
        }

        /// <summary>
        /// Add a Movie to an Array helper function. 
        /// </summary>
        /// <param name="movies">The array of Movies</param>
        /// <param name="movie">The Movie to add</param>
        /// <returns>The new array with all Movies</returns>
        private static Movie[] AddToArray(Movie[] movies, Movie movie)
        {
            var result = new Movie[movies.Length + 1];
            var i = 0;

            // Add all other movies
            for (; i < movies.Length; i++)
            {
                result[i] = movies[i];
            }

            // Add final movie
            result[i] = movie;
            return result;
        }
        
        /// <summary>
        /// Gets the minimum value Movie of the Left Nodes in the parent 
        /// </summary>
        /// <param name="parent">The parent Node</param>
        /// <returns>The Min Value Movie</returns>
        private static Node Min(Node parent)
        {
            var minMovie = parent;
            
            while (parent.Left != null)
            {
                minMovie = parent.Left;
                // push the Left Node up one
                parent = parent.Left;
            }

            return minMovie;
        }
        
    }
}