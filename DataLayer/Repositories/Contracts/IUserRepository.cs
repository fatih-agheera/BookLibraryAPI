using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IUserRepository : IBaseRepository<User>
	{
		bool IsUniqueLogin(string login);
		User? GetUserByLogin(string login);
	}
}
