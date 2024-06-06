using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.DataLayer.Configurations
{
    public class VetrinaConfiguration : IEntityTypeConfiguration<Vetrina>
    {
        public void Configure(EntityTypeBuilder<Vetrina> builder)
        {
            builder.ToTable("Vetrina").HasKey(x => x.Id);
        }
    }
}
