using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IBaseRepository<DbModel> where DbModel : BaseModel
	{
		IEnumerable<DbModel> GetAll();

		DbModel? Get(string id);

		DbModel Save(DbModel model);

		DbModel Update(DbModel model);

		DbModel Remove(DbModel model);
	}
}
