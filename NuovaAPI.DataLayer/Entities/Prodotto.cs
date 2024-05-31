using System.ComponentModel.DataAnnotations;

namespace NuovaAPI.DataLayer.Entities
{
    public class Prodotto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NomeProdotto { get; set; }
        [Required]
        public float Prezzo { get; set; }
        public virtual Vetrina Vetrina { get; set; }
        [Required]
        public int IdVetrina { get; set; }
    }
}
