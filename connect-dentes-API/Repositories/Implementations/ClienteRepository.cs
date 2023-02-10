using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace connect_dentes_API.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        public readonly DatabaseContext _dbContext;

        public ClienteRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = await _dbContext.Cliente.ToListAsync();

            if (clientes == null || clientes.Count == 0)
                throw new Exception("Não há clientes cadastrados!");

            return clientes;
        }

        public async Task<ClienteDto> GetById(int id)
        {
            var cliente = await _dbContext.Cliente.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Usuário não encontrado");

            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Telefone = cliente.Telefone,
                IsWhatsapp = cliente.IsWhatsapp,
                Email = cliente.Email,
                UF = cliente.UF,
                Cidade = cliente.Cidade,
                Bairro = cliente.Bairro,
                Rua = cliente.Rua,
                Numero = cliente.Numero,
                Complemento = cliente.Complemento,
                UltimoAtendimento = cliente.UltimoAtendimento,
                DataCadastro = cliente.DataCadastro,
                UsuarioCadastro = cliente.UsuarioCadastro,
                DataEdicao = cliente.DataEdicao,
                UsuarioEdicao = cliente.UsuarioEdicao
            };
        }

        public async Task<List<ClienteSelectDto>> GetSelect()
        {
            var clientes = await GetAllAsync();
            var clientesSelect = new List<ClienteSelectDto>();

            foreach (var cliente in clientes)
            {
                clientesSelect.Add(new ClienteSelectDto
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                });
            }

            return clientesSelect;
        }

        private void ValidaDadosCliente(ClienteCreateDto clienteDto)
        {
            if (clienteDto.Nome == null || clienteDto.Nome.Length == 0)
                throw new Exception("Preencha o campo nome!");

            if (clienteDto.Telefone== null || clienteDto.Telefone.Length == 0)
                throw new Exception("Preencha o campo telefone!");

            if (clienteDto.UF == null || clienteDto.UF.Length == 0)
                throw new Exception("Preencha o campo UF!");

            if (clienteDto.Cidade == null || clienteDto.Cidade.Length == 0)
                throw new Exception("Informe a cidade do cliente!");
        }

        public async Task<ClienteDto> CreateAsync(ClienteCreateDto clienteDto, string usuarioCadastro)
        {
            ValidaDadosCliente(clienteDto);

            var novoCliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Cpf = clienteDto.Cpf,
                Telefone = clienteDto.Telefone,
                IsWhatsapp = clienteDto.IsWhatsapp != null ? (bool)clienteDto.IsWhatsapp : false,
                Email = clienteDto.Email,
                UF = clienteDto.UF,
                Cidade = clienteDto.Cidade,
                Bairro = clienteDto.Bairro,
                Rua = clienteDto.Rua,
                Numero = clienteDto.Numero,
                Complemento = clienteDto.Complemento,
                UltimoAtendimento = clienteDto.UltimoAtendimento,
                DataCadastro = DateTime.Now,
                UsuarioCadastro = usuarioCadastro
            };

            await _dbContext.Cliente.AddAsync(novoCliente);
            await _dbContext.SaveChangesAsync();

            return new ClienteDto
            {
                Id = novoCliente.Id,
                Nome = novoCliente.Nome,
                Cpf = novoCliente.Cpf,
                Telefone = novoCliente.Telefone,
                IsWhatsapp = novoCliente.IsWhatsapp,
                Email = novoCliente.Email,
                UF = novoCliente.UF,
                Cidade = novoCliente.Cidade,
                Bairro = novoCliente.Bairro,
                Rua = novoCliente.Rua,
                Numero = novoCliente.Numero,
                Complemento = novoCliente.Complemento,
                UltimoAtendimento = novoCliente.UltimoAtendimento,
                DataCadastro = novoCliente.DataCadastro,
                UsuarioCadastro = usuarioCadastro
            };
        }

        public async Task<ClienteDto> UpdateAsync(ClienteCreateDto clienteDto, int id, string usuarioEdicao)
        {
            ValidaDadosCliente(clienteDto);
            var cliente = await _dbContext.Cliente.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Usuário não encontrado!");


            cliente.Nome = clienteDto.Nome;
            cliente.Cpf = clienteDto.Cpf;
            cliente.Telefone = clienteDto.Telefone;
            cliente.IsWhatsapp = clienteDto.IsWhatsapp != null ? (bool)clienteDto.IsWhatsapp : false;
            cliente.Email = clienteDto.Email;
            cliente.UF = clienteDto.UF;
            cliente.Cidade = clienteDto.Cidade;
            cliente.Bairro = clienteDto.Bairro;
            cliente.Rua = clienteDto.Rua;
            cliente.Numero = clienteDto.Numero;
            cliente.Complemento = clienteDto.Complemento;
            cliente.UltimoAtendimento = clienteDto.UltimoAtendimento;
            cliente.DataEdicao = DateTime.Now;
            cliente.UsuarioEdicao = usuarioEdicao;

            await _dbContext.SaveChangesAsync();

            return new ClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Telefone = cliente.Telefone,
                IsWhatsapp = cliente.IsWhatsapp,
                Email = cliente.Email,
                UF = cliente.UF,
                Cidade = cliente.Cidade,
                Bairro = cliente.Bairro,
                Rua = cliente.Rua,
                Numero = cliente.Numero,
                Complemento = cliente.Complemento,
                UltimoAtendimento = cliente.UltimoAtendimento,
                DataCadastro = cliente.DataCadastro,
                UsuarioCadastro = cliente.UsuarioCadastro,
                DataEdicao = cliente.DataEdicao,
                UsuarioEdicao = cliente.UsuarioEdicao
            };
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _dbContext.Cliente.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            _dbContext.Cliente.Remove(cliente);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
