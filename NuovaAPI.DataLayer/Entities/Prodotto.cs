using System.ComponentModel.DataAnnotations;

namespace NuovaAPI.DataLayer.Entities
{
    public class Prodotto
    {   
        public int Id { get; set; }
        public string NomeProdotto { get; set; }
        public float Prezzo { get; set; }
        public int Quantita { get; set; }   
        public virtual Vetrina Vetrina { get; set; }
        public int IdVetrina { get; set; }
       
    }
}
