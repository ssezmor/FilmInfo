using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class Director
    {
        public Director()
        {
            this.FilmInDirector = new HashSet<FilmInDirector>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FilmInDirector> FilmInDirector { get; set; }
    }
}