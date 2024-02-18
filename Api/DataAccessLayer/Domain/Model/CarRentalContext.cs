using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public partial class CarRentalContext : DbContext
{
    private readonly IKeyVaultRepository _keyVaultRepository;
    public CarRentalContext(IKeyVaultRepository keyVaultRepository)
    {
        _keyVaultRepository = keyVaultRepository;
    }

    public CarRentalContext(DbContextOptions<CarRentalContext> options, IKeyVaultRepository keyVaultRepository)
        : base(options)
    {
     _keyVaultRepository = keyVaultRepository;
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderContent> OrderContents { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleMake> VehicleMakes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =_keyVaultRepository.GetSecret("ConnectionString");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC079CD0E5F6");

            entity.ToTable("Order");

            entity.Property(e => e.CancelledOn).HasColumnType("datetime");
            entity.Property(e => e.OrderedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderCon__3214EC07903B7B67");

            entity.ToTable("OrderContent");

            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.VehicleUsDollarRatePerDay).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCont__Order__656C112C");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCont__Vehic__6477ECF3");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3214EC0793B2D01C");

            entity.ToTable("Vehicle");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsDollarRatePerDay).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Make).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.MakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicle__MakeId__66603565");
        });

        modelBuilder.Entity<VehicleMake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleM__3214EC07AD8C1679");

            entity.ToTable("VehicleMake");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
