using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.API.DTOs;

public class CreateBuildingDto
    {   
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Range(1, 10000)]
        public int NumberOfUnits { get; set; }
    }