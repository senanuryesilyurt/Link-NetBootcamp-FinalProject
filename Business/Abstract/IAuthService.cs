using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);

        //Kullanıcı mevcut mu?
        IResult UserExists(UserForRegisterDto userForRegisterDto);
        IDataResult<TokenDto> CreateToken(User user);

    }
}
