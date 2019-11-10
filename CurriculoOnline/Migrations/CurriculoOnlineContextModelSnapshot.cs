﻿// <auto-generated />
using System;
using CurriculoOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CurriculoOnline.Migrations
{
    [DbContext(typeof(CurriculoOnlineContext))]
    partial class CurriculoOnlineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CurriculoOnline.Models.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular");

                    b.Property<int?>("CidadeId");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Endereco");

                    b.Property<string>("Nacionalidade");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeMae");

                    b.Property<string>("NomePai");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.ToTable("Candidato");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Cidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EstadoId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidade");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("UF");

                    b.HasKey("Id");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Experiencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CandidatoId");

                    b.Property<int?>("CidadeId");

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("Detalhes");

                    b.Property<string>("Empresa");

                    b.Property<string>("Profissao");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId");

                    b.HasIndex("CidadeId");

                    b.ToTable("Experiencia");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Formacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CandidatoId");

                    b.Property<double>("CargaHoraria");

                    b.Property<int?>("CidadeId");

                    b.Property<string>("Curso");

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("Instituicao");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId");

                    b.HasIndex("CidadeId");

                    b.ToTable("Formacao");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Candidato", b =>
                {
                    b.HasOne("CurriculoOnline.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Cidade", b =>
                {
                    b.HasOne("CurriculoOnline.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Experiencia", b =>
                {
                    b.HasOne("CurriculoOnline.Models.Candidato", "Candidato")
                        .WithMany("Experiencias")
                        .HasForeignKey("CandidatoId");

                    b.HasOne("CurriculoOnline.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");
                });

            modelBuilder.Entity("CurriculoOnline.Models.Formacao", b =>
                {
                    b.HasOne("CurriculoOnline.Models.Candidato", "Candidato")
                        .WithMany("Formacoes")
                        .HasForeignKey("CandidatoId");

                    b.HasOne("CurriculoOnline.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");
                });
#pragma warning restore 612, 618
        }
    }
}
