using System;
using System.Text.RegularExpressions;

namespace MovieManager.Utils
{
    /// <summary>
    /// Helper function for retrieving input from the Console
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Prompts an enter
        /// </summary>
        /// <param name="print">The string you want printed to prompt the user</param>
        public static void PromptEnter(string print = null)
        {
            while (true)
            {
                if (print != null) Console.WriteLine(print);
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    break;
            }
        }
        
        /// <summary>
        /// Attempts to read an integer from the console
        /// </summary>
        /// <param name="print">What you want to print to prompt the user</param>
        /// <param name="options">The regex that you want the input to match (null if none)</param>
        /// <param name="hidden">Whether the value should be hidden in the console</param>
        /// <returns>The validated integer</returns>
        public static int ReadInt(string print = null, Regex options = null, bool hidden = false)
        {
            while (true)
            {
                if (print != null) Console.Write(print);
                var stringValue = Get(hidden);

                // test the regex if any and attempt to parse the int, return value if parseable
                if ((options == null || options.IsMatch(stringValue)) && int.TryParse(stringValue, out var value)) 
                    return value;
                // otherwise continue and re-prompt input
                Console.WriteLine("Input not in correct format, please try again");
            }
        }

        /// <summary>
        /// Attempts to read a string from the console
        /// </summary>
        /// <param name="print">What you want to print to prompt the user</param>
        /// <param name="options">The regex that you want the input to match (null if none)</param>
        /// <param name="hidden">Whether the value should be hidden in the console</param>
        /// <returns>The validated string</returns>
        public static string ReadString(string print = null, Regex options = null, bool hidden = false)
        {
            while (true)
            {
                if (print != null) Console.Write(print);
                var value = Get(hidden);
                
                // test the regex if any and return if a match
                if (options == null || options.IsMatch(value)) 
                    return value;
                // otherwise continue and re-prompt input
                Console.WriteLine("Input not in correct format, please try again");
            }
        }

        /// <summary>
        /// Gets the value written by the user from the Console
        /// </summary>
        /// <param name="hidden">Whether the input should be masked or not</param>
        /// <returns>The returned string</returns>
        private static string Get(bool hidden = false)
        {
            var value = "";
            
            // forever loop until user presses enter
            do
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    // if enter then user is done, return value
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return value;
                    // if backspace and the value isn't empty, remove a character
                    case ConsoleKey.Backspace:
                        if (value != "")
                        {
                            value = value.Substring(0, value.Length - 1);
                            Console.Write("\b \b");   
                        }
                        break;
                    // otherwise input the character given
                    default:
                        value += key.KeyChar;
                        if (hidden)
                            Console.Write("*");
                        else 
                            Console.Write(key.KeyChar);
                        break;
                }
            } while (true);
        }
    }
}