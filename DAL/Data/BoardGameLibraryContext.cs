using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;

namespace DAL.Data
{
    public class BoardGameLibraryContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<BoardGameInCollection> BoardGamesInCollections { get; set; }

        public BoardGameLibraryContext(DbContextOptions<BoardGameLibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGameInCollection>()
                  .HasKey(bc => new { bc.BoardGameId, bc.CollectionId });

            //modelBuilder.Entity<BoardGame>()
            //    .HasMany(b => b.BoardGamesInCollections)
            //    .WithOne(bic => bic.BoardGame)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<BoardGameInCollection>()
            //            .HasOne(bc => bc.BoardGame)
            //            .WithMany(b => b.BoardGamesInCollections)
            //            .HasForeignKey(bc => bc.BoardGameId);

            //modelBuilder.Entity<BoardGameInCollection>()
            //            .HasOne(bc => bc.Collection)
            //            .WithMany(c => c.BoardGamesInCollections)
            //            .HasForeignKey(bc => bc.CollectionId);

            modelBuilder.Entity<User>()
              .HasMany(u => u.Collections)
              .WithOne(c => c.User)
              .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Producer>()
            .HasMany(a => a.BoardGames)
            .WithOne(b => b.Producer)
            .HasForeignKey(a => a.ProducerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
