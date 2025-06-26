using DevMindSpeed.Common.Db.Models;

namespace DevMindSpeed.Common.Db.Abstraction
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetByIdsAsync(List<Guid> ids);
        Task<PaginatedResult<TEntity>> GetPaginatedAsync(IQueryable<TEntity> queryable, PaginationParams paginationParams);
        Task<PaginatedResult<TEntity>> GetPaginatedAsync(PaginationParams paginationParams);
        Task<bool> TryDeleteByIdAsync(object id);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllByIdsAsync(List<Guid> ids);
        Task<bool> Exists(Guid id);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}