﻿// <auto-generated />
using Labb2LibraryAngular.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Labb2LibraryAngular.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241004122650_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Labb2LibraryAngular.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("BookDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsInStock")
                        .HasColumnType("bit");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BookID");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookID = 101,
                            Author = "F. Scott Fitzgerald",
                            BookDescription = "Lorem Ipsum",
                            Genre = "Fiction",
                            IsInStock = true,
                            PublicationYear = 1925,
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            BookID = 102,
                            Author = "Harper Lee",
                            BookDescription = "Lorem Ipsum",
                            Genre = "Fiction",
                            IsInStock = true,
                            PublicationYear = 1960,
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            BookID = 103,
                            Author = "George Orwell",
                            BookDescription = "Lorem Ipsum",
                            Genre = "Fiction",
                            IsInStock = false,
                            PublicationYear = 1949,
                            Title = "1984"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
