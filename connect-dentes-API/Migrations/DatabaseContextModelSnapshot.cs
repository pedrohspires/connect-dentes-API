﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using connect_dentes_API;

#nullable disable

namespace connectdentesAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("connect_dentes_API.Entities.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer")
                        .HasColumnName("id_cliente");

                    b.Property<DateTime>("DataAgendada")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_agendada");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime?>("DataEdicao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_edicao");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("UsuarioCadastro")
                        .HasColumnType("text")
                        .HasColumnName("usuario_cadastro");

                    b.Property<string>("UsuarioEdicao")
                        .HasColumnType("text")
                        .HasColumnName("usuario_edicao");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("agendamento");
                });

            modelBuilder.Entity("connect_dentes_API.Entities.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer")
                        .HasColumnName("id_cliente");

                    b.Property<DateTime>("DataAtendimento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_atendimento");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime?>("DataEdicao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_edicao");

                    b.Property<DateTime?>("DataRetorno")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_retorno");

                    b.Property<string>("Dentes")
                        .HasColumnType("text")
                        .HasColumnName("dentes");

                    b.Property<string>("Detalhes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("detalhes");

                    b.Property<int>("MedicoId")
                        .HasColumnType("integer")
                        .HasColumnName("id_medico");

                    b.Property<string>("Observacoes")
                        .HasColumnType("text")
                        .HasColumnName("observacoes");

                    b.Property<string>("UsuarioCadastro")
                        .HasColumnType("text")
                        .HasColumnName("usuario_cadastro");

                    b.Property<string>("UsuarioEdicao")
                        .HasColumnType("text")
                        .HasColumnName("usuario_edicao");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("MedicoId");

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("connect_dentes_API.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .HasColumnType("text")
                        .HasColumnName("bairro");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cidade");

                    b.Property<string>("Complemento")
                        .HasColumnType("text")
                        .HasColumnName("complemento");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime?>("DataEdicao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_edicao");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsWhatsapp")
                        .HasColumnType("boolean")
                        .HasColumnName("is_whatsapp");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<int?>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("numero");

                    b.Property<string>("Rua")
                        .HasColumnType("text")
                        .HasColumnName("rua");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("telefone");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("uf");

                    b.Property<DateTime?>("UltimoAtendimento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ultimo_atendimento");

                    b.Property<string>("UsuarioCadastro")
                        .HasColumnType("text")
                        .HasColumnName("usuario_cadastro");

                    b.Property<string>("UsuarioEdicao")
                        .HasColumnType("text")
                        .HasColumnName("usuario_edicao");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("connect_dentes_API.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime?>("DataEdicao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_edicao");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Salt")
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tipo");

                    b.Property<string>("UsuarioCadastro")
                        .HasColumnType("text")
                        .HasColumnName("usuario_cadastro");

                    b.Property<string>("UsuarioEdicao")
                        .HasColumnType("text")
                        .HasColumnName("usuario_edicao");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("connect_dentes_API.Entities.Agendamento", b =>
                {
                    b.HasOne("connect_dentes_API.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("connect_dentes_API.Entities.Atendimento", b =>
                {
                    b.HasOne("connect_dentes_API.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("connect_dentes_API.Entities.Usuario", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Medico");
                });
#pragma warning restore 612, 618
        }
    }
}
