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
    [Migration("20200114145817_ModifyUserTable")]
    partial class ModifyUserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MediaHub.Model.FileChunk", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<int>("ChunkNumber");

                    b.Property<long>("ChunkSize");

                    b.Property<string>("CreateDate");

                    b.Property<long>("CurrentChunkSize");

                    b.Property<string>("Filename")
                        .IsRequired();

                    b.Property<string>("Identifier")
                        .IsRequired();

                    b.Property<string>("RelativePath")
                        .IsRequired();

                    b.Property<int>("TotalChunks");

                    b.Property<long>("TotalSize");

                    b.HasKey("Id");

                    b.ToTable("FileChunks");
                });

            modelBuilder.Entity("MediaHub.Model.FileModel", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("CreateDate");

                    b.Property<string>("ExtensionName");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<long>("FileSize");

                    b.Property<string>("Identifier");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("MediaHub.Model.MediaHubUser", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<int>("Gender");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserAccount")
                        .IsRequired();

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MediaHubUsers");
                });
#pragma warning restore 612, 618
        }
    }
}