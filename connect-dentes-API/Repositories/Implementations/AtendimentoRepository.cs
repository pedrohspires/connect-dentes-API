using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using connect_dentes_API.Utils;
using Microsoft.EntityFrameworkCore;

namespace connect_dentes_API.Repositories.Implementations
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        public readonly DatabaseContext _dbContext;

        public AtendimentoRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool VerificaAtendimento(AtendimentoCreateDto atendimento)
        {
            if (atendimento == null)
                throw new Exception("Forneça os dados do atendimento!");

            if (atendimento.ClienteId == null && atendimento.AgendamentoId == null)
                throw new Exception("Informe um agendamento ou um cliente!");

            if (atendimento.Detalhes == null || atendimento.Detalhes.Length == 0)
                throw new Exception("Informe os detalhes do atendimento!");

            if (atendimento.DataAtendimento == null)
                throw new Exception("Informe a data do atendimento!");

            return true;
        }

        public async Task<List<AtendimentoDto>> GetAll()
        {
            var atendimentos = await _dbContext.Atendimento.ToListAsync();

            var listaAtendimentos = new List<AtendimentoDto>();
            foreach (var atendimento in atendimentos)
            {
                var usuario = await _dbContext.Usuario.Where(x => x.Id == atendimento.MedicoId && (x.Tipo == Tipos.Medico || x.Tipo == Tipos.Admin)).FirstOrDefaultAsync();
                UsuarioDto medicoAtendimento = new UsuarioDto
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    Tipo = usuario.Tipo
                };

                var cliente = await _dbContext.Cliente.Where(x => x.Id == atendimento.ClienteId).FirstOrDefaultAsync();

                listaAtendimentos.Add(new AtendimentoDto
                {
                    Id = atendimento.Id,
                    MedicoId = atendimento.MedicoId,
                    Medico = medicoAtendimento,
                    ClienteId = atendimento.ClienteId,
                    Cliente = cliente,
                    AgendamentoId = atendimento.AgendamentoId,
                    Detalhes = atendimento.Detalhes,
                    Observacoes = atendimento.Observacoes,
                    Dentes = atendimento.Dentes,
                    DataAtendimento = atendimento.DataAtendimento,
                    DataRetorno = atendimento.DataRetorno,
                    DataCadastro = atendimento.DataCadastro,
                    UsuarioCadastro = atendimento.UsuarioCadastro,
                    DataEdicao = atendimento.DataEdicao,
                    UsuarioEdicao = atendimento.UsuarioEdicao
                });
            }

            return listaAtendimentos;
        }

        public async Task<AtendimentoDto> GetById(int atendimentoId)
        {
            var atendimento = await _dbContext.Atendimento.Where(x => x.Id == atendimentoId).FirstOrDefaultAsync();

            if (atendimento == null)
                throw new Exception("Atendimento não encontrado!");

            return new AtendimentoDto
            {
                Id = atendimento.Id,
                MedicoId = atendimento.MedicoId,
                ClienteId = atendimento.ClienteId,
                AgendamentoId = atendimento.AgendamentoId,
                Detalhes = atendimento.Detalhes,
                Observacoes = atendimento.Observacoes,
                Dentes = atendimento.Dentes,
                DataAtendimento = atendimento.DataAtendimento,
                DataRetorno = atendimento.DataRetorno,
                DataCadastro = atendimento.DataCadastro,
                UsuarioCadastro = atendimento.UsuarioCadastro,
                DataEdicao = atendimento.DataEdicao,
                UsuarioEdicao = atendimento.UsuarioEdicao
            };
        }

        public async Task<AtendimentoDto> Create(AtendimentoCreateDto atendimento, int medicoId, string userName)
        {
            VerificaAtendimento(atendimento);
            Agendamento? agendamento = await GetAgendamento(atendimento);

            var novoAtendimento = new Atendimento
            {
                MedicoId = medicoId,
                ClienteId = atendimento.ClienteId != null ? (int)atendimento.ClienteId : agendamento.ClienteId,
                AgendamentoId = atendimento.AgendamentoId,
                Detalhes = atendimento.Detalhes,
                Observacoes = atendimento.Observacoes,
                Dentes = atendimento.Dentes,
                DataAtendimento = (DateTime)atendimento.DataAtendimento,
                DataRetorno = atendimento.DataRetorno,
                DataCadastro = DateTime.Now,
                UsuarioCadastro = userName
            };

            await _dbContext.Atendimento.AddAsync(novoAtendimento);
            await _dbContext.SaveChangesAsync();

            return new AtendimentoDto
            {
                Id = novoAtendimento.Id,
                MedicoId = novoAtendimento.MedicoId,
                ClienteId = novoAtendimento.ClienteId,
                AgendamentoId = novoAtendimento.AgendamentoId,
                Agendamento = agendamento,
                Detalhes = novoAtendimento.Detalhes,
                Observacoes = novoAtendimento.Observacoes,
                Dentes = novoAtendimento.Dentes,
                DataAtendimento = novoAtendimento.DataAtendimento,
                DataRetorno = novoAtendimento.DataRetorno,
                DataCadastro = novoAtendimento.DataCadastro,
                UsuarioCadastro = userName
            };
        }

        private async Task<Agendamento> GetAgendamento(AtendimentoCreateDto atendimento)
        {
            Agendamento? agendamento = null;
            if (atendimento.AgendamentoId != null)
            {
                agendamento = await _dbContext.Agendamento.Where(x => x.Id == atendimento.AgendamentoId).FirstOrDefaultAsync();
                if (agendamento == null)
                    throw new Exception("Agendamento não encontrado!");

                if (atendimento.ClienteId != null && atendimento.ClienteId != agendamento.ClienteId)
                    throw new Exception("O cliente informado é diferente do cliente agendado!");

                await AlteraStatusAgendamento(agendamento);
            }

            return agendamento;
        }

        private async Task AlteraStatusAgendamento(Agendamento? agendamento)
        {
            if (agendamento.Status != AgendamentoStatus.Atendido && agendamento.Status != AgendamentoStatus.AtendidoComAtraso)
            {
                if (agendamento.DataAgendada < DateTime.Now)
                    agendamento.Status = AgendamentoStatus.AtendidoComAtraso;
                else
                    agendamento.Status = AgendamentoStatus.Atendido;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Update(AtendimentoCreateDto atendimento, int atendimentoId, int medicoId, string userName)
        {
            var atendimentoAtual = await _dbContext.Atendimento.Where(x => x.Id == atendimentoId).FirstOrDefaultAsync();
            Agendamento? agendamento = await GetAgendamento(atendimento);

            if (atendimentoAtual == null)
                throw new Exception("Atendimento não encontrado");

            VerificaAtendimento(atendimento);

            atendimentoAtual.MedicoId = medicoId;
            atendimentoAtual.ClienteId = atendimento.ClienteId != null ? (int)atendimento.ClienteId : agendamento.ClienteId;
            atendimentoAtual.AgendamentoId = atendimento.AgendamentoId;
            atendimentoAtual.Detalhes = atendimento.Detalhes;
            atendimentoAtual.Observacoes = atendimento.Observacoes;
            atendimentoAtual.Dentes = atendimento.Dentes;
            atendimentoAtual.DataAtendimento = (DateTime)atendimento.DataAtendimento;
            atendimentoAtual.DataRetorno = atendimento.DataRetorno;
            atendimentoAtual.DataEdicao = DateTime.Now;
            atendimentoAtual.UsuarioEdicao = userName;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int atendimentoId)
        {
            var atendimento = await _dbContext.Atendimento.Where(x => x.Id == atendimentoId).FirstOrDefaultAsync();

            if (atendimento == null)
                throw new Exception("Atendimento não encontrado!");

            _dbContext.Atendimento.Remove(atendimento);
            _dbContext.SaveChanges();

            return true;
        }

    }
}
