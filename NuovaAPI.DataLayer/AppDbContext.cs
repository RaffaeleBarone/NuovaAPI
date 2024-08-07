﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false)
              .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Vetrina> Vetrine { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Ordini> Ordini { get; set; }
        public DbSet<OrdineProdotto> OrdiniProdotti { get; set; }
        public DbSet<Taxonomy> Taxonomy { get; set; }
        public DbSet<Termini> Termini { get; set; }
        public AppDbContext()
        {

        }
    }
}
