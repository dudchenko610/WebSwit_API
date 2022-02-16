using System.Collections.Generic;

namespace WebSwIT.Shared.Models.Pagination
{
    public class PagedResponseModel<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}