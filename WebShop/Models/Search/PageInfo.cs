using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models.Search
{
    public class PageInfo
    {
        public int? PageNumber { get; }
        public int? TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PageInfo(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public PageInfo()
        {
            PageNumber = null;
            TotalPages = null;
        }
    }
}