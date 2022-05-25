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
        public virtual DbSet<User> users => Set<User>();
        public virtual DbSet<UserSchedule> user_schedule => Set<UserSchedule>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=heyday;Username=local_user;Password=local_user+-2020*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.schedules).WithOne(x => x.user).HasForeignKey(x => x.manager_id);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.start_date).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<UserSchedule>(entity =>
            {
                entity.HasKey(us => new { us.user_id, us.schedule_id });
                entity.Property(e => e.suitable_hours).HasColumnType("jsonb");
            });

            modelBuilder.Entity<UserSchedule>()
                        .HasOne(us => us.schedule)
                        .WithMany(s => s.user_schedule)
                        .HasForeignKey(us => us.schedule_id);


            modelBuilder.Entity<UserSchedule>()
                        .HasOne(us => us.user)
                        .WithMany(u => u.user_schedule)
                        .HasForeignKey(us => us.user_id);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
