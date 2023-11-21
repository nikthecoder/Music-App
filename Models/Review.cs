using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Required]
        [MinLength(3)]
        public string Content { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public Album Album { get; set; }
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}
