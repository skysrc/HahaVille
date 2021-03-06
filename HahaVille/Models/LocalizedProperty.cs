﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HahaVille.Models
{
    public class LocalizedProperty
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int LanguageId { get; set; }
        public string LocaleKeyGroup { get; set; }
        public string LocaleKey { get; set; }
        public string LocaleValue { get; set; }
        public virtual Language Language { get; set; }
        public virtual Game Game { get; set; }

    }
}