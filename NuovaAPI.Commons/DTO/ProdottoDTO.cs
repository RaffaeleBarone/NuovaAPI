using System.ComponentModel.DataAnnotations;

namespace NuovaAPI.Commons.DTO
{
    public class ProdottoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NomeProdotto { get; set; }
        [Required]
        public float Prezzo { get; set; }
        [Required]
        public int IdVetrina { get; set; }
    }
}
