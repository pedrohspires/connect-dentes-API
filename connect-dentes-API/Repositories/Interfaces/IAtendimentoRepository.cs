using connect_dentes_API.DTOs;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IAtendimentoRepository
    {
        public Task<List<AtendimentoDto>> GetAll();
        public Task<AtendimentoDto> GetById(int atendimentoId);
        public Task<AtendimentoDto> Create(AtendimentoCreateDto atendimento, int medicoId, string userName);
        public Task<bool> Update(AtendimentoCreateDto atendimento, int atendimentoId, int medicoId, string userName);
        public Task<bool> Delete(int atendimentoId);
    }
}
