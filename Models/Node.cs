namespace MovieManager.Models
{
    /// <summary>
    /// BST Node using the Movie as it's value (determined by title)
    /// </summary>
    public class Node
    {
        public readonly Movie Movie;
        public Node Left, Right;
        
        public Node(Movie movie)
        {
            Movie = movie;
        }
    }
}