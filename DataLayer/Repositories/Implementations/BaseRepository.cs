using DataLayer.Contexts;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using DataLayer.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer.Repositories.Implementations
{
    public abstract class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected LibraryDbContext _context;
        protected DbSet<DbModel> _dbSet;
        protected readonly IMongoClient _mongoClient;

        protected BaseRepository(
            IMongoClient mongoClient,
            IOptions<MongoDbConfiguration> options)
        {
            _mongoClient = mongoClient;
            _context = new LibraryDbContext(new DbContextOptionsBuilder<LibraryDbContext>()
                .UseMongoDB(_mongoClient, options.Value.DatabaseName)
                .Options);

            _dbSet = _context.Set<DbModel>();
        }

        public virtual DbModel? Get(string id)
            => _dbSet.FirstOrDefault(x => x.Id == id);

        public virtual IEnumerable<DbModel> GetAll()
            => _dbSet.ToList();

        public virtual DbModel Save(DbModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
            {
                model.Id = ObjectId.GenerateNewId().ToString();
            }

            _dbSet.Add(model);
            _context.SaveChanges();
            return model;
        }

        public virtual DbModel Update(DbModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
            return model;
        }

        public virtual DbModel Remove(DbModel model)
        {
            var deletedModel = _dbSet.Remove(model).Entity;
            _context.SaveChanges();
            return deletedModel;
        }
    }
}