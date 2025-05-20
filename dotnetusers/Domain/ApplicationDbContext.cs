using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Domain;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public  DbSet<Track> Tracks { get; set; }
    public  DbSet<TrackKeys> TrackKeys { get; set; }
    public  DbSet<Genre> Genres { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dotnetuser;Username=postgres;Password=password;SSL Mode=Prefer;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasMany(d => d.Roles).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "Usuariorole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("Roleid")
                        .HasConstraintName("usuarioroles_roleid_fkey"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("Usuarioid")
                        .HasConstraintName("usuarioroles_usuarioid_fkey"),
                    j =>
                    {
                        j.HasKey("Usuarioid", "Roleid").HasName("usuarioroles_pkey");
                        j.ToTable("usuarioroles");
                        j.HasIndex(new[] { "Roleid" }, "ix_usuarioroles_roleid");
                        j.IndexerProperty<int>("Usuarioid").HasColumnName("usuarioid");
                        j.IndexerProperty<int>("Roleid").HasColumnName("roleid");
                    });
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("tracks"); // Nome da tabela

            entity.HasOne(t => t.Usuario)
                  .WithMany(u => u.UserTracks) // Nome da coleção em Usuario
                  .HasForeignKey(t => t.UsuarioId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade); // Se o usuário for deletado, suas tracks também são (ou .Restrict)            

            // --- Relação Um-para-Muitos: MusicalKey e Track ---
            // (Um MusicalKey pode estar em muitas Tracks, uma Track tem um MusicalKey)
            entity.HasOne(t => t.Key) // Track tem um MusicalKey (opcional)
                  .WithMany(k => k.TracksInThisKey)   // MusicalKey tem muitas Tracks
                  .HasForeignKey(t => t.KeyId) // A FK em Track é MusicalKeyId
                  .IsRequired(false) // Torna a relação opcional
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(t => t.Genres) // Track tem muitos Genres
              .WithMany(g => g.Tracks) // Genre tem muitas Tracks
              .UsingEntity<Dictionary<string, object>>(
                  "TrackGenres", // Nome da entidade de junção para o EF Core
                  j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.Cascade),
                  j => j.HasOne<Track>().WithMany().HasForeignKey("TrackId").OnDelete(DeleteBehavior.Cascade),
                  j =>
                  {
                      j.HasKey("TrackId", "GenreId"); // Chave primária composta
                      j.ToTable("track_genres"); // NOME DA TABELA DE JUNÇÃO NO BANCO DE DADOS
                  });// Se um MusicalKey for deletado, o MusicalKeyId nas tracks vira NULL
        });

        modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");
                entity.HasIndex(g => g.Nome).IsUnique();
                // Seed de Genres (opcional)
                // entity.HasData(new Genre { Id = 1, Nome = "Rock" }, ...);
            });

            modelBuilder.Entity<TrackKeys>(entity =>
            {
                entity.ToTable("track_keys");
                entity.HasIndex(k => k.Nome).IsUnique();
                // Seed de MusicalKeys (opcional)
                // entity.HasData(new MusicalKey { Id = 1, Nome = "Cmaj" }, ...);
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
