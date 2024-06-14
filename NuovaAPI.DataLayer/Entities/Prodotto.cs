using System.Text.Json.Serialization;

namespace NuovaAPI.DataLayer.Entities
{
    //[PrimaryKey(nameof(Id))]
    public class Prodotto
    {
        public int Id { get; set; }
        public string NomeProdotto { get; set; }
        public float Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
        [JsonIgnore]
        public virtual Vetrina Vetrina { get; set; }
        public int? IdVetrina { get; set; }
        //[JsonIgnore]
        //public virtual Ordini Ordini { get; set; }
        //public int? IdOrdine { get; set; }
        public virtual ICollection<OrdineProdotto> ProdottiAcquistati { get; set; }
    }
}
