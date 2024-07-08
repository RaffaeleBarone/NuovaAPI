using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Configurations
{
    public class TaxonomyConfiguration : IEntityTypeConfiguration<Taxonomy>
    {
        public void Configure(EntityTypeBuilder<Taxonomy> builder)
        {
            builder.ToTable("Taxonomy")
                .HasKey(x => x.Id);

            builder.HasMany(t => t.Termini)
                .WithOne(te => te.Taxonomy)
                .HasForeignKey(te => te.TaxonomyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

