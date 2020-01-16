﻿// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200105124844_initial_migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEndAPI.Data.CDDVD", b =>
                {
                    b.Property<Guid>("CDDVDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SCId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CDDVDId");

                    b.HasIndex("SCId");

                    b.ToTable("CDVDs");
                });

            modelBuilder.Entity("BackEndAPI.Data.Network", b =>
                {
                    b.Property<Guid>("NetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstanceID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VMId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NetId");

                    b.HasIndex("VMId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("BackEndAPI.Data.SC", b =>
                {
                    b.Property<Guid>("SCId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VMId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SCId");

                    b.HasIndex("VMId");

                    b.ToTable("SCs");
                });

            modelBuilder.Entity("BackEndAPI.Data.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PositionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Workplace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BackEndAPI.Data.VHD", b =>
                {
                    b.Property<Guid>("VHDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SCId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("VHDId");

                    b.HasIndex("SCId");

                    b.ToTable("VHDs");
                });

            modelBuilder.Entity("BackEndAPI.Data.VM", b =>
                {
                    b.Property<Guid>("VMId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Configuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cores")
                        .HasColumnType("int");

                    b.Property<string>("LastSave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RAM")
                        .HasColumnType("int");

                    b.Property<string>("RealID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VMId");

                    b.HasIndex("UserId");

                    b.ToTable("VMs");
                });

            modelBuilder.Entity("BackEndAPI.Data.CDDVD", b =>
                {
                    b.HasOne("BackEndAPI.Data.SC", null)
                        .WithMany("CDDVDs")
                        .HasForeignKey("SCId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEndAPI.Data.Network", b =>
                {
                    b.HasOne("BackEndAPI.Data.VM", null)
                        .WithMany("Networks")
                        .HasForeignKey("VMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEndAPI.Data.SC", b =>
                {
                    b.HasOne("BackEndAPI.Data.VM", null)
                        .WithMany("SCs")
                        .HasForeignKey("VMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEndAPI.Data.VHD", b =>
                {
                    b.HasOne("BackEndAPI.Data.SC", null)
                        .WithMany("VHDs")
                        .HasForeignKey("SCId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEndAPI.Data.VM", b =>
                {
                    b.HasOne("BackEndAPI.Data.User", null)
                        .WithMany("VMS")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
