using System.Text;
using System.Linq.Expressions;

using Mapster;
using Microsoft.EntityFrameworkCore;

using Heyday.Application.Common.Contracts;
using Heyday.Domain.Contracts;
using Heyday.Infrastructure.Contexts;
using Heyday.Shared;
using Heyday.Application.Common.Exceptions;
using Heyday.Application.Wrapper;
using Heyday.Shared.Filters;
using Heyday.Application.Catalog.Contracts;
using Heyday.Infrastructure.Extensions.SubExtensions;

namespace Heyday.Infrastructure.Common;


public class RepositoryAsync : IRepositoryAsync
{
    private readonly heydayContext _dbContext;

    public RepositoryAsync(heydayContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get All

    public Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default)
    where T : BaseEntity
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (noTracking) query = query.AsNoTracking();
        if (expression != null) query = query.Where(expression);
        return query.ToListAsync(cancellationToken);
    }

    #endregion Get All

    public Task<T?> GetByIdAsync<T>(Guid entityId, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        IQueryable<T> query = _dbContext.Set<T>();
        return query.Where(e => e.id == entityId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TDto> GetByIdAsync<T, TDto>(Guid entityId, CancellationToken cancellationToken = default)
    where T : BaseEntity
    where TDto : IDto
    {

        IQueryable<T> query = _dbContext.Set<T>();
        var entity = await query.Where(a => a.id == entityId).FirstOrDefaultAsync(cancellationToken);
        if (entity is not null)
        {
            var dto = entity.Adapt<TDto>();
            return dto;
        }

        throw new EntityNotFoundException(string.Format("{0} Not Found:{1}", typeof(T).Name, entityId));
    }

    public Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (expression != null) return query.AnyAsync(expression, cancellationToken);
        return query.AnyAsync(cancellationToken);
    }

    public Task UpdateAsync<T>(T entity) where T : BaseEntity
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) throw new NothingToUpdateException();

        var existing = _dbContext.Set<T>().Find(entity.id);

        if (existing is null) throw new EntityNotFoundException(string.Format("{0} Not Found:{1}", typeof(T).Name, entity.id));

        _dbContext.Entry(existing).CurrentValues.SetValues(entity);

        return Task.CompletedTask;
    }

    #region Create

    public async Task<Guid> CreateAsync<T>(T entity) where T : BaseEntity
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity.id;
    }

    public async Task<IList<Guid>> CreateRangeAsync<T>(IEnumerable<T> entity) where T : BaseEntity
    {
        await _dbContext.Set<T>().AddRangeAsync(entity);
        return entity.Select(x => x.id).ToList();
    }

    #endregion Create

    #region DeleteOrRemoveOrClear

    public Task RemoveAsync<T>(T entity) where T : BaseEntity
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T> RemoveByIdAsync<T>(Guid entityId) where T : BaseEntity
    {
        var entity = await _dbContext.Set<T>().FindAsync(entityId);
        if (entity == null) throw new EntityNotFoundException(string.Format("{0} Not Found:{1}", typeof(T).Name, entityId));
        _dbContext.Set<T>().Remove(entity);

        return entity;
    }

    public Task ClearAsync<T>(Expression<Func<T, bool>>? expression = null) where T : BaseEntity
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (expression != null)
            query = query.Where(expression);

        return query.ForEachAsync(x =>
        {
            _dbContext.Entry(x).State = EntityState.Deleted;
        });
    }

    #endregion DeleteOrRemoveOrClear

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<PaginatedResult<TDto>> GetSearchResultsAsync<T, TDto>(int pageNumber, int pageSize = int.MaxValue, string[]? orderBy = null, Search? advancedSearch = null, string? keyword = null, Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TDto : IDto
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<TDto>> GetSearchResultsAsync<T, TDto>(int pageNumber, int pageSize = int.MaxValue, string[]? orderBy = null, Filters<T>? filters = null, Search? advancedSearch = null, string? keyword = null, CancellationToken cancellationToken = default)
      where T : BaseEntity
      where TDto : IDto
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (filters is not null)
            query = query.ApplyFilter(filters);
        if (advancedSearch?.Fields.Count > 0 && !string.IsNullOrEmpty(advancedSearch.Keyword))
            query = query.AdvancedSearch(advancedSearch);
        else if (!string.IsNullOrEmpty(keyword))
            query = query.SearchByKeyword(keyword);
        query = query.ApplySort(orderBy);
        return query.ToMappedPaginatedResultAsync<T, TDto>(pageNumber, pageSize);
    }
}
