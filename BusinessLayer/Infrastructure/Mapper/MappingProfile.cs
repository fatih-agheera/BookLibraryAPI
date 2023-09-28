using AutoMapper;
using BusinessLayer.Models.Book;
using BusinessLayer.Models.User;
using DataLayer.Models;

namespace BusinessLayer.Infrastructure.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Books
			CreateMap<CreateBookDto, Book>();
			CreateMap<UpdateBookDto, Book>();

			//Users
			CreateMap<CreateUserDto, User>();
			CreateMap<UpdateUserDto, User>();
			CreateMap<RegistrationUserDto, User>();
			CreateMap<LoginUserDto, User>();
			CreateMap<User, AuthorizedUserDto>();
		}
	}
}
