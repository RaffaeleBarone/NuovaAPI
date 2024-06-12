using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.DataLayer.Configurations
{
    public class OrdiniConfiguration : IEntityTypeConfiguration<Ordini>
    {
        public void Configure(EntityTypeBuilder<Ordini> builder)
        {
            builder.ToTable("Ordini")
                .HasKey(x => x.Id);
            builder.HasOne(a => a.Cliente)
                .WithMany(c => c.Ordini)
                .HasForeignKey(a => a.ClienteId);

            builder.HasMany(o => o.ProdottiAcquistati)
                .WithOne(x => x.Ordini)
                .HasForeignKey(x => x.IdOrdine);

        }
    }
}
