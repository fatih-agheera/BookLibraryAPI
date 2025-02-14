using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using DataLayer.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataLayer.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(
            IMongoClient mongoClient,
            IOptions<MongoDbConfiguration> options) : base(mongoClient, options)
        {
        }

        public virtual Book? GetByIsbn(string isbn)
            => _dbSet.FirstOrDefault(x => x.Isbn == isbn);

        public bool IsUniqueIsbn(string isbn)
        {
            var result = _dbSet.Any(x => x.Isbn == isbn);
            return !result;
        }
    }
}