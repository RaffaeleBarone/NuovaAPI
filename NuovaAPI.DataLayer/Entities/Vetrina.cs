namespace NuovaAPI.DataLayer.Entities
{
    public class Vetrina
    {
        public int Id { get; set; }
        public int CodiceVetrina { get; set; }
        public virtual IEnumerable<Prodotto>? ProdottiInVetrina { get; set; }
    }
}