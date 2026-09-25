using Microsoft.EntityFrameworkCore;
using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;

namespace Showlio.api.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entity;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
