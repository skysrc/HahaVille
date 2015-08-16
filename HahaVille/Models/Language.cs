using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HahaVille.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UniqueSeoCode { get; set; }
        public virtual ICollection<LocaleStringResource> LocaleStringResources { get; set; }
        public virtual ICollection<LocalizedProperty> LocalizedProperty { get; set; }
    }
}