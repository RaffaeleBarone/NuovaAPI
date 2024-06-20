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
        public virtual Vetrina Vetrina { get; set; }
        public int? IdVetrina { get; set; }
        public virtual IEnumerable<OrdineProdotto> ProdottiAcquistati { get; set; }
    }
}
