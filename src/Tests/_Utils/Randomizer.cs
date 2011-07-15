using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcentCqrs.Tests._Utils
{
    public class Randomizer
    {
        private static readonly Random _random = new Random();

        #region Array

        public static T[] Array<T>(Func<T> create)
        {
            return Array(create, _random.Next(2, 15));
        }

        public static T[] Array<T>(Func<T> create, int length)
        {
            T[] result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = create();
            }

            return result;
        }

        #endregion

        #region String

        private const int A = 'A';
        private const int z = 'z';

        /// <summary>
        /// Generates a random non empty string of a random length.
        /// </summary>
        public static string String()
        {
            return String(_random.Next(15) + 1);
        }

        /// <summary>
        /// Generates a random string of a specified length.
        /// </summary>
        /// <param name="length">Length of the result string.</param>
        public static string String(int length)
        {
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                var charToAppend = (char)_random.Next(A, z + 1);
                if (char.IsLetter(charToAppend) == false)
                {
                    i--;
                }
                else
                {
                    result.Append(charToAppend);
                }
            }

            return result.ToString();
        }

        public static string StringNotIn(params string[] excluded)
        {
            return StringNotIn(excluded.ToList());
        }

        public static string StringNotIn(IEnumerable<string> excluded)
        {
            string result;

            do
            {
                result = String();
            } while (excluded.Contains(result));

            return result;
        }

        #endregion

        #region Number

        public static int Number()
        {
            return _random.Next();
        }

        public static int Number(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static int Number(int max)
        {
            return _random.Next(max);
        }

        public static int NumberNotIn(params int[] excluded)
        {
            return NumberNotIn(excluded.ToList());
        }

        public static int NumberNotIn(IEnumerable<int> excluded)
        {
            int result;

            do
            {
                result = Number();
            } while (excluded.Contains(result));

            return result;
        }

        #endregion

        #region TimeSpan

        public static TimeSpan TimeSpan()
        {
            return new TimeSpan(Number());
        }

        #endregion

        #region Guid

        public static Guid Guid()
        {
            return System.Guid.NewGuid();
        }

        #endregion

        #region DateTime

        public static DateTime DateTime()
        {
            return new DateTime(
                Number(2000, 2010), Number(1, 13), Number(1, 25), Number(0, 24), Number(0, 60), Number(0, 60)
                );
        }

        public static DateTime FutureDate()
        {
            return System.DateTime.UtcNow.AddDays(Number(1, 1000));
        }

        public static DateTime PastDate()
        {
            return System.DateTime.UtcNow.AddDays(-1 * Number(1, 1000));
        }

        #endregion

        #region Email & Phone no

        public static string Email()
        {
            return String(4) + "@" + String(5) + "." + String(3);
        }

        public static string PhoneNumber()
        {
            return Number(100000000, 999999999).ToString();
        }

        #endregion

        #region OneOf

        public static T OneOf<T>(params T[] args)
        {
            return args[Number(args.Length)];
        }

        #endregion
    }
}