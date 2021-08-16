using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// Sort and filter criteria.
    /// </summary>
    public class SortAndFilterCriteria
    {
        //******************************************************************************
        //Variables:

        /// <summary>
        /// Sort items name
        /// </summary>
        public const string SortItemsDataMemberName = "sort";

        /// <summary>
        /// Filter items name.
        /// </summary>
        public const string FilterItemsDataMemberName = "filter";

        /// <summary>
        /// Page number name.
        /// </summary>
        public const string PageNoDataMemberName = "page_no";

        /// <summary>
        /// Page size name.
        /// </summary>
        public const string PageSizeDataMemberName = "page_size";

        /// <summary>
        /// Default page number.
        /// </summary>
        private int _defaultPageNo = 1;

        /// <summary>
        /// Default page size.
        /// </summary>
        private int _defaultPageSize = 10;

        /// <summary>
        /// Sort items.
        /// </summary>
        [DataMember(Name = SortItemsDataMemberName)]
        public List<SortExpression> SortItems { get; set; } = new List<SortExpression>();

        /// <summary>
        /// Filter items.
        /// </summary>
        [DataMember(Name = FilterItemsDataMemberName)]
        public List<FilterCriteria> FilterItems { get; set; } = new List<FilterCriteria>();

        /// <summary>
        /// Page number.
        /// </summary>
        [DataMember(Name = PageNoDataMemberName)]
        public int PageNo { get { return _defaultPageNo; } set { if (value > 0) _defaultPageNo = value; } }

        /// <summary>
        /// page size.
        /// </summary>
        [DataMember(Name = PageSizeDataMemberName)]
        public int PageSize { get { return _defaultPageSize; } set { if (value > 0) _defaultPageSize = value; } }
        //******************************************************************************
    }
}
