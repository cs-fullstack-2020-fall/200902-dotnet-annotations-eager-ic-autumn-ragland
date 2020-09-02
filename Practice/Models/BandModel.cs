using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Practice.Models
{
    public class BandModel
    {
        public int id{get;set;}
        [Required]
        public string bandName{get;set;}
        [Required]
        public string musicGenre{get;set;}
        public List<AlbumModel> albums{get;set;}
    }
}