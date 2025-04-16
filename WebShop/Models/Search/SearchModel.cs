using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models.Search
{
    public class SearchModel
    {
        public SearchPath Path { get; set;}
        public SearchResult Result { get; set; }
        public PageInfo Page { get; set; }

        public SearchModel(SearchResult searchResult, PageInfo Page)
        {
            Result = searchResult;
            this.Page = Page;
        }
        public SearchModel()
        {
            Page = new PageInfo();
            Result = new SearchResult();
            Path = new SearchPath();
        }
    }
}