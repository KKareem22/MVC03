using Microsoft.EntityFrameworkCore;
using RouteProject.DAL.Data;
using RouteProject.DAL.Data.Models;
using RouteProject.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace RouteProject.DAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {

        private readonly GymDbContext dbContext;
        public GenericRepository(GymDbContext c)
        {
            dbContext = c;

        }
        public async Task<int> AddAsync(T entity, CancellationToken ct = default)
        {
            dbContext.Add(entity);
            return await dbContext.SaveChangesAsync(ct);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return dbContext.Set<T>().AsNoTracking().AnyAsync(predicate, ct);
        }

        public async Task<int> DeleteAsync(T entity, CancellationToken ct = default)
        {
            dbContext.Remove(entity);
            return await dbContext.SaveChangesAsync(ct);
        }

        public Task<T?> FirstorDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<int> UpdateAsync(T entity, CancellationToken ct = default)
        {
            dbContext.Update(entity);
            return await dbContext.SaveChangesAsync(ct);
        }

        async Task<IEnumerable<T>> IGenericRepository<T>.GetAllAsync(bool track, CancellationToken ct)
        {
            var query = track ? dbContext.Set<T>() : dbContext.Set<T>().AsNoTracking();
            return await query.ToListAsync(ct);
        }

        async Task<T?> IGenericRepository<T>.GetByIdAsync(int id, CancellationToken ct)
        {
            return await dbContext.Set<T>().FindAsync(id, ct);
        }
    }
}
