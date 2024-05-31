using GerenciadorDeTarefas.API.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GerenciadorDeTarefas.API.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public string GenerateJwtToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));          
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
          
            var claims = new List<Claim>
            {
                 new Claim("userName", email),
                 new Claim(ClaimTypes.Role, role)
            };
           
            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience: audience, 
                expires: DateTime.Now.AddHours(4), 
                signingCredentials: credentials, 
                claims: claims); 
        
            var tokenHandler = new JwtSecurityTokenHandler();          
            var stringToken = tokenHandler.WriteToken(token);
           
            return stringToken;
        }
      
        public string ComputeSha256Hash(string password)
        {          
            using (SHA256 sha256Hash = SHA256.Create())
            {               
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));           
                StringBuilder builder = new StringBuilder();
              
                for (int i = 0; i < bytes.Length; i++)
                {                 
                    builder.Append(bytes[i].ToString("x2"));
                }
               
                return builder.ToString();
            }
        }
    }
}

