using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.UnitOfWork
{
    public interface IRepositoriesWrapper : IDisposable
    {
	    IBookRepository Books { get; }

        IUserRepository Users { get; }
	}
}