using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OriginBanking.Data.Models
{
    public partial class OriginBankingContext : DbContext
    {
        public OriginBankingContext()
        {
        }

        public OriginBankingContext(DbContextOptions<OriginBankingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Operations> Operations { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-7E4EHNL;Initial Catalog=OriginBanking;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.HasIndex(e => e.Number)
                    .HasName("I_Cards_Number")
                    .IsUnique();

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Pin).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.HasIndex(e => e.OperationId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Cardnumber).HasColumnName("cardnumber");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.OperationId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Operations>(entity =>
            {
                entity.HasKey(e => e.OperationId);

                entity.HasIndex(e => e.Description)
                    .HasName("I_Operations_Description")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });
        }
    }
}
