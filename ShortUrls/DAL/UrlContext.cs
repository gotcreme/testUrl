using ShortUrls.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShortUrls.DAL
{
    public class UrlContext: DbContext
    {
        public DbSet<Url> Urls { get; set; }
    }
}