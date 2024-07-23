using MedAppJwtAuthenticationManager.Data;
using MedAppJwtAuthenticationManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedAppJwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "ThisMustBeAVeryStrongKeyReallyBigAround100CharactersOrSo";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccountList;
        private static IConfigurationRoot config;
        private static IAccountRepository accountRepository;

        public JwtTokenHandler()
        {
            Initialize();

            //accountRepository = new AccountRepository();
            //_userAccountList = new List<UserAccount>
            //{
            //    new UserAccount{ Id=1, UserName="admin", Password="admin", Role="Administrator" },
            //    new UserAccount{ Id=2, UserName="user01", Password="user01", Role="User" }
            //};
            _userAccountList = accountRepository.GetAllAccounts();

        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

            //Initialize Database
            accountRepository = new AccountRepository(config.GetConnectionString("MedAppUserDB"));

        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        { 
            if( string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password)) 
                return null;

            var userAccount = _userAccountList.Where(u => u.UserName == authenticationRequest.UserName && u.Password == authenticationRequest.Password).FirstOrDefault();
            if( userAccount==null) 
                return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = authenticationRequest.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }

    }
}
