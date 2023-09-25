﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Premisson.Northwind.Data.Acces.Concreate.EntityFramework;

namespace Premisson.Northwind.Data.Acces.Migrations
{
    [DbContext(typeof(NorthwindContext))]
    partial class NorthwindContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.Dayoff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayoffDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DayoffTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Dayoff_Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsApprove")
                        .HasColumnType("bit");

                    b.Property<int>("ProxyUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayoffTypeId");

                    b.HasIndex("ProxyUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Dayoffs");
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.DayoffType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DayoffTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            Name = "Yıllık İzin"
                        },
                        new
                        {
                            Id = 2,
                            IsDelete = false,
                            Name = "Hastalık İzin"
                        },
                        new
                        {
                            Id = 3,
                            IsDelete = false,
                            Name = "Mazeret İzin"
                        });
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.Deparment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Deparments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(1326),
                            IsDelete = false,
                            Name = "Muhasebe Birimi"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(9303),
                            IsDelete = false,
                            Name = "Yazılım Birimi"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(9332),
                            IsDelete = false,
                            Name = "Satış Birimi"
                        });
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            IsDelete = false,
                            Name = "Yönetici"
                        },
                        new
                        {
                            Id = 3,
                            IsDelete = false,
                            Name = "Personel"
                        });
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@corporate.com",
                            IsActive = true,
                            IsDelete = false,
                            Name = "Admin",
                            Password = "1234",
                            RoleId = 1,
                            Surname = "Admin"
                        });
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.UserDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsManager")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDepartments");
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.Dayoff", b =>
                {
                    b.HasOne("Premisson.Northwind.Entities.Concreate.DayoffType", "DayoffType")
                        .WithMany("Dayoffs")
                        .HasForeignKey("DayoffTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Premisson.Northwind.Entities.Concreate.User", "ProxyUser")
                        .WithMany()
                        .HasForeignKey("ProxyUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Premisson.Northwind.Entities.Concreate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.User", b =>
                {
                    b.HasOne("Premisson.Northwind.Entities.Concreate.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Premisson.Northwind.Entities.Concreate.UserDepartment", b =>
                {
                    b.HasOne("Premisson.Northwind.Entities.Concreate.Deparment", "Deparment")
                        .WithMany("UserDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Premisson.Northwind.Entities.Concreate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
