
using DAL.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly BoardGameLibraryContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(BoardGameLibraryContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }


        public async Task<TEntity> FindByIdAsync(object id)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            return entity;
        }


        public TEntity? FindById(object id)
        {
            return _table.Find(id);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}