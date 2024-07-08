using System.ComponentModel.DataAnnotations;

namespace NuovaAPI.Commons.DTO
{
    public class ProdottoDTO
    {
        //public int Id { get; set; }
        public string? NomeProdotto { get; set; }
        public string CodiceProdotto { get; set; }
        public float? Prezzo { get; set; }
        public int? Quantita { get; set; }
        public int? IdVetrina { get; set; }
        public int? IdOrdine { get; set; }
    }
}
