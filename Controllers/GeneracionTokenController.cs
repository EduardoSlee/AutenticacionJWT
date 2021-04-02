using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ConversionMonedaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneracionTokenController : ControllerBase
    {
        public IConfiguration configuration { get; }

        public GeneracionTokenController(IConfiguration _configuration)
        {
            configuration = _configuration ??
                throw new ArgumentNullException(nameof(_configuration));
        }

        [HttpGet]
        public IActionResult Generar()
        {
            try
            {
                var token = GenerarToken();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private string GenerarToken()
        {
            Claim[] claims = new[]
            {
            new Claim("Nombre", "Eduardo Slee"),
            new Claim("Empresa", "Slee Technologies"),
            new Claim("Url", "https://www.linkedin.com/in/eduardoslee/")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Credentials:SecretToken"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int horasExpiracion = Int32.Parse(configuration["Credentials:HoursExpiration"]);
            DateTime expiration = DateTime.UtcNow.AddHours(horasExpiracion);

            JwtSecurityToken secToken = new JwtSecurityToken(
            issuer: configuration["Credentials:Domain"],
            audience: configuration["Credentials:Domain"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(secToken);

            return token;
        }
    }
}
