using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.Implementations
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(LibraryDbContext context) : base(context) { }

		public bool IsUniqueLogin(string login)
		{
			var result = _dbSet.Any(x => x.Login == login);
			return !result;
		}

		public User? GetUserByLogin(string login)
			=> _dbSet.FirstOrDefault(x => x.Login == login);
	}
}