using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Band
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Genre")]
        [Required]
        public string Genre { get; set; }
        [Display(Name = "Formed in")]
        public int YearFormed { get; set; }
        [Display(Name = "Average rating")]
        public double? AverageRating { get; set; }
        [Display(Name = "Albums")]
        public List<Musician> Musicians { get; set; }
        public List<Album> Albums { get; set; }
    }
}
