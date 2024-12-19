﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TS.EasyStockManager.Data.Context;

namespace TS.EasyStockManager.Data.Migrations
{
    [DbContext(typeof(EasyStockManagerDbContext))]
    [Migration("20241219085950_CreateDB")]
    partial class CreateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UnitOfMeasureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Barcode = "EX01",
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 819, DateTimeKind.Local).AddTicks(1588),
                            Price = 1m,
                            ProductName = "Example Product",
                            UnitOfMeasureId = 1
                        });
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Store");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 818, DateTimeKind.Local).AddTicks(7909),
                            StoreCode = "EX01",
                            StoreName = "Example Store"
                        });
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.StoreStock", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Stock")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("StoreId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("StoreStock");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int?>("ToStoreId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("ToStoreId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.TransactionDetail", b =>
                {
                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TransactionId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("TransactionDetail");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TransactionType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 680, DateTimeKind.Local).AddTicks(337),
                            TransactionTypeName = "Stock Receipt"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 687, DateTimeKind.Local).AddTicks(914),
                            TransactionTypeName = "Stock Out"
                        },
                        new
                        {
                            Id = 3,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 687, DateTimeKind.Local).AddTicks(928),
                            TransactionTypeName = "Transfer"
                        });
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.UnitOfMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Isocode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("UnitOfMeasureName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("UnitOfMeasure");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 688, DateTimeKind.Local).AddTicks(3634),
                            Isocode = "pc",
                            UnitOfMeasureName = "Piece"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 688, DateTimeKind.Local).AddTicks(3763),
                            Isocode = "kg",
                            UnitOfMeasureName = "Kilogram"
                        },
                        new
                        {
                            Id = 3,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 688, DateTimeKind.Local).AddTicks(3769),
                            Isocode = "m",
                            UnitOfMeasureName = "Meter"
                        });
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 12, 19, 14, 29, 49, 818, DateTimeKind.Local).AddTicks(2611),
                            Email = "admin@admin.com",
                            Name = "Admin",
                            Password = "827ccb0eea8a706c4c34a16891f84e7b",
                            Surname = "Admin"
                        });
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Product", b =>
                {
                    b.HasOne("TS.EasyStockManager.Data.Entity.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId");

                    b.HasOne("TS.EasyStockManager.Data.Entity.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany("Product")
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.StoreStock", b =>
                {
                    b.HasOne("TS.EasyStockManager.Data.Entity.Product", "Product")
                        .WithMany("StoreStock")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TS.EasyStockManager.Data.Entity.Store", "Store")
                        .WithMany("StoreStock")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Transaction", b =>
                {
                    b.HasOne("TS.EasyStockManager.Data.Entity.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TS.EasyStockManager.Data.Entity.Store", "ToStore")
                        .WithMany()
                        .HasForeignKey("ToStoreId");

                    b.HasOne("TS.EasyStockManager.Data.Entity.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("ToStore");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.TransactionDetail", b =>
                {
                    b.HasOne("TS.EasyStockManager.Data.Entity.Product", "Product")
                        .WithMany("TransactionDetail")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TS.EasyStockManager.Data.Entity.Transaction", "Transaction")
                        .WithMany("TransactionDetail")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Category", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Product", b =>
                {
                    b.Navigation("StoreStock");

                    b.Navigation("TransactionDetail");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Store", b =>
                {
                    b.Navigation("StoreStock");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.Transaction", b =>
                {
                    b.Navigation("TransactionDetail");
                });

            modelBuilder.Entity("TS.EasyStockManager.Data.Entity.UnitOfMeasure", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
