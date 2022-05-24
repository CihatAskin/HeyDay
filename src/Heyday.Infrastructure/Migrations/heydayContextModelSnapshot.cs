﻿// <auto-generated />
using System;
using Heyday.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Heyday.Infrastructure.Migrations
{
    [DbContext(typeof(heydayContext))]
    partial class heydayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Heyday.Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("created_by")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("deleted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("deleted_by")
                        .HasColumnType("uuid");

                    b.Property<bool>("is_executed")
                        .HasColumnType("boolean");

                    b.Property<string>("notes")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("period")
                        .HasColumnType("interval");

                    b.Property<DateTime?>("start_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("suitable_hours")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("updated_by")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.ToTable("schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
