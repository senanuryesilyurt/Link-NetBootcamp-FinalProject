using Microsoft.IdentityModel.Tokens;


namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //Sisteme gelen bir json web token'nın API tarafındanda doğrulaması  
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
