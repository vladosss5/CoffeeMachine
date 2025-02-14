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
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoffeeMachine.Core.Models.Banknote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<int>("Nominal")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("money_pk");

                    b.ToTable("Banknotes");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.BanknoteToMachine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<long>("BanknoteId")
                        .HasColumnType("bigint");

                    b.Property<int>("CountBanknote")
                        .HasColumnType("integer");

                    b.Property<long>("MachineId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("banknotes_machines_pk");

                    b.HasIndex("BanknoteId");

                    b.HasIndex("MachineId");

                    b.ToTable("BanknotesToMachines");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Coffee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

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

            modelBuilder.Entity("CoffeeMachine.Core.Models.CoffeeToMachine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<long>("CoffeeId")
                        .HasColumnType("bigint");

                    b.Property<long>("MachineId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("coffee_in_machine_pk");

                    b.HasIndex("CoffeeId");

                    b.HasIndex("MachineId");

                    b.ToTable("CoffeesToMachines");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Machine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id")
                        .HasName("machine_pk");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<long>("CoffeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTimeCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("MachineId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id")
                        .HasName("order_pk");

                    b.HasIndex("CoffeeId");

                    b.HasIndex("MachineId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id")
                        .HasName("role_pk");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<long>("BanknoteId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPayment")
                        .HasColumnType("boolean");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("transaction_pk");

                    b.HasIndex("BanknoteId");

                    b.HasIndex("OrderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("user_pk");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.BanknoteToMachine", b =>
                {
                    b.HasOne("CoffeeMachine.Core.Models.Banknote", "Banknote")
                        .WithMany("BanknotesToMachines")
                        .HasForeignKey("BanknoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("banknotes_machines_banknote_fk");

                    b.HasOne("CoffeeMachine.Core.Models.Machine", "Machine")
                        .WithMany("BanknotesToMachines")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("banknotes_machines_machine_fk");

                    b.Navigation("Banknote");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.CoffeeToMachine", b =>
                {
                    b.HasOne("CoffeeMachine.Core.Models.Coffee", "Coffee")
                        .WithMany("CoffeesToMachines")
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("coffee_in_machine_coffee_fk");

                    b.HasOne("CoffeeMachine.Core.Models.Machine", "Machine")
                        .WithMany("CoffeesToMachines")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("coffee_in_machine_machine_fk");

                    b.Navigation("Coffee");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Order", b =>
                {
                    b.HasOne("CoffeeMachine.Core.Models.Coffee", "Coffee")
                        .WithMany("Orders")
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("order_coffee_fk");

                    b.HasOne("CoffeeMachine.Core.Models.Machine", "Machine")
                        .WithMany("Orders")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("order_machine_fk");

                    b.Navigation("Coffee");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Transaction", b =>
                {
                    b.HasOne("CoffeeMachine.Core.Models.Banknote", "Banknote")
                        .WithMany("Transactions")
                        .HasForeignKey("BanknoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transaction_banknote_fk");

                    b.HasOne("CoffeeMachine.Core.Models.Order", "Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transaction_order_fk");

                    b.Navigation("Banknote");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.User", b =>
                {
                    b.HasOne("CoffeeMachine.Core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("user_role_fk");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Banknote", b =>
                {
                    b.Navigation("BanknotesToMachines");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Coffee", b =>
                {
                    b.Navigation("CoffeesToMachines");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Machine", b =>
                {
                    b.Navigation("BanknotesToMachines");

                    b.Navigation("CoffeesToMachines");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Order", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CoffeeMachine.Core.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
