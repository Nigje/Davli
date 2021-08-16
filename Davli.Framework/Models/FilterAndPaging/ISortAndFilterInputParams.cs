using System;
using System.Collections.Generic;
using System.Text;

namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// An interface for input restrictions.
    /// </summary>
    public interface ISortAndFilterInputParams
    {
        /// <summary>
        /// Items that must be sorted.
        /// </summary>
        string SortItems { get; set; }

        /// <summary>
        /// Filter conditions.
        /// </summary>
        string FilterItems { get; set; }

        /// <summary>
        /// Page number.
        /// </summary>
        int PageNo { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; set; }
    }
}
