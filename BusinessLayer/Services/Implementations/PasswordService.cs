using BusinessLayer.Services.Contracts;
using static BCrypt.Net.BCrypt;

namespace BusinessLayer.Services.Implementations{
	public class PasswordService : IPasswordService
	{
		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
		}

		public bool IsPasswordCorrect(string password, string hashedPassword)
		{
			return Verify(password, hashedPassword);
		}
	}
}