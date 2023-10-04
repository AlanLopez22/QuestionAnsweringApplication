using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuestionAnsweringApplication.Persistence
{
    public class Repository : IRepository
    {
        private ApplicationDbContext Context { get; }

        public Repository(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private DbSet<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        public T Update<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            return result.Entity;
        }

        public T Add<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = Set<T>().Add(entity);

            return result.Entity;
        }

        public T Get<T, TKey>(TKey id) where T : class where TKey : IEquatable<TKey>
        {
            return Set<T>().Find(id)!;
        }

        public Task<T> FirstAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return query.FirstOrDefaultAsync(cancellationToken)!;
        }

        public IQueryable<T> Include<T, TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> navigationPropertyPath) where T : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (navigationPropertyPath == null)
            {
                throw new ArgumentNullException(nameof(navigationPropertyPath));
            }

            return query.Include(navigationPropertyPath);
        }

        public Task<List<T>> ListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return query.ToListAsync(cancellationToken);
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>>? condition, params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = (IQueryable<T>)Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return WithIncludes(query, includes);
        }

        private static IQueryable<T> WithIncludes<T>(IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
