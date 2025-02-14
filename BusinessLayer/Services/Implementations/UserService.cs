using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace BusinessLayer.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IPasswordService _passwordService;

		public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, IPasswordService passwordService)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_tokenService = tokenService;
			_passwordService = passwordService;
		}

		public AuthorizedUserDto Register(RegistrationUserDto item)
		{
			var isUniqueLogin = _userRepository.IsUniqueLogin(item.Login);

			if (!isUniqueLogin)
			{
				throw new ItemAlreadyExistsException($"Login: '{item.Login}' is already used!");
			}

			var user = _mapper.Map<User>(item);
			user.Password = _passwordService.HashPassword(user.Password);
			_userRepository.Save(user);

			var authorizedUser = _mapper.Map<AuthorizedUserDto>(user);
			authorizedUser.JwtToken = _tokenService.CreateToken(user);

			return authorizedUser;
		}

		public AuthorizedUserDto Login(LoginUserDto loginUserDto)
		{
			var user = _userRepository.GetUserByLogin(loginUserDto.Login);

			if (user is null)
			{
				throw new UserLoginIsNotFoundException($"Wrong login or password!");
			}

			var result = _passwordService.IsPasswordCorrect(loginUserDto.Password, user.Password);

			if (!result)
			{
				throw new WrongUserPasswordException($"Wrong login or password!");
			}

			var authorizedUser = _mapper.Map<AuthorizedUserDto>(user);
			authorizedUser.JwtToken = _tokenService.CreateToken(user);

			return authorizedUser;
		}
	}
}
