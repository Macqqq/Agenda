using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using POIRE.MusicDB;

namespace POIRE.AgendaMb; // Updated namespace to reflect the new context name

public partial class AgendaMbContext : DbContext // Updated class name to AgendaMbContext
{
    public AgendaMbContext()
    {
    }

    public AgendaMbContext(DbContextOptions<AgendaMbContext> options) // Updated to use the new class name
        : base(options)
    {
    }

    public virtual DbSet<Contactsocialmedium> Contactsocialmedia { get; set; }
    public virtual DbSet<Contactstable> Contactstables { get; set; }
    public virtual DbSet<Evenement> Evenements { get; set; }
    public virtual DbSet<Note> Notes { get; set; }
    public virtual DbSet<Socialmedium> Socialmedia { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<TaskList> TaskLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Warning comment updated to suggest moving connection string out of source code
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        // Updated to use the 'agenda_mb' database
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=bdd2", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Contactsocialmedium>(entity =>
        {
            entity.HasKey(e => new { e.IdContactSocialMedia, e.SocialMediaIdSocialMedia, e.SocialMediaContactstableIdContactstable, e.ContactstableIdContactstable })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("contactsocialmedia");

            entity.HasIndex(e => e.ContactstableIdContactstable, "fk_ContactSocialMedia_Contactstable1_idx");

            entity.HasIndex(e => new { e.SocialMediaIdSocialMedia, e.SocialMediaContactstableIdContactstable }, "fk_ContactSocialMedia_SocialMedia1_idx");

            entity.Property(e => e.IdContactSocialMedia).HasColumnName("idContactSocialMedia");
            entity.Property(e => e.SocialMediaIdSocialMedia).HasColumnName("SocialMedia_idSocialMedia");
            entity.Property(e => e.SocialMediaContactstableIdContactstable).HasColumnName("SocialMedia_Contactstable_idContactstable");
            entity.Property(e => e.ContactstableIdContactstable).HasColumnName("Contactstable_idContactstable");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("URL");
            entity.Property(e => e.Username).HasMaxLength(45);
        });

        modelBuilder.Entity<Contactstable>(entity =>
        {
            entity.HasKey(e => e.IdContactstable).HasName("PRIMARY");

            entity.ToTable("contactstable");

            entity.Property(e => e.IdContactstable)
                .ValueGeneratedNever()
                .HasColumnName("idContactstable");
            entity.Property(e => e.Adresse).HasMaxLength(70);
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Prenom).HasMaxLength(45);
            entity.Property(e => e.Ville).HasMaxLength(45);
        });

        modelBuilder.Entity<Evenement>(entity =>
        {
            entity.HasKey(e => new { e.IdEvenements, e.ContactstableIdContactstable })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("evenements");

            entity.HasIndex(e => e.ContactstableIdContactstable, "fk_Evenements_Contactstable1_idx");

            entity.Property(e => e.IdEvenements).HasColumnName("idEvenements");
            entity.Property(e => e.ContactstableIdContactstable).HasColumnName("Contactstable_idContactstable");
            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Titre).HasMaxLength(45);

            entity.HasOne(d => d.ContactstableIdContactstableNavigation).WithMany(p => p.Evenements)
                .HasForeignKey(d => d.ContactstableIdContactstable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Evenements_Contactstable1");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => new { e.IdNotes, e.ContactstableIdContactstable })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("notes");

            entity.HasIndex(e => e.ContactstableIdContactstable, "fk_Notes_Contactstable1_idx");

            entity.Property(e => e.IdNotes).HasColumnName("idNotes");
            entity.Property(e => e.ContactstableIdContactstable).HasColumnName("Contactstable_idContactstable");
            entity.Property(e => e.Contenu).HasColumnType("text");
            entity.Property(e => e.DateCreation).HasColumnType("datetime");

            entity.HasOne(d => d.ContactstableIdContactstableNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.ContactstableIdContactstable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notes_Contactstable1");
        });

        modelBuilder.Entity<Socialmedium>(entity =>
        {
            entity.HasKey(e => new { e.IdSocialMedia, e.ContactstableIdContactstable })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("socialmedia");

            entity.HasIndex(e => e.ContactstableIdContactstable, "fk_SocialMedia_Contactstable_idx");

            entity.Property(e => e.IdSocialMedia).HasColumnName("idSocialMedia");
            entity.Property(e => e.ContactstableIdContactstable).HasColumnName("Contactstable_idContactstable");
            entity.Property(e => e.Discord).HasMaxLength(20);
            entity.Property(e => e.Facebook).HasMaxLength(20);
            entity.Property(e => e.Instagram).HasMaxLength(20);
            entity.Property(e => e.Tiktok).HasMaxLength(20);
            entity.Property(e => e.Twitch).HasMaxLength(20);
            entity.Property(e => e.XTwitter)
                .HasMaxLength(20)
                .HasColumnName("X (Twitter)");
            entity.Property(e => e.Youtube).HasMaxLength(20);

            entity.HasOne(d => d.ContactstableIdContactstableNavigation).WithMany(p => p.Socialmedia)
                .HasForeignKey(d => d.ContactstableIdContactstable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SocialMedia_Contactstable");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.TaskListIdTaskList, "fk_Tasks_TaskLists_idx");

            entity.Property(e => e.IdTask).HasColumnName("idTask");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsCompleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.TaskContent).HasColumnType("text");
            entity.Property(e => e.TaskListIdTaskList).HasColumnName("TaskList_idTaskList");

            entity.HasOne(d => d.TaskListIdTaskListNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskListIdTaskList)
                .HasConstraintName("fk_Tasks_TaskLists");
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(e => e.IdTaskList).HasName("PRIMARY");

            entity.ToTable("task_lists");

            entity.Property(e => e.IdTaskList).HasColumnName("idTaskList");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
