
namespace WebSwIT.Shared.Models.Pagination
{
    public class PaginationFilterModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationFilterModel()
        {
            PageNumber = 1;
            PageSize = 2;
        }

        public PaginationFilterModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
