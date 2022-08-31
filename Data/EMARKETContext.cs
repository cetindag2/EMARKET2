using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EMARKET_MVC.Models;

namespace EMARKET_MVC.Data
{
    public partial class EMARKETContext : DbContext
    {
        public EMARKETContext()
        {
        }

        public EMARKETContext(DbContextOptions<EMARKETContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Localhost\\SQLEXPRESS;Database=EMARKET;User ID=cetin;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("BASKETS");

                entity.Property(e => e.BasketId)
                    .HasMaxLength(50)
                    .HasColumnName("BASKET_ID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_ID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_BASKETS_CUSTOMERS");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_BASKETS_PRODUCTS");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("SURNAME");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDER");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_ID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ORDER_CUSTOMERS");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ORDER_PRODUCTS");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("PRICE");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductProperty)
                    .HasMaxLength(200)
                    .HasColumnName("PRODUCT_PROPERTY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
