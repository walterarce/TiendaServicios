﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    [DbContext(typeof(ContextoCarrito))]
    partial class ContextoCarritoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoSession", b =>
                {
                    b.Property<int>("CarritoSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.HasKey("CarritoSessionId");

                    b.ToTable("CarritoSession");
                });

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoSessionDetalle", b =>
                {
                    b.Property<int>("CarritoSessionDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarritoSessionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<string>("ProductoSeleccionado")
                        .HasColumnType("text");

                    b.HasKey("CarritoSessionDetalleId");

                    b.HasIndex("CarritoSessionId");

                    b.ToTable("CarritoSessionDetalle");
                });

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoSessionDetalle", b =>
                {
                    b.HasOne("TiendaServicios.Api.CarritoCompra.Modelo.CarritoSession", "CarritoSession")
                        .WithMany("ListaDetalle")
                        .HasForeignKey("CarritoSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}