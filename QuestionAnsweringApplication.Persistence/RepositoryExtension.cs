using System.Linq.Expressions;

namespace QuestionAnsweringApplication.Persistence
{
    public static class RepositoryExtension
    {
        public static Task<T> FirstAsync<T>(this IRepository repository, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            var query = repository.Query(null, includes);
            return repository.FirstAsync(query);
        }

        public static Task<T> FirstAsync<T>(this IRepository repository, Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (includes is null)
            {
                throw new ArgumentNullException(nameof(includes));
            }

            var query = repository.Query(condition, includes);
            return repository.FirstAsync(query);
        }

        public static Task<List<T>> ListAsync<T>(this IRepository repository, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (includes is null)
            {
                throw new ArgumentNullException(nameof(includes));
            }

            var query = repository.Query(null, includes);
            return repository.ListAsync(query);
        }

        public static Task<List<T>> ListAsync<T>(this IRepository repository, Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (includes is null)
            {
                throw new ArgumentNullException(nameof(includes));
            }

            var query = repository.Query(condition, includes);
            return repository.ListAsync(query);
        }
    }
}
