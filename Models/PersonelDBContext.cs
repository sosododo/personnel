using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace personnel.Models
{
    public partial class PersonelDBContext : DbContext
    {
        public PersonelDBContext()
        {
        }

        public PersonelDBContext(DbContextOptions<PersonelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bonuse> Bonuses { get; set; }
        public virtual DbSet<Decision> Decisions { get; set; }
        public virtual DbSet<Delegating> Delegatings { get; set; }
        public virtual DbSet<FunctionalChange> FunctionalChanges { get; set; }
        public virtual DbSet<Punishment> Punishments { get; set; }
        public virtual DbSet<Rest> Rests { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<Secondment> Secondments { get; set; }
        public virtual DbSet<SelfCard> SelfCards { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 
               
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bonuse>(entity =>
            {
                entity.HasKey(e => new { e.DecisionId, e.PersonId });

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.Bonuses)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_bonus_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Bonuses)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bonuses_selfcards");
            });

            modelBuilder.Entity<Decision>(entity =>
            {
                entity.HasKey(e => e.DecisionId)
                    .HasName("PK_decision");
            });

            modelBuilder.Entity<Delegating>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.DecisionId })
                    .HasName("PK_delegating");

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.Delegatings)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_delegatings_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Delegatings)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_delegatings_selfcards");
            });

            modelBuilder.Entity<FunctionalChange>(entity =>
            {
                entity.HasKey(e => new { e.DecisionId, e.PersonId });

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.FunctionalChanges)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_functional_changes_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.FunctionalChanges)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_functional_changes_selfcards");
            });

            modelBuilder.Entity<Punishment>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.DecisionId })
                    .HasName("PK_punishment");

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_punishments_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_punishments_selfcards");
            });

            modelBuilder.Entity<Rest>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.DecisionId })
                    .HasName("PK_rest");

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.Rests)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_rests_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Rests)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_rests_selfcards");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => new { e.DecisionId, e.PersonId })
                    .HasName("PK_reward");
            });

            modelBuilder.Entity<Secondment>(entity =>
            {
                entity.HasKey(e => new { e.DecisionId, e.PersonId })
                    .HasName("PK_secondment");

                entity.HasOne(d => d.Decision)
                    .WithMany(p => p.Secondments)
                    .HasForeignKey(d => d.DecisionId)
                    .HasConstraintName("FK_secondments_decisions");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Secondments)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_secondments_selfcards");
            });

            modelBuilder.Entity<SelfCard>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK_self_card");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
