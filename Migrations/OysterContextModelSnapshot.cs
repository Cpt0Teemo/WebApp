﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(OysterContext))]
    partial class OysterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp.Models.Order", b =>
                {
                    b.Property<Guid>("orderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("comment");

                    b.Property<DateTime>("createdOn");

                    b.Property<DateTime?>("done");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<DateTime>("expectedDate");

                    b.Property<string>("name")
                        .IsRequired();

                    b.Property<byte[]>("timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("orderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApp.Models.OysterTypePrice", b =>
                {
                    b.Property<int>("oysterType");

                    b.Property<decimal>("price");

                    b.HasKey("oysterType");

                    b.ToTable("OysterTypePrices");
                });

            modelBuilder.Entity("WebApp.Models.SubOrder", b =>
                {
                    b.Property<Guid>("subOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("orderId");

                    b.Property<int>("oysterType");

                    b.Property<int>("quantity");

                    b.HasKey("subOrderId");

                    b.HasIndex("orderId");

                    b.ToTable("SubOrders");
                });

            modelBuilder.Entity("WebApp.Models.SubOrder", b =>
                {
                    b.HasOne("WebApp.Models.Order")
                        .WithMany("subOrders")
                        .HasForeignKey("orderId");
                });
#pragma warning restore 612, 618
        }
    }
}
