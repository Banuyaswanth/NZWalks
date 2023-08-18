﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class CreateRegionRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
