using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.SearchParams.Abstract;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.JiraApi
{
    public static class Extensions
    {
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }

        public static DateTime GetDefaultStartFrom(this IDateFilter dateFilter)
        {
            DateTime now = DateTime.UtcNow;
            return new DateTime(now.Year, now.Month, 1);
        }
    }
}
