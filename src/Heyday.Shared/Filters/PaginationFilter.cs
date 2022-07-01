using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Shared.Filters
{
    public class PaginationFilter : BaseFilter
    {
        protected PaginationFilter()
        {
            PageSize = 20;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }

    }
}
