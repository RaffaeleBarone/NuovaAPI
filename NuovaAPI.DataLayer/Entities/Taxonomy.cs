namespace NuovaAPI.DataLayer.Entities
{
    public class Taxonomy
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Termini> Termini { get; set; }
        public string en_US { get; set; }
        public bool isActive { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
