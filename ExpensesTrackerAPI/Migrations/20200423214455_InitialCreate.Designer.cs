﻿// <auto-generated />
using System;
using ExpensesTrackerAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpensesTrackerAPI.Migrations
{
    [DbContext(typeof(ExpensesContext))]
    [Migration("20200423214455_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpensesTrackerAPI.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ExpenseId");

                    b.HasIndex("ExpenseTypeId");

                    b.ToTable("Expense");

                    b.HasData(
                        new
                        {
                            ExpenseId = 1,
                            Amount = 10.4m,
                            Currency = "GBP",
                            ExpenseTypeId = 1,
                            Recipient = "Alex",
                            TransactionDate = new DateTime(2020, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 2,
                            Amount = 5.6m,
                            Currency = "CHF",
                            ExpenseTypeId = 2,
                            Recipient = "Eliza",
                            TransactionDate = new DateTime(2020, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 3,
                            Amount = 35.88m,
                            Currency = "EUR",
                            ExpenseTypeId = 0,
                            Recipient = "Artemis",
                            TransactionDate = new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 4,
                            Amount = 105.22m,
                            Currency = "GBP",
                            ExpenseTypeId = 1,
                            Recipient = "Thomas",
                            TransactionDate = new DateTime(2020, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 5,
                            Amount = 44.17m,
                            Currency = "JPY",
                            ExpenseTypeId = 1,
                            Recipient = "John",
                            TransactionDate = new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 6,
                            Amount = 25.00m,
                            Currency = "CHF",
                            ExpenseTypeId = 2,
                            Recipient = "Rick",
                            TransactionDate = new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 7,
                            Amount = 182.59m,
                            Currency = "AUD",
                            ExpenseTypeId = 1,
                            Recipient = "Patricia",
                            TransactionDate = new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 8,
                            Amount = 56.16m,
                            Currency = "EUR",
                            ExpenseTypeId = 0,
                            Recipient = "Glen",
                            TransactionDate = new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 9,
                            Amount = 8.25m,
                            Currency = "EUR",
                            ExpenseTypeId = 1,
                            Recipient = "Maria",
                            TransactionDate = new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 10,
                            Amount = 98.99m,
                            Currency = "CHF",
                            ExpenseTypeId = 2,
                            Recipient = "George",
                            TransactionDate = new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 11,
                            Amount = 3.99m,
                            Currency = "USD",
                            ExpenseTypeId = 1,
                            Recipient = "Paul",
                            TransactionDate = new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ExpenseId = 12,
                            Amount = 54.12m,
                            Currency = "EUR",
                            ExpenseTypeId = 0,
                            Recipient = "Caren",
                            TransactionDate = new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ExpensesTrackerAPI.Models.ExpenseType", b =>
                {
                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseTypeId");

                    b.ToTable("ExpenseType");

                    b.HasData(
                        new
                        {
                            ExpenseTypeId = 0,
                            Description = "Other"
                        },
                        new
                        {
                            ExpenseTypeId = 1,
                            Description = "Food"
                        },
                        new
                        {
                            ExpenseTypeId = 2,
                            Description = "Drinks"
                        });
                });

            modelBuilder.Entity("ExpensesTrackerAPI.Models.Expense", b =>
                {
                    b.HasOne("ExpensesTrackerAPI.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
