using System;
using System.Collections.Generic;
using System.Text;

namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// An interface for reports.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedResult<T>
    {
        /// <summary>
        /// Starts from 1
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Defaults will be 10
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// Total count of the items.
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// List of items.
        /// </summary>
        IList<T> Items { get; set; }
    }
}
