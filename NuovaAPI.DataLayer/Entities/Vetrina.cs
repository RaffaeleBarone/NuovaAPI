namespace NuovaAPI.DataLayer.Entities
{
    public class Vetrina
    {
        public Vetrina()
        {
            ProdottiInVetrina = new List<Prodotto>();
        }

        public int Id { get; set; }
        public int CodiceVetrina { get; set; }
        public virtual ICollection<Prodotto>? ProdottiInVetrina { get; set; }
    }
}