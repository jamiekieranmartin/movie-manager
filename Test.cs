using MovieManager.Models;
using MovieManager.Models.Collections;
using System;

namespace MovieManager
{
    public static class Test
    {
        public static void TestAmount(int length)
        {
            for (int i = 0; i < length; i++)
            {
                MovieCollection.AddMovie(Movie.Random());
            }
            Console.WriteLine("Sorting");
            Utils.Test.OrderedTest("QuickSort", Utils.Test.SpeedTest("QuickSort", () => MovieCollection.GetQuickSort()), m => m.GetAllTime());
            Console.WriteLine();
        }
    }
}
