using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Extension
{
    public static class QueryableExtensions
    {
        public static PagedResult<T> ApplyPagination<T>(this IQueryable<T> query,Pagination pagination)
        {
            var totalRecords=query.Count();

            var items= query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize).ToList();

            return new PagedResult<T>
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords/pagination.PageSize),
                Data = items
            };
        }
    }
}
