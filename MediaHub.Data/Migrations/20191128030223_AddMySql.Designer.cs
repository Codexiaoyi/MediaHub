﻿// <auto-generated />
using System;
using MediaHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaHub.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20191128030223_AddMySql")]
    partial class AddMySql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MediaHub.Model.FileModel", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("CreateDate");

                    b.Property<string>("ExtensionName")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<long>("FileSize");

                    b.HasKey("Id");

                    b.ToTable("FileModels");
                });
#pragma warning restore 612, 618
        }
    }
}