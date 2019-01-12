using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageApp.Utils
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random element from the given collection
        /// </summary>
        public static T Choose<T>(this Random random, IEnumerable<T> collection)
        {
            return collection.ElementAt(random.Next(collection.Count()));
        }

        /// <summary>
        /// Returns a random element from the given array
        /// </summary>
        public static T Choose<T>(this Random random, T[] array)
        {
            return array[random.Next(array.Length)];
        }
    }
}
