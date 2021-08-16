using Microsoft.Extensions.Primitives;
using Davli.Framework.Models.FilterAndPaging;
using System.Collections.Generic;
using System.Linq;

namespace Davli.Framework.Extentions
{
    /// <summary>
    /// Extension class to extract restrictions from the query string.
    /// </summary>
    public static class QueryCollectionExtensions
    {
        //***********************************************************************************
        //Variables:

        //***********************************************************************************
        public static SortAndFilterCriteria ToSortAndFilterCriteria(this IEnumerable<KeyValuePair<string, StringValues>> queryCollection)
        {
            SortAndFilterCriteria sortAndFilterCriteria = new SortAndFilterCriteria();

            //Extract page number.
            if (queryCollection.TryGetByKey("page_no", out var pageNoValues))
            {
                sortAndFilterCriteria.PageNo = int.Parse(pageNoValues);
            }

            //Extract page size.
            if (queryCollection.TryGetByKey("page_size", out var pageSizeValues))
            {
                sortAndFilterCriteria.PageSize = int.Parse(pageSizeValues);
            }

            //Extract items that should be sorted.
            if (queryCollection.TryGetByKey("sort", out var sortValues))
            {
                var sorts = sortValues.ToString().Split(new[] { ',' });
                foreach (var sortItem in sorts)
                    sortAndFilterCriteria.SortItems.Add(new SortExpression(sortItem));
            }

            //Extract items that should be Filtered.
            if (queryCollection.TryGetByKey("filter", out var filterValues))
            {
                var filters = filterValues.ToString().Split(new char[] { '&' });
                foreach (var filterItem in filters)
                    sortAndFilterCriteria.FilterItems.Add(new FilterCriteria(filterItem));
            }
            return sortAndFilterCriteria;
        }
        //***********************************************************************************
        /// <summary>
        /// Parse query collection for key values.
        /// </summary>
        /// <param name="queryCollection"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static bool TryGetByKey(this IEnumerable<KeyValuePair<string, StringValues>> queryCollection,
            string key, out StringValues values)
        {
            KeyValuePair<string, StringValues> result = queryCollection.FirstOrDefault(x => x.Key == key);

            if (result.Equals(default(KeyValuePair<string, StringValues>)))
            {
                values = default;
                return false;
            }

            values = result.Value;
            return true;
        }
        //***********************************************************************************
    }
}

