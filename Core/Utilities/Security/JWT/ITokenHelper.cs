using Core.Entities.Concrete;
using Core.Entities.DTOs;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        TokenDto CreateToken(User user, List<Role> roles);
        string CreateRefreshToken(User user);
    }
}
