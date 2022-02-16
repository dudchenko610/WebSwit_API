using System.Collections.Generic;

namespace WebSwIT.Shared.Models.ListPagination
{
    public class PagedListResponseModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public bool HasMore { get; set; }
    }
}
