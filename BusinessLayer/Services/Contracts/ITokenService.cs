using DataLayer.Models;

namespace BusinessLayer.Services.Contracts
{
    public interface ITokenService
    {   
        string CreateToken(User user);
    }
}
