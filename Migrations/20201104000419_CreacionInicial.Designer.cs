﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asap.mvc;

namespace asap.mvc.Migrations
{
    [DbContext(typeof(NotasContext))]
    [Migration("20201104000419_CreacionInicial")]
    partial class CreacionInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("asap.mvc.Nota", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreadorMail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cuerpo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CreadorMail");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("asap.mvc.Usuario", b =>
                {
                    b.Property<string>("Mail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mail");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("asap.mvc.Nota", b =>
                {
                    b.HasOne("asap.mvc.Usuario", "Creador")
                        .WithMany("Notas")
                        .HasForeignKey("CreadorMail");
                });
#pragma warning restore 612, 618
        }
    }
}
