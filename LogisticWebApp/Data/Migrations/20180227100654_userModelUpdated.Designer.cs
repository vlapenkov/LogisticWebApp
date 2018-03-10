﻿// <auto-generated />
using Logistic.Data;
using LogisticWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LogisticWebApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180227100654_userModelUpdated")]
    partial class userModelUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Logistic.Data.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CarrierId")
                        .HasMaxLength(7);

                    b.Property<int?>("DriverId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("StateNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("DriverId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Logistic.Data.Carrier", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(7);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Kpp")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("Logistic.Data.ClaimForTransport", b =>
                {
                    b.Property<Guid>("GuidIn1S");

                    b.Property<string>("CarrierId")
                        .HasMaxLength(7);

                    b.Property<string>("Comments");

                    b.Property<DateTime?>("DeadlineDate");

                    b.Property<DateTime>("DocDate");

                    b.Property<string>("NumberIn1S")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<DateTime?>("ReadyDate");

                    b.Property<int>("Status");

                    b.Property<int>("Volume");

                    b.HasKey("GuidIn1S");

                    b.HasIndex("CarrierId");

                    b.ToTable("ClaimsForTransport");
                });

            modelBuilder.Entity("Logistic.Data.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarrierId")
                        .HasMaxLength(7);

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("HasContract");

                    b.Property<bool>("IsActive");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Logistic.Data.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<string>("Username")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Logistic.Data.ReplyToClaim", b =>
                {
                    b.Property<Guid>("GuidOfClaimIn1S");

                    b.Property<string>("CarrierId")
                        .HasMaxLength(7);

                    b.Property<DateTime?>("ArrivalDate");

                    b.Property<int?>("CarId");

                    b.Property<decimal>("Cost");

                    b.Property<int?>("DriverId");

                    b.Property<DateTime?>("UnloadDate");

                    b.HasKey("GuidOfClaimIn1S", "CarrierId");

                    b.HasAlternateKey("CarrierId", "GuidOfClaimIn1S");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.ToTable("RepliesToClaims");
                });

            modelBuilder.Entity("LogisticWebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CarrierId")
                        .HasMaxLength(7);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Fio")
                        .HasMaxLength(100);

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Kpp")
                        .HasMaxLength(9);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Logistic.Data.Car", b =>
                {
                    b.HasOne("Logistic.Data.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");

                    b.HasOne("Logistic.Data.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");
                });

            modelBuilder.Entity("Logistic.Data.ClaimForTransport", b =>
                {
                    b.HasOne("Logistic.Data.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("Logistic.Data.Driver", b =>
                {
                    b.HasOne("Logistic.Data.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("Logistic.Data.ReplyToClaim", b =>
                {
                    b.HasOne("Logistic.Data.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("Logistic.Data.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Logistic.Data.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("Logistic.Data.ClaimForTransport", "Claim")
                        .WithMany("Replies")
                        .HasForeignKey("GuidOfClaimIn1S")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LogisticWebApp.Models.ApplicationUser", b =>
                {
                    b.HasOne("Logistic.Data.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LogisticWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LogisticWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LogisticWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LogisticWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
