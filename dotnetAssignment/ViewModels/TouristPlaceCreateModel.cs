using dotnetAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace dotnetAssignment.ViewModels
{
    public class TouristPlaceCreateModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can't exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double Rating { get; set; }
        [Required]
        public PlaceType? Type { get; set; }

        public IFormFile? Photo { get; set; }
        public string? Title { get; set; }
    }
}
