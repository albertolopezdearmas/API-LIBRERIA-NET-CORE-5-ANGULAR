using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LibraryContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(LibraryContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _entities.ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public async Task<bool> Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            var row = _context.SaveChanges();
            return row > 0;
        }
        public async Task<bool> AddAll(List<T> entity)
        {
            await _entities.AddRangeAsync(entity);
            var row = await _context.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> DeleteAll()
        {
            List<T> entitys = GetAll().ToList();
            if (entitys.Count == 0)
                return true;
            _entities.RemoveRange(entitys);
            var row = _context.SaveChanges();
            return row > 0;
        }
    }
}
