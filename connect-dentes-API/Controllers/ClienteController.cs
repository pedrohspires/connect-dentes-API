using connect_dentes_API.DTOs;
using connect_dentes_API.Entities;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace connect_dentes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IClienteRepository _clienteRepository;

        public ClienteController(IAuthService authService, IClienteRepository clienteRepository)
        {
            _authService = authService;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("cliente", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar clientes");

                return await _clienteRepository.GetAllAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("cliente", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar clientes!");

                return await _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("select")]
        public async Task<ActionResult<List<ClienteSelectDto>>> GetSelect()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("cliente", "listar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para listar clientes!");

                return await _clienteRepository.GetSelect();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create([FromBody]ClienteCreateDto clienteDto)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("cliente", "cadastrar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para cadastrar clientes!");

                return await _clienteRepository.CreateAsync(clienteDto, dadosToken.Nome);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> Update([FromBody] ClienteCreateDto clienteDto, int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var dadosToken = _authService.GetDadosToken(token);
                var temAcesso = _authService.GetAcesso("cliente", "editar", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para editar clientes!");

                return await _clienteRepository.UpdateAsync(clienteDto, id, dadosToken.Nome);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var temAcesso = _authService.GetAcesso("cliente", "excluir", token);

                if (!temAcesso)
                    throw new Exception("Você não tem autorização para excluir clientes!");

                return await _clienteRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
