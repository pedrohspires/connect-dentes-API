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

            if (atendimento.MedicoId == null)
                throw new Exception("Médico não informado!");

            if (atendimento.ClienteId == null)
                throw new Exception("Cliente não informado!");

            if (atendimento.Detalhes == null || atendimento.Detalhes.Length == 0)
                throw new Exception("Informe os detalhes do atendimento!");

            if (atendimento.DataAtendimento == null)
                throw new Exception("Informe a data do atendimento!");

            return true;
        }
        
        private async Task VerificaMedicoExiste(int medicoId)
        {
            var medico = await _dbContext.Usuario.Where(x => x.Id == medicoId && x.Tipo == Tipos.Medico).FirstOrDefaultAsync();

            if (medico == null)
                throw new Exception("Médico não encontrado!");
        }
        
        public async Task<List<AtendimentoDto>> GetAll()
        {
            var atendimentos = await _dbContext.Atendimento.ToListAsync();

            var listaAtendimentos = new List<AtendimentoDto>();
            foreach(var atendimento in atendimentos)
            {
                var usuario = await _dbContext.Usuario.Where(x => x.Id == atendimento.MedicoId && x.Tipo == Tipos.Medico).FirstOrDefaultAsync();
                UsuarioDto medicoAtendimento = new UsuarioDto
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    Tipo = usuario.Tipo
                };

                listaAtendimentos.Add(new AtendimentoDto
                {
                    Id = atendimento.Id,
                    MedicoId = atendimento.MedicoId,
                    Medico = medicoAtendimento,
                    ClienteId = atendimento.ClienteId,
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

        public async Task<AtendimentoDto> Create(AtendimentoCreateDto atendimento, string userName)
        {
            VerificaAtendimento(atendimento);
            await VerificaMedicoExiste((int)atendimento.MedicoId);

            var novoAtendimento = new Atendimento
            {
                MedicoId = (int)atendimento.MedicoId,
                ClienteId = (int)atendimento.ClienteId,
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
                Detalhes = novoAtendimento.Detalhes,
                Observacoes = novoAtendimento.Observacoes,
                Dentes = novoAtendimento.Dentes,
                DataAtendimento = novoAtendimento.DataAtendimento,
                DataRetorno = novoAtendimento.DataRetorno,
                DataCadastro = novoAtendimento.DataCadastro,
                UsuarioCadastro = userName
            };
        }

        public async Task<bool> Update(AtendimentoCreateDto atendimento, int atendimentoId, string userName)
        {
            var atendimentoAtual = await _dbContext.Atendimento.Where(x => x.Id == atendimentoId).FirstOrDefaultAsync();

            if (atendimentoAtual == null)
                throw new Exception("Atendimento não encontrado");

            VerificaAtendimento(atendimento);
            await VerificaMedicoExiste((int)atendimento.MedicoId);

            atendimentoAtual.MedicoId = (int)atendimento.MedicoId;
            atendimentoAtual.ClienteId = (int)atendimento.ClienteId;
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
