﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuovaAPI.DataLayer;

#nullable disable

namespace NuovaAPI.DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240705081325_IsDefault")]
    partial class IsDefault
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataDiNascita")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.OrdineProdotto", b =>
                {
                    b.Property<int>("IdOrdine")
                        .HasColumnType("int");

                    b.Property<int>("IdProdotto")
                        .HasColumnType("int");

                    b.Property<int>("QuantitaAcquistata")
                        .HasColumnType("int");

                    b.HasKey("IdOrdine", "IdProdotto");

                    b.HasIndex("IdProdotto");

                    b.ToTable("OrdiniProdotti");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Ordini", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("CodiceOrdine")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Ordini", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Prodotto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdVetrina")
                        .HasColumnType("int");

                    b.Property<string>("NomeProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Prezzo")
                        .HasColumnType("real");

                    b.Property<int>("QuantitaDisponibile")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdVetrina");

                    b.ToTable("Prodotto", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Taxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Taxonomy", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Termini", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Lingua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxonomyId")
                        .HasColumnType("int");

                    b.Property<string>("Traduzione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaxonomyId");

                    b.ToTable("Termini", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Vetrina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CodiceVetrina")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CodiceVetrina")
                        .IsUnique();

                    b.ToTable("Vetrina", (string)null);
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.OrdineProdotto", b =>
                {
                    b.HasOne("NuovaAPI.DataLayer.Entities.Ordini", "Ordine")
                        .WithMany("ProdottiAcquistati")
                        .HasForeignKey("IdOrdine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NuovaAPI.DataLayer.Entities.Prodotto", "Prodotto")
                        .WithMany("ProdottiAcquistati")
                        .HasForeignKey("IdProdotto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ordine");

                    b.Navigation("Prodotto");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Ordini", b =>
                {
                    b.HasOne("NuovaAPI.DataLayer.Entities.Cliente", "Cliente")
                        .WithMany("Ordini")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Prodotto", b =>
                {
                    b.HasOne("NuovaAPI.DataLayer.Entities.Vetrina", "Vetrina")
                        .WithMany("ProdottiInVetrina")
                        .HasForeignKey("IdVetrina");

                    b.Navigation("Vetrina");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Termini", b =>
                {
                    b.HasOne("NuovaAPI.DataLayer.Entities.Taxonomy", "Taxonomy")
                        .WithMany("Termini")
                        .HasForeignKey("TaxonomyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Taxonomy");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Cliente", b =>
                {
                    b.Navigation("Ordini");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Ordini", b =>
                {
                    b.Navigation("ProdottiAcquistati");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Prodotto", b =>
                {
                    b.Navigation("ProdottiAcquistati");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Taxonomy", b =>
                {
                    b.Navigation("Termini");
                });

            modelBuilder.Entity("NuovaAPI.DataLayer.Entities.Vetrina", b =>
                {
                    b.Navigation("ProdottiInVetrina");
                });
#pragma warning restore 612, 618
        }
    }
}
