using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IAgendamentoRepository
    {
        public Task<Agendamento> GetById(int id);
        public Task<List<AgendamentoSelectDto>> GetSelect();
        public Task<List<Agendamento>> GetAll(AgendamentoFiltroDto filtros);
        public Task<Agendamento> Create(AgendamentoCreateDto agendamentoDto, string userName);
        public Task<List<DateTime>> GetHorarios(DateTime? dataAgendada);
        public Task<Agendamento> Update(AgendamentoCreateDto agendamentoDto, int id, string userName);
        public Task<Agendamento> Deletar(int id);
    }
}
