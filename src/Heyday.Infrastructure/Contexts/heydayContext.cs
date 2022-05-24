using System;
using System.Collections.Generic;
using Heyday.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Heyday.Infrastructure.Contexts
{
    public partial class heydayContext : DbContext
    {
        public heydayContext()
        {
        }

        public heydayContext(DbContextOptions<heydayContext> options) : base(options)
        {
        }

        public virtual DbSet<Schedule> schedules => Set<Schedule>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=heyday;Username=local_user;Password=*****");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.start_date).HasColumnType("timestamp without time zone");
                entity.Property(e => e.suitable_hours).HasColumnType("jsonb");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
