namespace ExpensesTrackerAPI.DAL
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IDataRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> Get(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetById(object id, params Expression<Func<T, object>>[] includeProperties);

        Task<bool> EntityExists(object id);

        Task<T> SaveAsync(T entity);
    }
}