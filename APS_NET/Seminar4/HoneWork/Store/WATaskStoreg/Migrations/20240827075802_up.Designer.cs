﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WATaskStoreg.Models.Context;

#nullable disable

namespace WATaskStoreg.Migrations
{
    [DbContext(typeof(StoregeContext))]
    [Migration("20240827075802_up")]
    partial class up
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WATaskStoreg.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Count")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("integer")
                        .HasColumnName("ProductCount");

                    b.Property<string>("Descript")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Descript");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("PositionName");

                    b.Property<int?>("productId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("integer")
                        .HasColumnName("ProductID");

                    b.HasKey("Id")
                        .HasName("PositionID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Storage", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
