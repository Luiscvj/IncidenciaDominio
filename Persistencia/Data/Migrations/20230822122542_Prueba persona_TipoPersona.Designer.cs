﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

#nullable disable

namespace Persistencia.Data.Migrations
{
    [DbContext(typeof(IncidenciaContext))]
    [Migration("20230822122542_Prueba persona_TipoPersona")]
    partial class Pruebapersona_TipoPersona
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dominio.Ciudad", b =>
                {
                    b.Property<string>("IdCiudad")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("DepartamentoId")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<string>("NombreCiudad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdCiudad");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("ciudad", (string)null);
                });

            modelBuilder.Entity("Dominio.Departamento", b =>
                {
                    b.Property<string>("IdDep")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("NombreDep")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PaisId")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.HasKey("IdDep");

                    b.HasIndex("PaisId");

                    b.ToTable("departamento", (string)null);
                });

            modelBuilder.Entity("Dominio.Direccion", b =>
                {
                    b.Property<int>("IdDireccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Letra")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("NroViaSecundaria")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("PersonaId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SufijoCardinal")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SufijoCardinalSec")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TipoVia")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdDireccion");

                    b.HasIndex("PersonaId");

                    b.ToTable("direccion", (string)null);
                });

            modelBuilder.Entity("Dominio.Genero", b =>
                {
                    b.Property<int>("IdGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreGenero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdGenero");

                    b.ToTable("GENERO", (string)null);
                });

            modelBuilder.Entity("Dominio.Matricula", b =>
                {
                    b.Property<int>("IdMatricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PersonaId")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("IdMatricula");

                    b.HasIndex("PersonaId");

                    b.HasIndex("SalonId");

                    b.ToTable("matricula", (string)null);
                });

            modelBuilder.Entity("Dominio.Pais", b =>
                {
                    b.Property<string>("PaisId")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("NombrePais")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("PaisId");

                    b.ToTable("pais", (string)null);
                });

            modelBuilder.Entity("Dominio.Persona", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CiudadId")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TipoPersonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.HasIndex("GeneroId");

                    b.ToTable("persona", (string)null);
                });

            modelBuilder.Entity("Dominio.Salon", b =>
                {
                    b.Property<int>("IdSalon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.Property<string>("NombreSalon")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdSalon");

                    b.ToTable("salon", (string)null);
                });

            modelBuilder.Entity("Dominio.TipoPersona", b =>
                {
                    b.Property<int>("IdTipoPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdTipoPersona");

                    b.ToTable("tipo_persona", (string)null);
                });

            modelBuilder.Entity("Dominio.TrainerSalon", b =>
                {
                    b.Property<string>("PersonaId")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("PersonaId", "SalonId");

                    b.HasIndex("SalonId");

                    b.ToTable("trainer_salon", (string)null);
                });

            modelBuilder.Entity("PersonaTipoPersona", b =>
                {
                    b.Property<string>("PersonasId")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TipoPersonasIdTipoPersona")
                        .HasColumnType("int");

                    b.HasKey("PersonasId", "TipoPersonasIdTipoPersona");

                    b.HasIndex("TipoPersonasIdTipoPersona");

                    b.ToTable("persona_TipoPersonas", (string)null);
                });

            modelBuilder.Entity("Dominio.Ciudad", b =>
                {
                    b.HasOne("Dominio.Departamento", "Departamento")
                        .WithMany("Ciudades")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Dominio.Departamento", b =>
                {
                    b.HasOne("Dominio.Pais", "Pais")
                        .WithMany("Departamentos")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("Dominio.Direccion", b =>
                {
                    b.HasOne("Dominio.Persona", "Persona")
                        .WithMany("Direcciones")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Dominio.Matricula", b =>
                {
                    b.HasOne("Dominio.Persona", "Persona")
                        .WithMany("Matriculas")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Salon", "Salon")
                        .WithMany("Matriculas")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Dominio.Persona", b =>
                {
                    b.HasOne("Dominio.Ciudad", "Ciudad")
                        .WithMany("Personas")
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Genero", "Genero")
                        .WithMany("Personas")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciudad");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("Dominio.TrainerSalon", b =>
                {
                    b.HasOne("Dominio.Persona", "Persona")
                        .WithMany("TrainerSalones")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Salon", "Salon")
                        .WithMany("TrainerSalones")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("PersonaTipoPersona", b =>
                {
                    b.HasOne("Dominio.Persona", null)
                        .WithMany()
                        .HasForeignKey("PersonasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.TipoPersona", null)
                        .WithMany()
                        .HasForeignKey("TipoPersonasIdTipoPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Ciudad", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Dominio.Departamento", b =>
                {
                    b.Navigation("Ciudades");
                });

            modelBuilder.Entity("Dominio.Genero", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Dominio.Pais", b =>
                {
                    b.Navigation("Departamentos");
                });

            modelBuilder.Entity("Dominio.Persona", b =>
                {
                    b.Navigation("Direcciones");

                    b.Navigation("Matriculas");

                    b.Navigation("TrainerSalones");
                });

            modelBuilder.Entity("Dominio.Salon", b =>
                {
                    b.Navigation("Matriculas");

                    b.Navigation("TrainerSalones");
                });
#pragma warning restore 612, 618
        }
    }
}