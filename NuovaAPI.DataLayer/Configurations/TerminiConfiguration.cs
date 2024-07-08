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
           .HasKey(x => x.Id);

            builder.HasOne(te => te.Taxonomy)
                .WithMany(t => t.Termini)
                .HasForeignKey(te => te.TaxonomyId);

            //builder.OwnsOne(te => te.Labels, lb =>
            //{
            //    lb.Property(l => l.en_US).IsRequired();
            //    lb.Property(l => l.fr_FR).IsRequired();
            //    lb.Property(l => l.it_IT).IsRequired();
            //});

            builder.Property(te => te.Lingua).IsRequired();
            builder.Property(te => te.Traduzione).IsRequired();
            builder.Property(te => te.IsDefault).IsRequired();

            //builder.HasOne(te => te.Taxonomy)
            //     .WithMany(t => t.Termini)
            //.HasForeignKey(te => te.TaxonomyId);

        }
    }
}
