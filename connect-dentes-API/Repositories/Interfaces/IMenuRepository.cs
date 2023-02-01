using connect_dentes_API.DTOs;

namespace connect_dentes_API.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        public Task<List<MenuItemDto>> GetMenu(string tipoUsuario);
    }
}
