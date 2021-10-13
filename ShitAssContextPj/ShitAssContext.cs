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

        public virtual DbSet<CinematekaTable> CinematekaTables { get; set; }
        

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

            modelBuilder.Entity<CinematekaTable>(entity =>
            {
                entity.ToTable("cinemateka_table");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.KpId)
                    .HasColumnType("int(11)")
                    .HasColumnName("KP_ID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
