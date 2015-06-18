using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class Country
    {
        public Country()
        {
            this.FilmInCountry = new HashSet<FilmInCountry>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FilmInCountry> FilmInCountry { get; set; }
    }
}