using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Models.FilterAndPaging
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IList<T> Items { get; set; } = new List<T>();
        public Metadata Metadata { get; set; }
    }
}
