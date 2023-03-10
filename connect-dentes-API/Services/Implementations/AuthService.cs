using connect_dentes_API.DTOs;
using connect_dentes_API.Services.Interfaces;
using connect_dentes_API.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace connect_dentes_API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ByteToString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();

            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        public string GetHashSenhaSHA256(string senha, string? salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha + salt));

                return ByteToString(bytes);
            }
        }

        public string GetSalt()
        {
            var salt = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return ByteToString(salt);
        }

        public string GenerateToken(UsuarioDto usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("nome", usuario.Nome),
                new Claim("tipo", usuario.Tipo)
            };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool GetAcesso(string controller, string operacao, string token)
        {
            if (token == null)
                throw new Exception("Forneça um token!");

            var dadosToken = GetDadosToken(token);

            if (dadosToken.Tipo == Tipos.Admin)
                return true;

            foreach (var acesso in Acessos.acessos)
            {
                var acessoOperacao = acesso.controller.Split("_").LastOrDefault();

                if (acesso.controller.Contains(controller) && acessoOperacao == operacao)
                {
                    if (acesso.tiposAceitos.Contains(dadosToken.Tipo))
                        return true;
                    else
                        return false; // encontrou o controller, mas o tipo do usuário não possui a permissão
                }
            }

            return false;
        }

        public JwtSecurityToken LerToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenJwt = handler.ReadJwtToken(token);

            return tokenJwt;
        }

        public JwtSecurityToken ValidaToken(string token)
        {
            if (token == null)
                throw new Exception("Token inválido!");

            var tokenJwt = LerToken(token.Replace("Bearer", "").Trim());

            if(tokenJwt.Payload == null)
                throw new Exception("Token inválido!");

            if(!tokenJwt.Payload.ContainsKey("nome"))
                throw new Exception("Token inválido!");

            if (!tokenJwt.Payload.ContainsKey("tipo"))
                throw new Exception("Token inválido!");

            if ((tokenJwt.ValidFrom > DateTime.UtcNow) || (tokenJwt.ValidTo < DateTime.UtcNow))
                throw new Exception("Token expirado!");

            return tokenJwt;
        }

        public DadosTokenDto GetDadosToken(string token)
        {
            var tokenJwt = ValidaToken(token);

            var dadosToken = new DadosTokenDto
            {
                Id = int.Parse(tokenJwt.Payload["id"].ToString()),
                Nome = tokenJwt.Payload["nome"].ToString(),
                Tipo = tokenJwt.Payload["tipo"].ToString()
            };

            return dadosToken;
        }
    }
}
