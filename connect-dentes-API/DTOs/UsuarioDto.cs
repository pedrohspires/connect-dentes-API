﻿using connect_dentes_API.Enums;

namespace connect_dentes_API.DTOs
{
    public class UsuarioDto : BaseDto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public Roles? Role { get; set; }
    }
}
