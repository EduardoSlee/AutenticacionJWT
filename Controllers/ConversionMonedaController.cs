using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionMonedaAPI.Abstraccion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ConversionMonedaAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConversionMonedaController : ControllerBase
    {
        private readonly IConversionMoneda conversionMonedaBR;
        public IConfiguration configuration { get; }

        public ConversionMonedaController(IConversionMoneda _conversionMonedaBR, IConfiguration _configuration)
        {
            conversionMonedaBR = _conversionMonedaBR ??
                throw new ArgumentNullException(nameof(_conversionMonedaBR));

            configuration = _configuration ??
                throw new ArgumentNullException(nameof(_configuration));
        }

        [HttpGet]
        public IActionResult Convertir(DateTime fechaCambio, decimal monto)
        {
            try
            {
                //Realizamos validaciones
                string errMsg = string.Empty;
                if (fechaCambio == DateTime.MinValue)
                    errMsg = "- La fecha debe tener un valor." + Environment.NewLine;
                if (monto <= 0)
                    errMsg += "- El monto debe ser mayor a cero.";
                
                if(errMsg != string.Empty)
                    return UnprocessableEntity(errMsg);

                // Realizamos la conversión
                string tipoCambioAPI = configuration.GetSection("TipoCambioAPI").Value;
                var resultado = conversionMonedaBR.Convertir(tipoCambioAPI, fechaCambio, monto);

                // Retornamos el resultado
                if (resultado != null)
                    return Ok(resultado);
                else return NotFound($"No se cuenta el tipo de cambio en dólares a la fecha {fechaCambio.ToShortDateString()}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
