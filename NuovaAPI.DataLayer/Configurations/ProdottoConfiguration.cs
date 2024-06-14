using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.DataLayer.Configurations
{
    public class ProdottoConfiguration : IEntityTypeConfiguration<Prodotto>
    {
        public void Configure(EntityTypeBuilder<Prodotto> builder)
        {
            builder.ToTable("Prodotto").HasKey(x => x.Id);
            builder.HasOne<Vetrina>(x => x.Vetrina)
                .WithMany(x => x.ProdottiInVetrina)
                .HasForeignKey(x => x.IdVetrina);

            //builder.HasOne<Ordini>(x => x.Ordini)
            //.WithMany(x => x.ProdottiAcquistati)
            //.HasForeignKey(x => x.IdOrdine);
                
        }

    }
}
