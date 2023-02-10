using connect_dentes_API.DTOs;
using connect_dentes_API.Repositories.Implementations;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_dentes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IAuthService _authService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        [HttpGet("Logado")]
        public async Task<ActionResult<DadosTokenDto>> GetUsuarioLogado()
        {
            try
            {
                string? token = Request.Headers["Authorization"];

                if (token == null || token.Length == 0)
                    throw new Exception("Não existe usuário logado!");

                DadosTokenDto dadosToken = _authService.GetDadosToken(token);

                return Ok(dadosToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Medico/select")]
        public async Task<ActionResult<List<UsuarioSelectDto>>> GetMedicosSelect()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("usuario_select_medico", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para realizar esta operação!");

                return await _usuarioRepository.GetAllMedicos();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Cadastro")]
        public async Task<ActionResult<string>> Cadastrar([FromBody]UsuarioCadastroDto usuario)
        {
            try
            {
                var usuarioCadastrado = await _usuarioRepository.CreateAsync(usuario);
                var token = _authService.GenerateToken(usuarioCadastrado);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Entrar")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Entrar([FromBody]UsuarioLoginDto usuario)
        {
            try
            {
                var usuarioLogado = await _usuarioRepository.GetUsuarioAsync(usuario);
                var token = _authService.GenerateToken(usuarioLogado);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Acessos")]
        public async Task<ActionResult<List<string>>> GetAcessos([FromBody]GetAcessosListagemDto dto)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("usuario_acessos", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para realizar esta operação!");

                return _usuarioRepository.GetAcessos(token, dto.Controller);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUsuario(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("usuario", "excluir", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para excluir usuarios!");

                return await _usuarioRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
