using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.Commons.DTO
{
    public class OrdineProdottoDTO
    {
        public int IdOrdine { get; set; }
        public int IdProdotto { get; set; }
        public int QuantitaAcquistata { get; set; }
    }
}
