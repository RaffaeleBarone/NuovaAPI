namespace NuovaAPI.DataLayer.Entities
{
    public class Termini
    {
        public int Id { get; set; }
        //public Labels Labels {  get; set; } 
        public string Lingua { get; set; }
        public string Traduzione { get; set; }
        public bool IsDefault { get; set; }
        public int TaxonomyId { get; set; }
        public Taxonomy Taxonomy { get; set; }
    }
}
