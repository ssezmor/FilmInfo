using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class FilmContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public FilmContext() : base("name=FilmContext")
        {
        }

      //  public System.Data.Entity.DbSet<FilmInfo.Models.Film> Film { get; set; }


        public DbSet<Country> Country { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmInCountry> FilmInCountry { get; set; }
        public DbSet<FilmInDirector> FilmInDirector { get; set; }
    
    }
}
