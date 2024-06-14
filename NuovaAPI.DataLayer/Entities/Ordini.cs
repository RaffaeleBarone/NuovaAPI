namespace NuovaAPI.DataLayer.Entities
{
    public class Ordini
    {
        public Ordini()
        {
            ProdottiAcquistati = new List<OrdineProdotto>();
        }
        public int Id { get; set; }
        public int CodiceOrdine { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public virtual ICollection<OrdineProdotto> ProdottiAcquistati { get; set; }

    }
}
