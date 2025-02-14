using DataLayer.Contexts;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using DataLayer.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataLayer.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            IMongoClient mongoClient,
            IOptions<MongoDbConfiguration> options) : base(mongoClient, options)
        {
        }

        public bool IsUniqueLogin(string login)
        {
            var result = _dbSet.Any(x => x.Login == login);
            return !result;
        }

        public User? GetUserByLogin(string login)
            => _dbSet.FirstOrDefault(x => x.Login == login);
    }
}