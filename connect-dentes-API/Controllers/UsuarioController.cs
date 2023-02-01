using connect_dentes_API.DTOs;
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
    }
}
