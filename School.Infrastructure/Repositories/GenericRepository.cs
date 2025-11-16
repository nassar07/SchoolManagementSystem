using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : BaseEntity
    {
        public async Task<Guid> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return 0;

            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return entity!;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var updatedEntity = context.Set<TEntity>().Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
