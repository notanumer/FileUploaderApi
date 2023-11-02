﻿// <auto-generated />
using System;
using FileUploader.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileUploader.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231102173437_ChangeGuidToString")]
    partial class ChangeGuidToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FileUploader.Domain.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FileGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FileGroupId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileUploader.Domain.FileGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("DownloadedByTokenAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("FileGroups");
                });

            modelBuilder.Entity("FileUploader.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FileUploader.Domain.File", b =>
                {
                    b.HasOne("FileUploader.Domain.FileGroup", "FileGroup")
                        .WithMany("Files")
                        .HasForeignKey("FileGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileGroup");
                });

            modelBuilder.Entity("FileUploader.Domain.FileGroup", b =>
                {
                    b.HasOne("FileUploader.Domain.User", "CreatedByUser")
                        .WithMany("FileGroups")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("FileUploader.Domain.FileGroup", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("FileUploader.Domain.User", b =>
                {
                    b.Navigation("FileGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
