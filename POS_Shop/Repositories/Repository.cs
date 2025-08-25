using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly POSDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(POSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T GetById(int id) => _dbSet.Find(id);

        public void Insert(T entity) => _dbSet.Add(entity);

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Save() => _context.SaveChanges();
    }
}
