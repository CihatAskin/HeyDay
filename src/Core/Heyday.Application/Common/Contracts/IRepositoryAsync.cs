using Heyday.Domain.Contracts;
using Heyday.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Application.Common.Contracts
{
    public interface IRepositoryAsync : ITransientService
    {
        Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken = default)
        where T : BaseEntity;

        Task<TDto> GetByIdAsync<T, TDto>(Guid id, CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TDto : IDto;

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> expression, bool noTracking = false, CancellationToken cancellationToken = default)
        where T : BaseEntity;

        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        where T : BaseEntity;

        Task UpdateAsync<T>(T entity)
        where T : BaseEntity;


        #region Create

        Task<Guid> CreateAsync<T>(T entity)
        where T : BaseEntity;

        Task<IList<Guid>> CreateRangeAsync<T>(IEnumerable<T> entity)
        where T : BaseEntity;

        #endregion Create

        Task RemoveAsync<T>(T entity) where T : BaseEntity;

        Task<T> RemoveByIdAsync<T>(Guid entityId) where T : BaseEntity;

        Task ClearAsync<T>(Expression<Func<T, bool>>? expression = null) where T : BaseEntity;

        #region Save Changes

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Save Changes

    }
}
