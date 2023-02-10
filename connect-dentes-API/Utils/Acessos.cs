namespace connect_dentes_API.Utils
{
    public class TipoAcesso
    {
        public readonly string controller;
        public readonly List<string> tiposAceitos;

        public TipoAcesso(string controller, List<string> tiposAceitos)
        {
            this.controller = controller;
            this.tiposAceitos = tiposAceitos;
        }
    }

    public static class Acessos
    {
        public static List<TipoAcesso> acessos = new List<TipoAcesso>
        {
            new TipoAcesso("usuario_select_medico_listar", new List<string>{Tipos.Admin, Tipos.Gerente, Tipos.Atendente, Tipos.Medico}),
            new TipoAcesso("usuario_acessos_listar", new List<string>{ Tipos.Admin, Tipos.Gerente, Tipos.Atendente, Tipos.Medico}),

            new TipoAcesso("cliente_listar", new List<string>{ Tipos.Admin, Tipos.Atendente, Tipos.Medico}),
            new TipoAcesso("cliente_cadastrar", new List<string>{ Tipos.Admin, Tipos.Atendente}),
            new TipoAcesso("cliente_editar", new List<string>{ Tipos.Admin, Tipos.Atendente}),
            new TipoAcesso("cliente_excluir", new List<string>{ Tipos.Admin, Tipos.Atendente, Tipos.Medico}),

            new TipoAcesso("atendimento_listar", new List<string>{ Tipos.Admin, Tipos.Medico, Tipos.Atendente}),
            new TipoAcesso("atendimento_cadastrar", new List<string>{ Tipos.Admin, Tipos.Medico}),
            new TipoAcesso("atendimento_editar", new List<string>{ Tipos.Admin, Tipos.Medico}),
            new TipoAcesso("atendimento_excluir", new List<string>{ Tipos.Admin, Tipos.Medico}),
        };
    }
}
