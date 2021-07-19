using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cinemateka
{
    public partial class ShitAssContext : DbContext
    {
        public ShitAssContext()
        {
        }

        public ShitAssContext(DbContextOptions<ShitAssContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TableCinemateka> TableCinematekas { get; set; }
        public virtual DbSet<TableUser> TableUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=mysql60.hostland.ru;database=host1323541_vrn05;uid=host1323541_itstep;pwd=269f43dc", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<TableCinemateka>(entity =>
            {
                entity.ToTable("table_cinemateka");

                entity.HasIndex(e => e.MovieTitle, "table_cinemateka_Movie_Title_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("director");

                entity.Property(e => e.LeadActor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("lead_actor");

                entity.Property(e => e.MovieTitle)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("movie_title");

                entity.Property(e => e.Year)
                    .HasColumnType("int(11)")
                    .HasColumnName("year");
            });

            modelBuilder.Entity<TableUser>(entity =>
            {
                entity.ToTable("table_user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
