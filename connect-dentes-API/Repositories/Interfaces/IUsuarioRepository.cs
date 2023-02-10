using connect_dentes_API.DTOs;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioDto> CreateAsync(UsuarioCadastroDto dto);
        public Task<UsuarioDto> GetUsuarioAsync(UsuarioLoginDto dto);
        public Task<List<UsuarioSelectDto>> GetAllMedicos();
        public List<string> GetAcessos(string token, string controller);
        public Task<bool> Delete(int id);
    }
}
