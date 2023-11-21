using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Album
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Release year")]
        [RegularExpression(@"^(18|19|20)\d{2}$")]
        public int ReleaseYear { get; set; }
        public List<Review> Reviews { get; set; }
        [Display(Name = "Average score")]
        public double? AverageRating { get; set; }
        [Display(Name = "Band")]
        public Band Band { get; set; }
    }
}
