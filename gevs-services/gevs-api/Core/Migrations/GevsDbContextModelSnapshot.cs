﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using gevs_api.Data;

#nullable disable

namespace gevs_api.Core.Migrations
{
    [DbContext(typeof(GevsDbContext))]
    partial class GevsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("gevs_api.Domain.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConstituencyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PartyId")
                        .HasColumnType("uuid");

                    b.Property<int>("VoteCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConstituencyId");

                    b.HasIndex("PartyId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("gevs_api.Domain.Constituency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Constituencies");
                });

            modelBuilder.Entity("gevs_api.Domain.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("gevs_api.Domain.Candidate", b =>
                {
                    b.HasOne("gevs_api.Domain.Constituency", "Constituency")
                        .WithMany()
                        .HasForeignKey("ConstituencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gevs_api.Domain.Party", "Party")
                        .WithMany()
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Constituency");

                    b.Navigation("Party");
                });
#pragma warning restore 612, 618
        }
    }
}