using MinimalAPICatalago.Models;

namespace MinimalAPICatalago.Services
{
    public interface ITokenService
    {
        string TokenGenerate(string key, string issuer, string audience, UserModel user);
    }
}
