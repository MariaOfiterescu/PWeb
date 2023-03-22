using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Pweb.API.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pweb.API.Data
{
    public class PWebDBContext : DbContext
    {
        //constructor
        public PWebDBContext(DbContextOptions <PWebDBContext> options): base(options)
        {
            
        }

        public DbSet<Actori> actori { get; set; } //te rog creeaza un tabel actori daca acesta nu exista in baza de date , e bun in termenii migrarii

        public DbSet<Director> director { get; set; }

        public DbSet<Filme> filme { get; set; }

        public DbSet<Genuri> genuri { get; set; }

        public DbSet<Scenariu> scenariu { get; set; }

        public DbSet<Scriitori> scriitori { get; set; }

        public DbSet<FilmeGenuri> filmegenuri { get; set; }

        public DbSet<FilmeActori> filmeactori { get; set; }

        public DbSet<ScriitoriScenariu> scriitoriscenariu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //one to one 

            modelBuilder.Entity<Scenariu>()
                        .HasOne<Filme>(s => s.filme)
                        .WithOne(f => f.scenariu)
                        .HasForeignKey<Filme>(f => f.scenariuid1);
            
            //many to one 
            modelBuilder.Entity<Filme>()
                        .HasOne<Director>(f => f.director)
                        .WithMany(d => d.filme)
                        .HasForeignKey(f => f.directorid1);

            //many to many 

            modelBuilder.Entity<FilmeGenuri>().HasKey(fg => new { fg.filmid, fg.genid });

            modelBuilder.Entity<FilmeGenuri>()
                .HasOne<Filme>(f => f.Filme)
                .WithMany(fg => fg.filmegenuri)
                .HasForeignKey(fg => fg.filmid);

            modelBuilder.Entity<FilmeGenuri>()
                .HasOne<Genuri>(fg => fg.Genuri)
                .WithMany(f => f.filmegenuri)
                .HasForeignKey(fg => fg.genid);

            modelBuilder.Entity<FilmeActori>().HasKey(fa => new { fa.filmid, fa.actorid });

            modelBuilder.Entity<FilmeActori>()
                .HasOne<Filme>(f => f.Filme)
                .WithMany(fa => fa.filmeactori)
                .HasForeignKey(fa => fa.filmid);

            modelBuilder.Entity<FilmeActori>()
                .HasOne<Actori>(fa => fa.Actori)
                .WithMany(f => f.filmeactori)
                .HasForeignKey(fa => fa.actorid);


            modelBuilder.Entity<ScriitoriScenariu>().HasKey(ss => new { ss.scriitorid, ss.scenariuid });

            modelBuilder.Entity<ScriitoriScenariu>()
                .HasOne<Scriitori>(s1 => s1.Scriitori)
                .WithMany(ss => ss.scriitoriscenariu)
                .HasForeignKey(ss => ss.scriitorid);

            modelBuilder.Entity<ScriitoriScenariu>()
                .HasOne<Scenariu>(ss => ss.Scenariu)
                .WithMany(s1 => s1.scriitoriscenariu)
                .HasForeignKey(ss => ss.scenariuid);


        }
        
    }
}
