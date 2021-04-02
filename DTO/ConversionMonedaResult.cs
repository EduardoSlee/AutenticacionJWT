using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionMonedaAPI.DTO
{
    public class ConversionMonedaResult
    {
        public string Origen { get; set; }
        public decimal TipoCambioVenta { get; set; }
        public decimal MontoOriginal { get; set; }
        public decimal MontoConvertido { get; set; }
    }
}
