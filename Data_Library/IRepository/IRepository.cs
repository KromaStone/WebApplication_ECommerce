using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.IRepository
{
    public interface IRepository<T> where T : class
    {

        void Add(T entity);
        void Remove(T entity);
        void Remove(int id);
        void Update(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T get(int Id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>,IOrderedQueryable<T>> orderby = null,string includeProperties = null);
    }
}
