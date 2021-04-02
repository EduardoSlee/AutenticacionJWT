using ConversionMonedaAPI.Abstraccion;
using ConversionMonedaAPI.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConversionMonedaAPI.Implementacion
{
    public class ConversionMonedaBR: IConversionMoneda
    {
        public ConversionMonedaResult Convertir(string url, DateTime fechaCambio, decimal monto)
        {
            try
            {
                url = string.Format(url, fechaCambio.ToString("yyyy-MM-dd"));
                string jsonString = ExecuteGet(url).Result;
                dynamic json = JValue.Parse(jsonString);
                if (json.venta != null)
                {
                    return new ConversionMonedaResult()
                    {
                        Origen = json.origen,
                        TipoCambioVenta = json.venta,
                        MontoOriginal = monto,
                        MontoConvertido = monto * Convert.ToDecimal(json.venta)
                    };
                }
                else return null;
                   // throw new Exception($"No se cuenta el tipo de cambio en dólares a la fecha {fechaCambio.ToShortDateString()}.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> ExecuteGet(string urlString)
        {
            string result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                using (var response = await httpClient.GetAsync(urlString))
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }
    }
}
