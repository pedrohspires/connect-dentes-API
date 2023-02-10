namespace connect_dentes_API.DTOs
{
    public class ClienteCreateDto
    {
        public string? Nome { get; set; }

        public string? Telefone { get; set; }

        public string? Cpf { get; set; }

        public bool? IsWhatsapp { get; set; }

        public string? Email { get; set; }

        public string? UF { get; set; }

        public string? Cidade { get; set; }

        public string? Bairro { get; set; }

        public string? Rua { get; set; }

        public int? Numero { get; set; }

        public string? Complemento { get; set; }

        public DateTime? UltimoAtendimento { get; set; }
    }
}
