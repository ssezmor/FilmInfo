using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class Film
    {
        public Film()
        {
            this.FilmInCountry = new HashSet<FilmInCountry>();
            this.FilmInDirector = new HashSet<FilmInDirector>();
        }

        public string Id { get; set; }
        public string OriginalName { get; set; }
        public string RussianName { get; set; }
        public string PosterUrl { get; set; }
        public Nullable<short> Year { get; set; }
        public string Slogan { get; set; }
        public string KPRatings { get; set; }
        public string IMDbRatings { get; set; }
        public string Description { get; set; }
      //  public string Director { get; set; }
      //  public string Country{ get; set; }

        public virtual ICollection<FilmInCountry> FilmInCountry { get; set; }
        public virtual ICollection<FilmInDirector> FilmInDirector { get; set; }
    }
}