using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomManagement.Data
{
    public class RepositoryBase<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly TelecomContext _context;

        public RepositoryBase(TelecomContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return GetRecords().ToList();
        }

        public IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
