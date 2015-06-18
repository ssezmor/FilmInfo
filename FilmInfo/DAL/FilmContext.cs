using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmInfo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;


namespace FilmInfo.DAL
{
    public class FilmContext : DbContext
    {

        public FilmContext() : base("name=FilmContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Country> Country { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmInCountry> FilmInCountry { get; set; }
        public DbSet<FilmInDirector> FilmInDirector { get; set; }


    }
}