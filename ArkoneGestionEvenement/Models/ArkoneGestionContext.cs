using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArkoneGestionEvenement.Models;

public partial class ArkoneGestionContext : DbContext
{
    public ArkoneGestionContext()
    {
    }

    public ArkoneGestionContext(DbContextOptions<ArkoneGestionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CodesAcce> CodesAcces { get; set; }

    public virtual DbSet<Evenement> Evenements { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<SousEvenement> SousEvenements { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.29.13,1433\\MSSQLSERVER;User ID=sa;Password=erty64%;Database=ArkoneGestion;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CodesAcce>(entity =>
        {
            entity.HasKey(e => e.IdCodeAcces).HasName("PK__CodesAcc__0AA78C88E7CB4C4F");

            entity.Property(e => e.Code).HasMaxLength(6);
            entity.Property(e => e.Statut).HasMaxLength(20);

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.CodesAcces)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("FK__CodesAcce__IdEve__2C3393D0");

            entity.HasOne(d => d.IdInviteNavigation).WithMany(p => p.CodesAcces)
                .HasForeignKey(d => d.IdInvite)
                .HasConstraintName("FK__CodesAcce__IdInv__2B3F6F97");
        });

        modelBuilder.Entity<Evenement>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PK__Evenemen__E0B2AF396A12E788");

            entity.ToTable("Evenement");

            entity.Property(e => e.IdEvent).ValueGeneratedNever();
            entity.Property(e => e.DateEvenement).HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasMaxLength(255);
            entity.Property(e => e.Lieu).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(255);

            entity.HasOne(d => d.IdOrganisateurNavigation).WithMany(p => p.Evenements)
                .HasForeignKey(d => d.IdOrganisateur)
                .HasConstraintName("FK__Evenement__IdOrg__267ABA7A");
        });

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.IdInvite).HasName("PK__Invite__22B0C94EBC18E359");

            entity.ToTable("Invite");

            entity.Property(e => e.IdInvite).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(255);
            entity.Property(e => e.Prenom).HasMaxLength(255);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<SousEvenement>(entity =>
        {
            entity.HasKey(e => e.IdSousEvent).HasName("PK__SousEven__A5C86BE62CB79868");

            entity.ToTable("SousEvenement");

            entity.Property(e => e.IdSousEvent).ValueGeneratedNever();
            entity.Property(e => e.DateHeure).HasColumnType("datetime");
            entity.Property(e => e.NomSousEvent).HasMaxLength(255);

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.SousEvenements)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("FK__SousEvene__IdEve__2F10007B");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("PK__Utilisat__45A4C15779500DCA");

            entity.ToTable("Utilisateur");

            entity.Property(e => e.IdUtilisateur).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.MotDePasse).HasMaxLength(255);
            entity.Property(e => e.NomUtilisateur).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
