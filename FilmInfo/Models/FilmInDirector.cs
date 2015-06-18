using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class FilmInDirector
    {
        public string Id { get; set; }
        public string FilmId { get; set; }
        public string DirectorId { get; set; }

        public virtual Director Director { get; set; }
        public virtual Film Film { get; set; }
    }
}