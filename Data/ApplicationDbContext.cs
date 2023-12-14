using JqueryDataTableProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JqueryDataTableProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TblContact> TblContacts { get; set; }
        public DbSet<TimeSession> TimeSession { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblContact>(entity =>
            {
                entity.HasKey(e => e.Id); // Define 'Id' as the primary key

                entity.ToTable("Tbl_Contact");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Subject).HasMaxLength(500);
            });
        }

    }
}
