﻿using Microsoft.EntityFrameworkCore;

namespace Class_09
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex < 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        public static async Task<PaginatedList<T>>CreateAsync(IQueryable<T> source, int pageIndex, int pSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pSize).Take(pSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pSize);
        }
    }
}
