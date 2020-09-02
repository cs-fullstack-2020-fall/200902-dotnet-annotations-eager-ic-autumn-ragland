using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class AlbumModel
    {
        public int id{get;set;}
        [Required]
        public string albumTitle{get;set;}
        [StringLength(1000, MinimumLength=50)]
        [Required]
        public string albumDesc{get;set;}
        [Range(1,5)]
        public int albumRating{get;set;}
    }
}