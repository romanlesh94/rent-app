using HouseApi.Entities.Exceptions;

namespace HouseApi.Models.Pagination
{
    public class PaginationParameters
    {
        public PaginationParameters(int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null)
            {
                PageIndex = pageIndex ?? 1;
                PageSize = pageSize ?? int.MaxValue;
            }
            else
            {
                ValidateParameters(pageIndex.Value, pageSize.Value);

                PageIndex = pageIndex.Value;
                PageSize = pageSize.Value;
            }
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        private static void ValidateParameters(int pageIndex, int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new InternalException("Page size can't be 0 or less than 0");
            }

            if (pageIndex <= 0)
            {
                throw new InternalException("Page index can't be 0 or less than 0");
            }
        }
    }
}
