using connect_dentes_API.DTOs;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioDto> CreateAsync(UsuarioCadastroDto dto);
        public Task<UsuarioDto> GetUsuarioAsync(UsuarioLoginDto dto);
    }
}
