﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sippedes.Cores.Database;

#nullable disable

namespace sippedes.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230201072948_otp")]
    partial class otp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("sippedes.Cores.Entities.CivilData", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("birthdate");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("blood_type");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("city");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("district");

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("education");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("fullname");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("gender");

                    b.Property<string>("NoKK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("no_kk");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("province");

                    b.Property<string>("Religion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("religion");

                    b.Property<string>("Village")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("village");

                    b.HasKey("NIK");

                    b.ToTable("m_civil_data");
                });

            modelBuilder.Entity("sippedes.Cores.Entities.Otp", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<short>("IsExpired")
                        .HasColumnType("smallint")
                        .HasColumnName("is_expired");

                    b.Property<DateTime>("LastExpiration")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_expiration");

                    b.Property<int>("OtpCode")
                        .HasColumnType("int")
                        .HasColumnName("otp_code");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("m_otp");
                });

            modelBuilder.Entity("sippedes.Cores.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("sippedes.Cores.Entities.Otp", b =>
                {
                    b.HasOne("sippedes.Cores.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
