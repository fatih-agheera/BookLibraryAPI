using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services.Implementations
{
	public class JwtTokenService : ITokenService
	{
		private readonly IConfiguration _config;

		public JwtTokenService(IConfiguration config)
		{
			_config = config;
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
			};

			var jwtKey = _config["JwtSettings:Key"] ?? throw new ConfigurationKeyNotFoundException("JWT key is null!");

			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwtKey)
			);

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var jwtToken = new JwtSecurityToken(
				issuer: _config["JwtSettings:Issuer"] ?? throw new ConfigurationKeyNotFoundException("JWT Issuer is null!"),
				audience: _config["JwtSettings:Audience"] ?? throw new ConfigurationKeyNotFoundException("JWT Audience is null!"),
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(jwtToken);
		}
	}
}
