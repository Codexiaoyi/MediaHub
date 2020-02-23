using MediaHub.Model;
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
            modelBuilder.Entity<Album>().HasOne(x => x.User)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<FileModel>().HasOne(x => x.Album)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.AlbumId);
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FileChunk> FileChunks { get; set; }
    }
}
