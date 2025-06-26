using DevMindSpeed.Common.Core.Abstractions;
using DevMindSpeed.Common.Db.Abstraction;
using DevMindSpeed.Common.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace DevMindSpeed.Common.Db.Abstraction
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity<Guid>
    {
        protected readonly DbBaseContext dbContext;
        protected readonly RequestUserEntity requestUserEntity;

        public virtual IUnitOfWork UnitOfWork => dbContext;

        public DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

        protected BaseRepository(DbBaseContext dbContext, RequestUserEntity requestUserEntity)
        {
            this.dbContext = dbContext;
            this.requestUserEntity = requestUserEntity;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetByIdsAsync(List<Guid> ids)
        {
            return await DbSet
                .Where(entity => ids.Contains(entity.Id))
                .ToListAsync();
        }

        public virtual async Task<PaginatedResult<TEntity>> GetPaginatedAsync(IQueryable<TEntity> queryable, PaginationParams paginationParams)
        {
            if (!string.IsNullOrEmpty(paginationParams.SortBy))
            {
                PropertyInfo propertyInfo = typeof(TEntity)
                    .GetProperty(paginationParams.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var conversion = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<TEntity, object>>(conversion, parameter);

                    queryable = string.Equals(paginationParams.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                        ? queryable.OrderByDescending(lambda)
                        : queryable.OrderBy(lambda);
                }
            }

            var totalCount = await queryable.CountAsync();
            var items = await queryable
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<TEntity>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public virtual async Task<PaginatedResult<TEntity>> GetPaginatedAsync(PaginationParams paginationParams)
        {
            var queryable = DbSet.AsQueryable();

            if (!string.IsNullOrEmpty(paginationParams.SortBy))
            {
                PropertyInfo propertyInfo = typeof(TEntity)
                    .GetProperty(paginationParams.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var conversion = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<TEntity, object>>(conversion, parameter);

                    queryable = string.Equals(paginationParams.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                        ? queryable.OrderByDescending(lambda)
                        : queryable.OrderBy(lambda);
                }
            }

            var totalCount = await queryable.CountAsync();
            var items = await queryable
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<TEntity>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public virtual async Task<bool> TryDeleteByIdAsync(object id)
        {
            var entity = await DbSet
                .FindAsync(id);

            if (entity is null)
            {
                return false;
            }

            DbSet.Remove(entity);

            return true;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity updatedEntity = DbSet.Update(entity).Entity;

            await Task.CompletedTask;

            return updatedEntity;
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            DbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return (await DbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public virtual async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllByIdsAsync(List<Guid> ids)
        {
            return await DbSet.Where(entity => ids.Contains(entity.Id)).ToListAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await DbSet
                .Where(entity => entity.Id == id)
                .AnyAsync();
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}