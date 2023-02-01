using connect_dentes_API.DTOs;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Utils;

namespace connect_dentes_API.Repositories.Implementations
{
    public class MenuRepository : IMenuRepository
    {
        public async Task<List<MenuItemDto>> GetMenu(string tipoUsuario)
        {
            if(tipoUsuario == Tipos.Medico)
            {
                return new List<MenuItemDto>
                {
                    new MenuItemDto
                    {
                        Nome = "Atendimento",
                        Link = "/Atendimento",
                        ReactIcon = "BsCheckLg"
                    }
                };
            }

            throw new Exception("Usuário desconhecido!");
        }
    }
}
