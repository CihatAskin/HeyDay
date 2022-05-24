using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Heyday.Infrastructure.Entities
{
    public partial class heydayContext : DbContext
    {
        public heydayContext()
        {
        }

        public heydayContext(DbContextOptions<heydayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Schedule> Schedules { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=heyday;Username=postgres;Password=Park%!@1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_time");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.SuitableHours)
                    .HasColumnType("jsonb")
                    .HasColumnName("suitable_hours");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
