using connect_dentes_API.DTOs;
using connect_dentes_API.Repositories.Interfaces;
using connect_dentes_API.Utils;

namespace connect_dentes_API.Repositories.Implementations
{
    public class MenuRepository : IMenuRepository
    {
        public readonly List<MenuItemDto> itens = new List<MenuItemDto> {
            new MenuItemDto
            {
                Nome = "Atendimento",
                Link = "/Atendimento",
                ReactIcon = "BsCheckLg",
                TiposAceitos = new List<string>{ Tipos.Gerente, Tipos.Medico, Tipos.Atendente }
            },
            new MenuItemDto
            {
                Nome = "Cliente",
                Link = "/Cliente",
                ReactIcon = "BsFillPersonFill",
                TiposAceitos = new List<string>{ Tipos.Gerente, Tipos.Medico, Tipos.Atendente }
            },
            new MenuItemDto
            {
                Nome = "Agendamento",
                Link = "/Agendamento",
                ReactIcon = "TfiAgenda",
                TiposAceitos = new List<string>{ Tipos.Gerente, Tipos.Medico, Tipos.Atendente }
            }
        };

        public async Task<List<MenuItemDto>> GetMenu(string tipoUsuario)
        {
            if(tipoUsuario == Tipos.Admin)
                return itens;


            var menuItens = new List<MenuItemDto>();
            foreach(var item in itens)
            {
                if (item.TiposAceitos.Contains(tipoUsuario))
                    menuItens.Add(item);
            }

            return menuItens;
        }
    }
}
