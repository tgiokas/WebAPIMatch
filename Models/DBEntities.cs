using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPIMatch.Models
{
    public partial class DBEntities : DbContext
    {
        public DBEntities()
        {
        }

        public DBEntities(DbContextOptions<DBEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<MatchOdd> MatchOdds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer("Server=localhost; Database=WEBAPI_DB;integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasIndex(e => new { e.TeamA, e.TeamB, e.Sport })
                    .HasName("UC_Match")
                    .IsUnique();
            });

            modelBuilder.Entity<MatchOdd>(entity =>
            {
                entity.HasIndex(e => new { e.MatchId, e.Specifier })
                    .HasName("UC_MatchOdds")
                    .IsUnique();

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchOdds)
                    .HasForeignKey(d => d.MatchId)
                    .HasConstraintName("FK_MatchOdds_Match");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
