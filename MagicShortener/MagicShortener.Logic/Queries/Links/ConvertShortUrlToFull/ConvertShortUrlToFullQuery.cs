﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Queries.Links.ConvertShortUrlToFull
{
    public class ConvertShortUrlToFullQuery : IQuery
    {
        public string ShortUrl { get; set; }
    }
}
