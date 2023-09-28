using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Contracts
{
	public interface IUserService
	{
		AuthorizedUserDto Register(RegistrationUserDto item);

		AuthorizedUserDto Login(LoginUserDto item);
	}
}
