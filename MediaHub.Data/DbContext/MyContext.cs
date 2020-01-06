﻿using MediaHub.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaHub.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<FileModel> Files { get; set; }
        public DbSet<MediaHubUser> MediaHubUsers { get; set; }
        public DbSet<FileChunk> FileChunks { get; set; }
    }
}
