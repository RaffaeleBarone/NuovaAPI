namespace NuovaAPI.Commons.DTO
{
    public class TaxonomyDTO
    {
        public string Name { get; set; }
        public List<TerminiDTO> Terms { get; set; }
        public string en_US { get; set; }
        public bool isActive { get; set; }
        public DateTime LastUpdate { get; set; }
        //public int Id { get; set; }
        //public string Labels { get; set; }
    }
}
