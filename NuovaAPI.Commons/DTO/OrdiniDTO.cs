namespace NuovaAPI.Commons.DTO
{
    public class OrdiniDTO
    {
        public int CodiceOrdine { get; set; }
        public IEnumerable<string> Prodotti { get; set; }
        public double Costo { get; set; }
    }
}
