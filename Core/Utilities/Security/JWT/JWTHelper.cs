using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.Utilities.Security.JWT
{
    //Token üretmeye yardımcı sınıf
    public class JWTHelper : ITokenHelper
    {
        //IConfiguration appsettings.json dosyası içerisindeki propertyleri okumamızı sağlar.
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        private DateTime _refreshTokenExpiration;
        
        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //appsettings içerisindeki token option ile token options classını map et.
            _tokenOptions = Configuration.GetSection("TokenOption").Get<TokenOptions>();
        }
        public TokenDto CreateToken(User user, List<Role> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            _refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken(user),
                RefreshTokenExpiration = _refreshTokenExpiration,
            };
        }

        //JWT Token propertylerini verdik.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
          SigningCredentials signingCredentials, List<Role> roles)
        {

            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user,roles), 
                signingCredentials: signingCredentials
            ); ;
            return jwt;
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken(User user)
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user, List<Role> roles)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName}" + $"{user.LastName}");
            claims.AddRoles(roles.Select(u => u.Name).ToArray());



            return claims;
        }

    }
}
