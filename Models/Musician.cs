using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class Musician
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Birthday")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Country")]
        [StringLength(57)]
        public string Country { get; set; }
        [Display(Name = "Band")]
        [Required]
        public Band Band { get; set; }

    }
}
