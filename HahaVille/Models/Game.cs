using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HahaVille.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string FbShareThumb { get; set; }
        public string GamePath { get; set; }
        public bool IsHtml5 { get; set; }
        public int CategoryId { get; set; }
        public int TotalPlayed { get; set; }
        public virtual Category Category { get; set; }

    }
}