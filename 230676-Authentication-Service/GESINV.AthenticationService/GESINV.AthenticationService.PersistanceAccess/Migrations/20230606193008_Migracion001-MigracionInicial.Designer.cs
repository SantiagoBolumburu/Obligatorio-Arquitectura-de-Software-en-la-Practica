﻿// <auto-generated />
using System;
using GESINV.AthenticationService.PersistanceAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GESINV.AthenticationService.PersistanceAccess.Migrations
{
    [DbContext(typeof(AuthenticationContext))]
    [Migration("20230606193008_Migracion001-MigracionInicial")]
    partial class Migracion001MigracionInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Invitacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Utilizada")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Invitaciones");
                });

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmpresaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Invitacion", b =>
                {
                    b.HasOne("GESINV.AthenticationService.Dominio.Empresa", "Empresa")
                        .WithMany("Invitaciones")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Usuario", b =>
                {
                    b.HasOne("GESINV.AthenticationService.Dominio.Empresa", "Empresa")
                        .WithMany("Integrantes")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("GESINV.AthenticationService.Dominio.Empresa", b =>
                {
                    b.Navigation("Integrantes");

                    b.Navigation("Invitaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
