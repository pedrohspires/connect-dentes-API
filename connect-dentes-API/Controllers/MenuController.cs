using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_dentes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAuthService _authService;

        public MenuController(IMenuRepository menuRepository, IAuthService authService)
        {
            _menuRepository = menuRepository;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuAsync()
        {
            try
            {
                var dadosToken = _authService.GetDadosToken(Request.Headers["Authorization"]);
                var itensMenu = await _menuRepository.GetMenu(dadosToken.Tipo);

                return Ok(itensMenu);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
