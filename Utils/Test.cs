using MovieManager.Models;
using System;
using System.Diagnostics;

namespace MovieManager.Utils
{
    class Test
    {
        /// <summary>
        /// Runs a speed test on the given func that returns a list of T
        /// </summary>
        /// <typeparam name="T">The object returned</typeparam>
        /// <param name="name">The name of the test</param>
        /// <param name="func">The func being tested, must return T[]</param>
        /// <returns>The T[] result</returns>
        public static T[] SpeedTest<T>(string name, Func<T[]> func)
        {
            var watch = new Stopwatch();
            watch.Start();
            var res = func();
            watch.Stop();
            Console.WriteLine($"{name}: {res.Length} movies // {watch.ElapsedTicks} ticks");
            return res;
        }

        /// <summary>
        /// Tests if the array of the values are ordered in descending order
        /// </summary>
        /// <typeparam name="T">The Object being tested</typeparam>
        /// <param name="name">The name of the test</param>
        /// <param name="A">The array being tested on</param>
        /// <param name="value">The func that returns a integer to test the order</param>
        public static void OrderedTest<T>(string name, T[] A, Func<T, int> value)
        {
            int i = 0, j = 1;
            while (j < A.Length)
            {
                // throws an exception if not ordered correctly
                if (value(A[i]) < value(A[j])) throw new Exception($"{name}: ({i}: {value(A[i])}) < ({j}: {value(A[j])})");
                i++; j++;
            }
        }
    }
}
