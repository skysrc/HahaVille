using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HahaVille.Models
{
    public class GameInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int LanguageId { get; set; }
        public string Uri { get; set; }
        public string Thumbnail { get; set; }
        public Boolean IsHtml5 { get; set; }
    }
}