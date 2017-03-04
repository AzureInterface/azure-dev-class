using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required]
        public string Rating { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}