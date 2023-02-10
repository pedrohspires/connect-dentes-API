using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> GetAllAsync();
        public Task<ClienteDto> GetById(int id);
        public Task<List<ClienteSelectDto>> GetSelect();
        public Task<ClienteDto> CreateAsync(ClienteCreateDto clienteDto, string usuarioCadastro);
        public Task<ClienteDto> UpdateAsync(ClienteCreateDto clienteDto, int id, string usuarioEdicao);
        public Task<bool> Delete(int id);
    }
}
