using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Entities
{
    public class OrdineProdotto
    {
        public virtual Ordini Ordine { get; set; }
        public int IdOrdine { get; set; }
        public virtual Prodotto Prodotto { get; set; }
        public int IdProdotto { get; set; }
        public int QuantitaAcquistata { get; set; }
    }
}
