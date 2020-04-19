namespace ExpensesTrackerAPI.Cache
{
    using System;
    using System.Threading.Tasks;

    public interface IMemoryCacheService<T>
    {
        Task<T> GetOrCreate(object key, Func<Task<T>> createItem);
    }
}
