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
    public class OrdineProdottoConfiguration : IEntityTypeConfiguration<OrdineProdotto>
    {
        public void Configure(EntityTypeBuilder<OrdineProdotto> builder)
        {
            builder.HasKey(op => new { op.IdOrdine, op.IdProdotto });

            builder.HasOne(op => op.Ordine)
                .WithMany(o => o.ProdottiAcquistati)
                .HasForeignKey(op => op.IdOrdine);

            builder.HasOne(op => op.Prodotto)
                .WithMany(p => p.ProdottiAcquistati)
                .HasForeignKey(op => op.IdProdotto);                
        }
    }
}
