using FDS2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<Models.Version> Versions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Software> Softwares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Update>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Models.Version>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<File>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Country>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Channel>().Property(x => x.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<Software>().Property(x => x.Id).HasDefaultValueSql("newid()");

            //UpdateCountry
            modelBuilder.Entity<UpdateCountry>()
                .HasKey(bc => new { bc.UpdateId, bc.CountryId });
            modelBuilder.Entity<UpdateCountry>()
                .HasOne(bc => bc.Update)
                .WithMany(b => b.UpdateCountries)
                .HasForeignKey(bc => bc.UpdateId);
            modelBuilder.Entity<UpdateCountry>()
                .HasOne(bc => bc.Country)
                .WithMany(c => c.UpdateCountries)
                .HasForeignKey(bc => bc.CountryId);

            //PackageFile
            modelBuilder.Entity<PackageFile>()
                .HasKey(bc => new { bc.PackageId, bc.FileId });
            modelBuilder.Entity<PackageFile>()
                .HasOne(bc => bc.Package)
                .WithMany(b => b.PackageFiles)
                .HasForeignKey(bc => bc.PackageId);
            modelBuilder.Entity<PackageFile>()
                .HasOne(bc => bc.File)
                .WithMany(c => c.PackageFiles)
                .HasForeignKey(bc => bc.FileId);

            //UpdateFile
            modelBuilder.Entity<UpdateFile>()
                .HasKey(bc => new { bc.UpdateId, bc.FileId });
            modelBuilder.Entity<UpdateFile>()
                .HasOne(bc => bc.Update)
                .WithMany(b => b.UpdateFiles)
                .HasForeignKey(bc => bc.UpdateId);
            modelBuilder.Entity<UpdateFile>()
                .HasOne(bc => bc.File)
                .WithMany(c => c.UpdateFiles)
                .HasForeignKey(bc => bc.FileId);

            //UpdateSoftware
            modelBuilder.Entity<UpdateSoftware>()
                .HasKey(bc => new { bc.UpdateId, bc.SoftwareId });
            modelBuilder.Entity<UpdateSoftware>()
                .HasOne(bc => bc.Update)
                .WithMany(b => b.UpdateSoftwares)
                .HasForeignKey(bc => bc.UpdateId);
            modelBuilder.Entity<UpdateSoftware>()
                .HasOne(bc => bc.Software)
                .WithMany(c => c.UpdateSoftwares)
                .HasForeignKey(bc => bc.SoftwareId);
        }
    }
}
