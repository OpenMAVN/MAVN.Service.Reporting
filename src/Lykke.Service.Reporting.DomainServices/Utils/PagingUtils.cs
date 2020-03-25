using System;

namespace Lykke.Service.Reporting.DomainServices.Utils
{
    public static class PagingUtils
    {
        public static (int skip, int take) GetNextPageParameters(int currentPage, int pageSize)
        {
            if (currentPage < 1)
                throw new ArgumentException("Current page can't be negative", nameof(currentPage));

            if (pageSize < 1)
                throw new ArgumentException("Page size has to be greater than 0", nameof(pageSize));

            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            return (skip, take);
        }
    }
}
