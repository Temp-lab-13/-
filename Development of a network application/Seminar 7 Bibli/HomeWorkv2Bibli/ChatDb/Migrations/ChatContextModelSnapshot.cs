﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatDb.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeWork.Model.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MessageId"));

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("messageData");

                    b.Property<bool>("IsSent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sent");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("messageName");

                    b.Property<int>("UserFromId")
                        .HasColumnType("integer");

                    b.Property<int>("UserTOId")
                        .HasColumnType("integer");

                    b.HasKey("MessageId")
                        .HasName("messagePK");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserTOId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("HomeWork.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("FullName");

                    b.HasKey("Id")
                        .HasName("user_pkey");

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("HomeWork.Model.Message", b =>
                {
                    b.HasOne("HomeWork.Model.User", "UserFrom")
                        .WithMany("messagesFrom")
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("messageFromUserFK");

                    b.HasOne("HomeWork.Model.User", "UserTO")
                        .WithMany("messagesTo")
                        .HasForeignKey("UserTOId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("messageToUserFK");

                    b.Navigation("UserFrom");

                    b.Navigation("UserTO");
                });

            modelBuilder.Entity("HomeWork.Model.User", b =>
                {
                    b.Navigation("messagesFrom");

                    b.Navigation("messagesTo");
                });
#pragma warning restore 612, 618
        }
    }
}
