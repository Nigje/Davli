using Microsoft.AspNetCore.Mvc;

namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// Input restrictions.
    /// </summary>
    public class SortAndFilterInputParams : ISortAndFilterInputParams
    {
        //************************************************************************
        //Variables:

        /// <summary>
        /// Sort items.
        /// </summary>
        [FromQuery(Name = SortAndFilterCriteria.SortItemsDataMemberName)]
        public string SortItems { get; set; }

        /// <summary>
        /// Filter items.
        /// </summary>
        [FromQuery(Name = SortAndFilterCriteria.FilterItemsDataMemberName)]
        public string FilterItems { get; set; }

        /// <summary>
        /// page number.
        /// </summary>
        [FromQuery(Name = SortAndFilterCriteria.PageNoDataMemberName)]
        public int PageNo { get; set; }

        /// <summary>
        /// page size.
        /// </summary>
        [FromQuery(Name = SortAndFilterCriteria.PageSizeDataMemberName)]
        public int PageSize { get; set; }
        //************************************************************************
    }
}
