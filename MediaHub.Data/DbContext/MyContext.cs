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
            modelBuilder.Entity<TestModel>().HasData(
                    new TestModel()
                    {
                        TestName = "111"
                    },
                    new TestModel()
                    {
                        TestName = "222"
                    },
                    new TestModel()
                    {
                        TestName = "333"
                    },
                    new TestModel()
                    {
                        TestName = "444"
                    }
                );
        }

        public DbSet<TestModel> TestModels { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
    }
}
