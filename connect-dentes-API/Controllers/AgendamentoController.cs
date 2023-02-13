using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Implementations;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_dentes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoController(IAuthService authService, IAgendamentoRepository agendamentoRepository)
        {
            _authService = authService;
            _agendamentoRepository = agendamentoRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("agendamento", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar agendamentos");

                var agendamento = await _agendamentoRepository.GetById(id);
                return Ok(agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Listar")]
        public async Task<ActionResult<List<Agendamento>>> GetAgendamentos([FromBody]AgendamentoFiltroDto filtros)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("agendamento", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar agendamentos");

                var agendamentos = await _agendamentoRepository.GetAll(filtros);
                return Ok(agendamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> Create([FromBody]AgendamentoCreateDto agendamento)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("agendamento", "cadastrar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para cadastrar agendamentos");

                var novoAgendamento = await _agendamentoRepository.Create(agendamento, dadosToken.Nome);
                return Ok(novoAgendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Agendamento>> Update([FromBody] AgendamentoCreateDto agendamento, int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("agendamento", "editar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para editar agendamentos");

                var agendamentoUpdate = await _agendamentoRepository.Update(agendamento, id, dadosToken.Nome);
                return Ok(agendamentoUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Agendamento>> Delete(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("agendamento", "deletar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para deletar agendamentos");

                var agendamentoDeletado = await _agendamentoRepository.Deletar(id);
                return Ok(agendamentoDeletado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
