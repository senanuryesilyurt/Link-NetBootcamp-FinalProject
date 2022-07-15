using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;


namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {   
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var newUser = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                City =userForRegisterDto.City,
                PhoneNumber = userForRegisterDto.PhoneNumber
            };
            _userService.Add(newUser);
            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }

        public IDataResult<TokenDto> CreateToken(User user)
        {
            var roles = _userService.GetRoles(user);
            
            var token = _tokenHelper.CreateToken(user,roles);

            return new SuccessDataResult<TokenDto>(token);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if( userToCheck is null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.ErrorUserOrPassword);
            }
            return new SuccessDataResult<User>(userToCheck);
        }

        public IResult UserExists(UserForRegisterDto userForRegisterDto)
        {
            var hasMail = _userService.GetByMail(userForRegisterDto.Email);
            if(hasMail is not null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
