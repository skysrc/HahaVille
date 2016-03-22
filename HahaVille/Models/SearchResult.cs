using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HahaVille.Models
{
    public class SearchResult
    {
        public int ResultCount { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<GameInfo> Results { get; set; }
    }
}