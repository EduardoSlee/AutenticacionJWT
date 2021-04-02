using ConversionMonedaAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionMonedaAPI.Abstraccion
{
    public interface IConversionMoneda
    {
        ConversionMonedaResult Convertir(string url, DateTime fechaCambio, decimal monto);
    }
}
