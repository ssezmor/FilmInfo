using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmInfo.Models
{
    public class FilmInCountry
    {
        public string Id { get; set; }
        public string FilmId { get; set; }
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Film Film { get; set; }
    }
}