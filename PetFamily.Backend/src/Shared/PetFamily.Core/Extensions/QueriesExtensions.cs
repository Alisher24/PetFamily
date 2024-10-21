using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Models;

namespace PetFamily.Core.Extensions;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int page, 
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        
        var items = await source.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>
        {
            Items = items,
            PageSize = pageSize,
            Page = page,
            TotalCount = totalCount
        };
    }
    
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
        
}