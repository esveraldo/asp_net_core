using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities.User;
using Api.Data.Mapping;
using System;

namespace Api.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = "Esveraldo M. de Oliveira",
                    Email = "esveraldo@hotmail.com",
                    Senha = "123456",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        
    }
}