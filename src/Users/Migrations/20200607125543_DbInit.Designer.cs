﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.API.Models.Context;

namespace Users.API.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20200607125543_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Users.API.Models.RP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Fio")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RPs");
                });

            modelBuilder.Entity("Users.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Fio")
                        .HasColumnType("text");

                    b.Property<int>("RpId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RpId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Users.API.Models.User", b =>
                {
                    b.HasOne("Users.API.Models.RP", "RP")
                        .WithMany("User")
                        .HasForeignKey("RpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
