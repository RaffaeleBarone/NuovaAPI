using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Configurations
{
    public class TerminiConfiguration : IEntityTypeConfiguration<Termini>
    {
        public void Configure(EntityTypeBuilder<Termini> builder)
        {
            builder.ToTable("Termini")
                .HasKey(x => new
                {
                    x.TaxonomyId,
                    x.Traduzione,
                    x.Lingua
                });

            //builder.HasOne(te => te.Taxonomy)
            //    .WithMany(t => t.Termini)
            //    .HasForeignKey(te => te.TaxonomyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(te => te.Lingua).IsRequired();
            builder.Property(te => te.Traduzione).IsRequired();
            builder.Property(te => te.IsDefault).IsRequired();
        }
    }

}
