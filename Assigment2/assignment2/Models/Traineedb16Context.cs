using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace assignment2.Models
{
    public partial class Traineedb16Context : DbContext
    {

        public Traineedb16Context(DbContextOptions<Traineedb16Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Employeee> Employeees { get; set; } = null!;
        public virtual DbSet<Hr> Hrs { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employeee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBB9959CDF39F");

                entity.ToTable("Employeee");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HomeAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("('true')");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WorkLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hr>(entity =>
            {
                entity.ToTable("Hr");

                entity.Property(e => e.EmpPayrollInfo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Hrs)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Hr__EmpId__681373AD");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.BuildingId).HasColumnName("buildingId");

                entity.Property(e => e.ManagerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("managerName");

                entity.Property(e => e.ZipCode).HasColumnName("zipCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
