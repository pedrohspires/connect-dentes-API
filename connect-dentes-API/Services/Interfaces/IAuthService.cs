using connect_dentes_API.DTOs;

namespace connect_dentes_API.Services.Interfaces
{
    public interface IAuthService
    {
        public string GetHashSenhaSHA256(string senha, string? salt);
        public string GetSalt();
        public string GetToken(UsuarioDto usuario);
    }
}
