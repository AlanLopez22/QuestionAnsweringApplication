using Microsoft.EntityFrameworkCore;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.Persistence
{
    public sealed class ApplicationDbContextInitializer : IApplicationDbContextInitializer
    {
        private readonly IRepository _repository;
        private readonly ApplicationDbContext _applicationDataContext;

        public ApplicationDbContextInitializer(IRepository repository, ApplicationDbContext applicationDataContext)
        {
            _repository = repository;
            _applicationDataContext = applicationDataContext;
        }

        public async Task Run()
        {
            try
            {
                await _applicationDataContext.Database.EnsureCreatedAsync();
                await _applicationDataContext.Database.MigrateAsync();

                await EnsureAddUser("test.user", "test", "Test", "User");
                await EnsureAddUser("test.user2", "test2", "Test", "Test 2");
                await EnsureAddUser("test.user3", "test3", "Test", "Test 3");

                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private async Task EnsureAddUser(string userName, string password, string lastName, string firstName)
        {
            var user = await _repository.FirstAsync<User>(x => x.UserName == userName);

            if (user == null)
            {
                user = new User()
                {
                    UserName = userName,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                };
                _repository.Add(user);
            }
        }
    }
}
