using Microsoft.IdentityModel.Tokens;
using MinimalAPICatalago.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPICatalago.Services
{
    public class TokenService : ITokenService
    {
        public string TokenGenerate(string key, string issuer, string audience, UserModel user)
        {
            //Declarações do usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            //Codificando chave secreta em um array de bytes
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            
            //Gerando chave simétrica através do algoritmo HmacSha256
            var credentials = new SigningCredentials(securityKey, 
                                                SecurityAlgorithms.HmacSha256);

            //Criando token
            var token = new JwtSecurityToken(issuer: issuer,
                            audience: audience,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
    }
}
