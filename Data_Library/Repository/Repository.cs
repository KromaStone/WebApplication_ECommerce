using Data_Library.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplication_ECommerce.Data;

namespace Data_Library.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T get(int Id)
        {
            return DbSet.Find(Id);   
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> var_query = DbSet;
            if(Filter != null)
                var_query = var_query.Where(Filter);
            if(includeProperties != null)
                foreach(var item in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    var_query = var_query.Include(item);
                }
            if(orderby != null)
                return orderby(var_query).ToList();
            return var_query.ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = DbSet.Find(id);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            DbSet.Update(entity);
        }
    }
}
