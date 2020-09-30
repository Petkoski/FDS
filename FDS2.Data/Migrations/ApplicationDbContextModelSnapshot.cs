﻿// <auto-generated />
using System;
using FDS2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FDS2.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FDS2.Data.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FDS2.Data.Models.CountryUpdate", b =>
                {
                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CountryId", "UpdateId");

                    b.HasIndex("UpdateId");

                    b.ToTable("CountryUpdate");
                });

            modelBuilder.Entity("FDS2.Data.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Checksum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FDS2.Data.Models.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("FDS2.Data.Models.PackageFile", b =>
                {
                    b.Property<Guid>("PackageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PackageId", "FileId");

                    b.HasIndex("FileId");

                    b.ToTable("PackageFile");
                });

            modelBuilder.Entity("FDS2.Data.Models.Update", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<bool>("CountryRestrictions")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PackageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("VersionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("VersionId");

                    b.ToTable("Updates");
                });

            modelBuilder.Entity("FDS2.Data.Models.UpdateFile", b =>
                {
                    b.Property<Guid>("UpdateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UpdateId", "FileId");

                    b.HasIndex("FileId");

                    b.ToTable("UpdateFile");
                });

            modelBuilder.Entity("FDS2.Data.Models.Version", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("FDS2.Data.Models.CountryUpdate", b =>
                {
                    b.HasOne("FDS2.Data.Models.Country", "Country")
                        .WithMany("CountryUpdates")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FDS2.Data.Models.Update", "Update")
                        .WithMany("CountryUpdates")
                        .HasForeignKey("UpdateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FDS2.Data.Models.PackageFile", b =>
                {
                    b.HasOne("FDS2.Data.Models.File", "File")
                        .WithMany("PackageFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FDS2.Data.Models.Package", "Package")
                        .WithMany("PackageFiles")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FDS2.Data.Models.Update", b =>
                {
                    b.HasOne("FDS2.Data.Models.Package", null)
                        .WithMany("Updates")
                        .HasForeignKey("PackageId");

                    b.HasOne("FDS2.Data.Models.Version", "Version")
                        .WithMany()
                        .HasForeignKey("VersionId");
                });

            modelBuilder.Entity("FDS2.Data.Models.UpdateFile", b =>
                {
                    b.HasOne("FDS2.Data.Models.File", "File")
                        .WithMany("UpdateFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FDS2.Data.Models.Update", "Update")
                        .WithMany("UpdateFiles")
                        .HasForeignKey("UpdateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
