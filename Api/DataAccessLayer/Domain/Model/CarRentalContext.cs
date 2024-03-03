using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Domain.Model;

public partial class CarRentalContext : DbContext
{
    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderContent> OrderContents { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleMake> VehicleMakes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC0766D6EC58");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CancelledOn).HasColumnType("datetime");
            entity.Property(e => e.OrderedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderCon__3214EC07B01DA370");

            entity.ToTable("OrderContent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.VehicleUsDollarRatePerDay).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCont__Order__08B54D69");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCont__Vehic__09A971A2");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3214EC0788DF27CD");

            entity.ToTable("Vehicle");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsDollarRatePerDay).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Make).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.MakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicle__MakeId__0A9D95DB");
        });

        modelBuilder.Entity<VehicleMake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleM__3214EC0738927AAD");

            entity.ToTable("VehicleMake");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
