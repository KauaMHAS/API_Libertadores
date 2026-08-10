using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLibertadoresHAS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApiLibertadoresHAS.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Time> TB_TIMES { get; set; }

        public DbSet<Jogador> TB_JOGADORES { get; set; }

        public DbSet<Posicao> TB_POSICOES { get; set; }

        public DbSet<Estadio> TB_ESTADIOS { get; set; }

        public DbSet<Rodada> TB_RODADAS { get; set; }

        public DbSet<Partida> TB_PARTIDAS { get; set; }

        public DbSet<PartidaTime> TB_PARTIDAS_TIMES { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            #region TB_TIMES

            modelBuilder.Entity<Time>(entity =>
            {
                entity.ToTable("TB_TIMES");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Escudo)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Jogadores)
                    .WithOne(e => e.Time)
                    .HasForeignKey(e => e.TimeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.PartidaTimes)
                    .WithOne(e => e.Time)
                    .HasForeignKey(e => e.TimeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region TB_POSICOES

            modelBuilder.Entity<Posicao>(entity =>
            {
                entity.ToTable("TB_POSICOES");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasMany(e => e.Jogadores)
                    .WithOne(e => e.Posicao)
                    .HasForeignKey(e => e.PosicaoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region TB_JOGADORES

            modelBuilder.Entity<Jogador>(entity =>
            {
                entity.ToTable("TB_JOGADORES");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .HasMaxLength(120)
                    .IsRequired();

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date");
            });

            #endregion

            #region TB_ESTADIOS

            modelBuilder.Entity<Estadio>(entity =>
            {
                entity.ToTable("TB_ESTADIOS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .HasMaxLength(120)
                    .IsRequired();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasMany(e => e.Partidas)
                    .WithOne(e => e.Estadio)
                    .HasForeignKey(e => e.EstadioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region TB_RODADAS

            modelBuilder.Entity<Rodada>(entity =>
            {
                entity.ToTable("TB_RODADAS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasMany(e => e.Partidas)
                    .WithOne(e => e.Rodada)
                    .HasForeignKey(e => e.RodadaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region TB_PARTIDAS

            modelBuilder.Entity<Partida>(entity =>
            {
                entity.ToTable("TB_PARTIDAS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.DataHora)
                    .HasColumnType("datetime");

                entity.HasMany(e => e.PartidaTimes)
                    .WithOne(e => e.Partida)
                    .HasForeignKey(e => e.PartidaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region TB_PARTIDAS_TIMES

            modelBuilder.Entity<PartidaTime>(entity =>
            {
                entity.ToTable("TB_PARTIDAS_TIMES");

                entity.HasKey(e => new
                {
                    e.PartidaId,
                    e.TimeId
                });

                entity.HasOne(e => e.Partida)
                    .WithMany(e => e.PartidaTimes)
                    .HasForeignKey(e => e.PartidaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Time)
                    .WithMany(e => e.PartidaTimes)
                    .HasForeignKey(e => e.TimeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion
        }
    }
}