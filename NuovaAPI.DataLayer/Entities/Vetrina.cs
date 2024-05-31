namespace NuovaAPI.DataLayer.Entities
{
    public class Vetrina
    {
        public int Id { get; set; }
        public virtual ICollection<Prodotto>? ProdottiInVetrina { get; set; }
    }
}
