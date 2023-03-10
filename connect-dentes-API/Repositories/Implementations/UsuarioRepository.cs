using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using connect_dentes_API.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace connect_dentes_API.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly DatabaseContext _dbContext;
        public readonly IAuthService _authService;

        public UsuarioRepository(DatabaseContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        private async Task<int> QuantidadeEmailDuplicado(string email)
        {
            return await _dbContext.Usuario.Where(x => x.Email == email).CountAsync();
        }

        private void VerificaPoliticaDeEmail(string email)
        {
            if (!Regex.IsMatch(email, "[a-zA-Z][a-zA-Z0-9_]{1,}@[a-zA-Z]+.com$"))
                throw new Exception("Endereço de email inválido!");
        }

        private async Task VerificaEmail(string email)
        {
            if (email == null || email.Length == 0)
                throw new Exception("Preencha o email!");

            if (await QuantidadeEmailDuplicado(email) > 0)
                throw new Exception("Email já cadastrado!");

            VerificaPoliticaDeEmail(email);
        }

        private void VerificaPoliticaDeSenha(string senha)
        {
            if (!Regex.IsMatch(senha, "^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{8,}$"))
                throw new Exception("A senha deve conter 8 ou mais caracteres, letras maiúsculas e minúsculas, números e caracteres especiais!");
        }

        private void VerificaSenha(string senha)
        {
            if (senha == null || senha.Length == 0)
                throw new Exception("Informe a senha!");

            VerificaPoliticaDeSenha(senha);
        }

        private async Task ValidaDadosCadastrais(UsuarioCadastroDto dto)
        {
            await VerificaEmail(dto.Email);
            VerificaSenha(dto.Senha);

            if (dto.Nome == null || dto.Nome.Length == 0)
                throw new Exception("Preencha o nome!");

            if ((dto.Tipo == null || dto.Tipo.Length == 0) && !(await VerificaPrimeiroCadastro()))
                throw new Exception("Forneça um tipo válido para o usuário!");
        }

        private async Task<bool> VerificaPrimeiroCadastro()
        {
            var usuarios = await _dbContext.Usuario.FirstOrDefaultAsync();

            if (usuarios == null)
                return true;

            return false;
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioCadastroDto dto)
        {
            await ValidaDadosCadastrais(dto);

            var salt = _authService.GetSalt();
            var hashSenha = _authService.GetHashSenhaSHA256(dto.Senha, salt);

            Usuario novoUsuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = hashSenha,
                Salt = salt,
                Ativo = dto.Ativo,
                DataCadastro = DateTime.Now,
                Tipo = await VerificaPrimeiroCadastro() ? Tipos.Admin : dto.Tipo.ToLower()
            };

            await _dbContext.Usuario.AddAsync(novoUsuario);
            await _dbContext.SaveChangesAsync();

            return new UsuarioDto
            {
                Id = novoUsuario.Id,
                Nome = novoUsuario.Nome,
                Email = novoUsuario.Email,
                Ativo = novoUsuario.Ativo,
                DataCadastro = novoUsuario.DataCadastro,
                Tipo = novoUsuario.Tipo
            };
        }

        public async Task<UsuarioDto> GetUsuarioAsync(UsuarioLoginDto dto)
        {
            var usuarioLogado = await _dbContext.Usuario.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();

            if (usuarioLogado == null)
                throw new Exception("Email não cadastrado!");

            if (!usuarioLogado.Ativo)
                throw new Exception("Usuário não está ativo!");

            var hashSenha = _authService.GetHashSenhaSHA256(dto.Senha, usuarioLogado.Salt);
            if (!hashSenha.Equals(usuarioLogado.Senha))
                throw new Exception("Senha incorreta!");

            return new UsuarioDto
            {
                Id = usuarioLogado.Id,
                Nome = usuarioLogado.Nome,
                Email = usuarioLogado.Email,
                Ativo = usuarioLogado.Ativo,
                DataCadastro = usuarioLogado.DataCadastro,
                Tipo = usuarioLogado.Tipo
            };
        }

        public async Task<List<UsuarioSelectDto>> GetAllMedicos()
        {
            var medicos = await _dbContext.Usuario.Where(x => x.Tipo == Tipos.Medico).ToListAsync();

            if (medicos == null)
                throw new Exception("Nenhum médico encontrado!");

            List<UsuarioSelectDto> medicosSelect = new List<UsuarioSelectDto>();

            foreach(var medico in medicos)
            {
                medicosSelect.Add(new UsuarioSelectDto
                {
                    Id = medico.Id,
                    Nome = medico.Nome
                });
            }

            return medicosSelect;
        }

        public List<string> GetAcessos(string token, string controller)
        {
            var dadosToken = _authService.GetDadosToken(token);

            var usuarioAcessos = new List<string>();

            foreach (var acesso in Acessos.acessos)
            {
                if (acesso.controller.Contains(controller) && acesso.tiposAceitos.Contains(dadosToken.Tipo))
                    usuarioAcessos.Add(acesso.controller.Split("_").LastOrDefault());
            }

            return usuarioAcessos;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await _dbContext.Usuario.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (usuario == null)
                throw new Exception("Usuario não encontrado!");

            _dbContext.Usuario.Remove(usuario);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
