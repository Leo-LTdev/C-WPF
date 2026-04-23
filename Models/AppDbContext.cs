using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hero> Heroes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Spell> Spells { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hero__3214EC2732DF3C9B");

            entity.ToTable("Hero");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Spells).WithMany(p => p.Heroes)
                .UsingEntity<Dictionary<string, object>>(
                    "HeroSpell",
                    r => r.HasOne<Spell>().WithMany()
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__HeroSpell__Spell__31EC6D26"),
                    l => l.HasOne<Hero>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__HeroSpell__HeroI__30F848ED"),
                    j =>
                    {
                        j.HasKey("HeroId", "SpellId").HasName("PK__HeroSpel__C61DD65A50FB443F");
                        j.ToTable("HeroSpell");
                        j.IndexerProperty<int>("HeroId").HasColumnName("HeroID");
                        j.IndexerProperty<int>("SpellId").HasColumnName("SpellID");
                    });
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3214EC27CFE4F78D");

            entity.ToTable("Login");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC27B197B24F");

            entity.ToTable("Player");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Login).WithMany(p => p.Players)
                .HasForeignKey(d => d.LoginId)
                .HasConstraintName("FK__Player__LoginID__267ABA7A");

            entity.HasMany(d => d.Heroes).WithMany(p => p.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerHero",
                    r => r.HasOne<Hero>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerHer__HeroI__2E1BDC42"),
                    l => l.HasOne<Player>().WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerHer__Playe__2D27B809"),
                    j =>
                    {
                        j.HasKey("PlayerId", "HeroId").HasName("PK__PlayerHe__697D178C3F0AC2D9");
                        j.ToTable("PlayerHero");
                        j.IndexerProperty<int>("PlayerId").HasColumnName("PlayerID");
                        j.IndexerProperty<int>("HeroId").HasColumnName("HeroID");
                    });
        });

        modelBuilder.Entity<Spell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spell__3214EC2742D9E2D4");

            entity.ToTable("Spell");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
