using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTeamE.web.Domain.DB
{
    public class Movie
    {
        public int CID { get; set; }

        public string Content_Title { get; set; }

        public string Poster { get; set; }

        public int ReleaseYear { get; set; }

        public string Genre { get; set; }
    }
}