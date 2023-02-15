using connect_dentes_API.DTOs;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using connect_dentes_API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_dentes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IAtendimentoRepository _atendimentoRepository;

        public AtendimentoController(IAuthService authService, IAtendimentoRepository atendimentoRepository)
        {
            _authService = authService;
            _atendimentoRepository = atendimentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AtendimentoDto>>> GetAtendimentos()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("atendimento", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar atendimentos");

                return await _atendimentoRepository.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AtendimentoDto>> GetById(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("atendimento", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar atendimentos");

                return await _atendimentoRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AtendimentoDto>> Create([FromBody] AtendimentoCreateDto atendimento)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("atendimento", "cadastrar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para cadastrar atendimentos");

                return await _atendimentoRepository.Create(atendimento, dadosToken.Id, dadosToken.Nome);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateById([FromBody] AtendimentoCreateDto atendimento, int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("atendimento", "editar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para editar um atendimento");

                return await _atendimentoRepository.Update(atendimento, id, dadosToken.Id, dadosToken.Nome);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteById(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("atendimento", "deletar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para deletar atendimentos");

                return await _atendimentoRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
