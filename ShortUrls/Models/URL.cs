using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShortUrls.Models
{
    public class Url
    {
        public long Id { get; set; }

        [Required]
        public string LongUrl { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        public DateTime DateGenerated { get; set; }

        public string UserId { get; set; }
    }
}