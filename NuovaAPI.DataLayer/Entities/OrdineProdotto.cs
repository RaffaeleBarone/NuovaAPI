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
        //public int Id { get; set; }
        [JsonIgnore]
        public virtual Ordini Ordine { get; set; }
        public int IdOrdine { get; set; }
        [JsonIgnore]
        public virtual Prodotto Prodotto { get; set; }
        public int IdProdotto { get; set; }
        //public string NomeProdotto { get; set; }
        //public ICollection<OrdineProdotto> ProdottiNellOrdine { get; set; }
        public int QuantitaAcquistata { get; set; }
    }
}
