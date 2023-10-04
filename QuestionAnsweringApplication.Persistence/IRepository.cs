using System.Linq.Expressions;

namespace QuestionAnsweringApplication.Persistence
{
    public interface IRepository
    {
        IQueryable<T> Query<T>(Expression<Func<T, bool>>? condition, params Expression<Func<T, object>>[] includes) where T : class;

        T Get<T, TKey>(TKey id) where T : class where TKey : IEquatable<TKey>;
        Task<T> FirstAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        IQueryable<T> Include<T, TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> navigationPropertyPath) where T : class;

        Task<List<T>> ListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}