using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using connect_dentes_API.Utils;
using Microsoft.EntityFrameworkCore;

namespace connect_dentes_API.Repositories.Implementations
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        public readonly DatabaseContext _dbContext;

        public AgendamentoRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void VerificaAgendamentoCreate(AgendamentoCreateDto dto)
        {
            if (dto == null)
                throw new Exception("Forneça os dados para cadastro de um agendamento!");

            if (dto.ClienteId == null)
                throw new Exception("Selecione um cliente!");

            if (dto.DataAgendada == null)
                throw new Exception("Selecione uma data para o agendamento!");
        }

        private async Task VerificaClienteExistente(AgendamentoCreateDto dto)
        {
            var cliente = await _dbContext
                                    .Cliente
                                    .Where(x => x.Id == dto.ClienteId)
                                    .FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Cliente não encontrado!");
        }

        private async Task VerificaClienteComAgendamento(AgendamentoCreateDto dto)
        {
            var agendamentoStatus = await _dbContext
                                            .Agendamento
                                            .Where(x => x.ClienteId == dto.ClienteId &&
                                                        (x.Status == AgendamentoStatus.Agendado ||
                                                        x.Status == AgendamentoStatus.Atrasado))
                                            .FirstOrDefaultAsync();

            if (agendamentoStatus != null)
                throw new Exception("O cliente já tem um agendamento aberto!");
        }

        private async Task AtualizaStatus()
        {
            var agendamentos = await _dbContext.Agendamento.ToListAsync();

            foreach (var agendamento in agendamentos)
            {
                if ((agendamento.DataAgendada < DateTime.Now) &&
                   (agendamento.Status != AgendamentoStatus.Atendido) &&
                   (agendamento.Status != AgendamentoStatus.AtendidoComAtraso))
                {
                    agendamento.Status = AgendamentoStatus.Atrasado;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task RemoveHorariosIndisponiveis(DateTime dataAgendadaTemp, List<DateTime> horarios)
        {
            var agendamentos = await _dbContext.Agendamento.Where(x => x.DataAgendada.Date == dataAgendadaTemp.Date).ToListAsync();

            foreach (var horario in horarios.ToList())
            {
                foreach (var agendamento in agendamentos)
                {
                    if (horario.Hour == agendamento.DataAgendada.Hour && agendamento.DataAgendada.Minute >= horario.Minute && agendamento.DataAgendada.Minute < horario.AddMinutes(15).Minute)
                        horarios.Remove(horario);
                }
            }
        }

        private static List<DateTime> GetHorarioInicial(DateTime dataAgendadaTemp)
        {
            var horarioInicial = new DateTime(dataAgendadaTemp.Year, dataAgendadaTemp.Month, dataAgendadaTemp.Day, 8, 0, 0);
            if (dataAgendadaTemp.Date == DateTime.Now.Date)
            {
                var hora = DateTime.Now.AddMinutes(15).Hour;
                int minutosParaProximoHorario = 15 - (DateTime.Now.Minute % 15);
                var minutos = DateTime.Now.AddMinutes(minutosParaProximoHorario).Minute;

                horarioInicial = new DateTime(dataAgendadaTemp.Year, dataAgendadaTemp.Month, dataAgendadaTemp.Day, hora, minutos, 0);
            }
            var horarios = new List<DateTime>();

            while (horarioInicial.Hour < 18)
            {
                horarios.Add(horarioInicial);
                horarioInicial = horarioInicial.AddMinutes(15);
            }

            return horarios;
        }

        public async Task<Agendamento> GetById(int id)
        {
            var agendamento = await _dbContext.Agendamento.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (agendamento == null)
                throw new Exception("Agendamento não encontrado!");

            var cliente = await _dbContext.Cliente.Where(x => x.Id == agendamento.ClienteId).FirstOrDefaultAsync();
            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            agendamento.Cliente = cliente;
            return agendamento;
        }

        public async Task<List<Agendamento>> GetAll(AgendamentoFiltroDto filtros)
        {
            await AtualizaStatus();
            var agendamentos = await _dbContext
                                        .Agendamento
                                        .Where(x => ((filtros.ClienteId == null || x.ClienteId == filtros.ClienteId) &&
                                                     (filtros.DataInicio == null || x.DataAgendada >= filtros.DataInicio) &&
                                                     (filtros.DataFim == null || x.DataAgendada <= filtros.DataFim) &&
                                                     (filtros.Status == null || x.Status == filtros.Status)))
                                        .Include("Cliente")
                                        .ToListAsync();

            if (agendamentos == null || agendamentos.Count == 0)
                throw new Exception("Não foram encontrados agendamentos!");

            return agendamentos;
        }

        public async Task<Agendamento> Create(AgendamentoCreateDto agendamentoDto, string userName)
        {
            VerificaAgendamentoCreate(agendamentoDto);
            await VerificaClienteExistente(agendamentoDto);
            await VerificaClienteComAgendamento(agendamentoDto);

            var novoAgendamento = new Agendamento
            {
                ClienteId = (int)agendamentoDto.ClienteId,
                DataAgendada = (DateTime)agendamentoDto.DataAgendada,
                Status = AgendamentoStatus.Agendado,
                DataCadastro = DateTime.Now,
                UsuarioCadastro = userName
            };

            await _dbContext.Agendamento.AddAsync(novoAgendamento);
            await _dbContext.SaveChangesAsync();
            return novoAgendamento;
        }

        public async Task<List<DateTime>> GetHorarios(DateTime? dataAgendada)
        {
            if (dataAgendada == null)
                throw new Exception("Forneça uma data para o agendamento!");

            var dataAgendadaTemp = ((DateTime)dataAgendada);
            List<DateTime> horarios = GetHorarioInicial(dataAgendadaTemp);
            await RemoveHorariosIndisponiveis(dataAgendadaTemp, horarios);

            return horarios;
        }

        public async Task<Agendamento> Update(AgendamentoCreateDto agendamentoDto, int id, string userName)
        {
            var agendamento = await _dbContext.Agendamento.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (agendamento == null)
                throw new Exception("Agendamento não encontrado!");


            agendamento.ClienteId = (int)agendamentoDto.ClienteId;
            agendamento.DataAgendada = (DateTime)agendamentoDto.DataAgendada;
            agendamento.DataEdicao = DateTime.Now;
            agendamento.UsuarioEdicao = userName;

            if (agendamento.DataAgendada < DateTime.Now &&
               agendamento.Status != AgendamentoStatus.Atendido &&
               agendamento.Status != AgendamentoStatus.AtendidoComAtraso)
            {
                agendamento.Status = AgendamentoStatus.Atrasado;
            }

            await _dbContext.SaveChangesAsync();
            return agendamento;
        }

        public async Task<Agendamento> Deletar(int id)
        {
            var agendamento = await _dbContext.Agendamento.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (agendamento == null)
                throw new Exception("Agendamento não encontrado!");

            _dbContext.Agendamento.Remove(agendamento);
            await _dbContext.SaveChangesAsync();
            return agendamento;
        }
    }
}
