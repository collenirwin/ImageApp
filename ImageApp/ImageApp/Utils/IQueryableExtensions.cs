using System;
using System.Linq;
using System.Linq.Expressions;

namespace ImageApp.Utils
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Calls OrderBy or OrderByDescending based on the value of the descending parameter given
        /// </summary>
        public static IOrderedQueryable<T> OrderBy<T, TKey>(this IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, bool descending)
        {
            return descending
                ? queryable.OrderByDescending(keySelector)
                : queryable.OrderBy(keySelector);
        }
    }
}
