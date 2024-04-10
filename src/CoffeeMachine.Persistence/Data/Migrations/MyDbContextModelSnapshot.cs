﻿// <auto-generated />
using System;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeMachine.Persistence.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Banknote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("Par")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("money_pk");

                    b.ToTable("Banknotes");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.BanknotesMachine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CountBanknotes")
                        .HasColumnType("integer");

                    b.Property<int>("IdBanknote")
                        .HasColumnType("integer");

                    b.Property<int>("IdMachine")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("banknotes_machines_pk");

                    b.HasIndex("IdBanknote");

                    b.HasIndex("IdMachine");

                    b.ToTable("BanknotesMachines");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Coffee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("coffee_pk");

                    b.ToTable("Coffees");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id")
                        .HasName("machine_pk");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdCoffee")
                        .HasColumnType("integer");

                    b.Property<int>("IdMachine")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id")
                        .HasName("purchase_pk");

                    b.HasIndex("IdCoffee");

                    b.HasIndex("IdMachine");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CountBanknotes")
                        .HasColumnType("integer");

                    b.Property<int>("IdBanknote")
                        .HasColumnType("integer");

                    b.Property<int>("IdPurchase")
                        .HasColumnType("integer");

                    b.Property<bool>("Type")
                        .HasColumnType("boolean");

                    b.HasKey("Id")
                        .HasName("transaction_pk");

                    b.HasIndex("IdBanknote");

                    b.HasIndex("IdPurchase");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.BanknotesMachine", b =>
                {
                    b.HasOne("CoffeeMachine.Domain.Models.Banknote", "Banknote")
                        .WithMany("BanknotesMachines")
                        .HasForeignKey("IdBanknote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("banknotes_machines_banknote_fk");

                    b.HasOne("CoffeeMachine.Domain.Models.Machine", "Machine")
                        .WithMany("BanknotesMachines")
                        .HasForeignKey("IdMachine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("banknotes_machines_machine_fk");

                    b.Navigation("Banknote");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Purchase", b =>
                {
                    b.HasOne("CoffeeMachine.Domain.Models.Coffee", "Coffee")
                        .WithMany("Purchases")
                        .HasForeignKey("IdCoffee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("purchase_coffee_fk");

                    b.HasOne("CoffeeMachine.Domain.Models.Machine", "Machine")
                        .WithMany("Purchases")
                        .HasForeignKey("IdMachine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("purchase_machine_fk");

                    b.Navigation("Coffee");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Transaction", b =>
                {
                    b.HasOne("CoffeeMachine.Domain.Models.Banknote", "Banknote")
                        .WithMany("Transactions")
                        .HasForeignKey("IdBanknote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transaction_banknote_fk");

                    b.HasOne("CoffeeMachine.Domain.Models.Purchase", "Purchase")
                        .WithMany("Transactions")
                        .HasForeignKey("IdPurchase")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transaction_purchase_fk");

                    b.Navigation("Banknote");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Banknote", b =>
                {
                    b.Navigation("BanknotesMachines");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Coffee", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Machine", b =>
                {
                    b.Navigation("BanknotesMachines");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CoffeeMachine.Domain.Models.Purchase", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
