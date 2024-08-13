﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeminarWork.Model;

#nullable disable

namespace SeminarWork.Migrations
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
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SeminarWork.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2")
                        .HasColumnName("messageData");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit")
                        .HasColumnName("is_sent");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("messageName");

                    b.Property<int>("UserFromId")
                        .HasColumnType("int");

                    b.Property<int>("UserTOId")
                        .HasColumnType("int");

                    b.HasKey("MessageId")
                        .HasName("messagePK");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserTOId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("SeminarWork.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FullName");

                    b.HasKey("Id")
                        .HasName("user_pkey");

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("SeminarWork.Message", b =>
                {
                    b.HasOne("SeminarWork.User", "UserFrom")
                        .WithMany("messagesFrom")
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("messageFromUserFK");

                    b.HasOne("SeminarWork.User", "UserTO")
                        .WithMany("messagesTo")
                        .HasForeignKey("UserTOId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("messageToUserFK");

                    b.Navigation("UserFrom");

                    b.Navigation("UserTO");
                });

            modelBuilder.Entity("SeminarWork.User", b =>
                {
                    b.Navigation("messagesFrom");

                    b.Navigation("messagesTo");
                });
#pragma warning restore 612, 618
        }
    }
}