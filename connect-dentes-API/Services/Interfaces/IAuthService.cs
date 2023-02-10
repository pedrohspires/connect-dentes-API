using connect_dentes_API.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace connect_dentes_API.Services.Interfaces
{
    public interface IAuthService
    {
        public string GetHashSenhaSHA256(string senha, string? salt);
        public string GetSalt();
        public string GenerateToken(UsuarioDto usuario);
        public bool GetAcesso(string controller, string operacao, string token);
        public JwtSecurityToken LerToken(string token);
        public JwtSecurityToken ValidaToken(string token);
        public DadosTokenDto GetDadosToken(string token);
    }
}
