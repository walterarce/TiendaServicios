﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Migrations
{
    [DbContext(typeof(ContextoAutor))]
    partial class ContextoAutorModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.AutorLibro", b =>
                {
                    b.Property<int>("AutorLibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("AutorLibroGuid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AutorLibroId");

                    b.ToTable("AutorLibro");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.GradoAcademico", b =>
                {
                    b.Property<int>("GraadoAcademicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AutorLibroId")
                        .HasColumnType("integer");

                    b.Property<string>("CentroAcademico")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaGrado")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GradoAcademicoGuid")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("GraadoAcademicoId");

                    b.HasIndex("AutorLibroId");

                    b.ToTable("GradoAcGradoAcademico");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Modelo.GradoAcademico", b =>
                {
                    b.HasOne("TiendaServicios.Api.Autor.Modelo.AutorLibro", "AutorLibro")
                        .WithMany("ListaGradoAcademicos")
                        .HasForeignKey("AutorLibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
