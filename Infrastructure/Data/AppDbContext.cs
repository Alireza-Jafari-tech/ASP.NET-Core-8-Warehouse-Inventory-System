using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Identity;
using Application.DTOs;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemRelation> ItemRelations { get; set; }
        public DbSet<ItemStatus> ItemsStatus { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierStatus> SuppliersStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrdersStatus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WarehouseLog> WarehouseLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemRelation>()
                  .HasKey(r => new { r.ItemId, r.RelatedItemId });

            modelBuilder.Entity<ItemRelation>()
                .HasOne(r => r.Item)
                .WithMany(i => i.RelatedItems)
                .HasForeignKey(r => r.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemRelation>()
                .HasOne(r => r.RelatedItemEntity)
                .WithMany(i => i.RelatedToItems)
                .HasForeignKey(r => r.RelatedItemId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderItem>()
                .HasKey(k => new { k.OrderId, k.ItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.Quantity)
                .IsRequired();

            modelBuilder.Entity<Supplier>()
              .HasMany(s => s.SuppliedItems)
              .WithOne(i => i.Supplier)
              .HasForeignKey(i => i.SupplierId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
              .Property(i => i.Cost)
              .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
              .Property(i => i.TotalCost)
              .HasPrecision(18, 2);
        }
    }
}