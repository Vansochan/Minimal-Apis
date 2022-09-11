using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Lib.Data
{
    public class IdentityDbContext : DbContext
    {
        public const string Schema = "identity";

        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("Users", schema: Schema).HasKey("Id").IsClustered();
                x.Property(p => p.UserName).HasColumnType("nvarchar(50)").IsRequired();
                x.Property(p => p.FirstName).HasColumnType("nvarchar(60)").IsRequired();
                x.Property(p => p.LastName).HasColumnType("nvarchar(60)").IsRequired();
                x.Property(p => p.Gender).HasColumnType("varchar(1)").IsRequired();
                x.Property(p => p.Email).HasColumnType("nvarchar(120)").IsRequired();
                x.Property(p => p.PasswordHash).HasColumnType("varchar(max)").IsRequired();
                x.Property(p => p.IsActive).HasColumnType("bit").HasDefaultValue(true);
                x.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("getutcdate()");
                x.Property(p => p.ModifiedAt).IsRequired(false);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}