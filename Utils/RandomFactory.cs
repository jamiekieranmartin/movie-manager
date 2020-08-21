using System;

namespace MovieManager.Utils
{
    /// <summary>
    /// Generates random values
    /// </summary>
    public static class RandomFactory
    {
        private static readonly Random random = new Random();
        private static readonly int max = 1 << 10;

        /// <summary>
        /// Returns a random string
        /// </summary>
        /// <returns>random guid string</returns>
        public static string String() => Guid.NewGuid().ToString();

        /// <summary>
        /// Returns a random integer
        /// </summary>
        /// <returns>random interger</returns>
        public static int Int() => random.Next(max);
        
        /// <summary>
        /// Returns a random enum value of the given type
        /// </summary>
        /// <typeparam name="T">The enum you wish to test</typeparam>
        /// <returns>random enum value</returns>
        public static T Enum<T> () => (T) GetValues<T>().GetValue(random.Next(GetValues<T>().Length));

        /// <summary>
        /// Helper function for the random enum
        /// Gets all possible values
        /// </summary>
        /// <typeparam name="T">The enum you wish to get</typeparam>
        /// <returns>All possible enum values</returns>
        private static Array GetValues<T>() => System.Enum.GetValues(typeof(T));

        /// <summary>
        /// Random DateTime
        /// </summary>
        /// <returns>Random DateTime</returns>
        public static DateTime DateTime()=> System.DateTime.Now.AddSeconds(random.Next(max));
    }
}