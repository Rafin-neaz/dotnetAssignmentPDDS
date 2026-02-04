using System.ComponentModel.DataAnnotations;

namespace dotnetAssignment.Models
{
    public class TouristPlace
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage="Name can't exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double Rating { get; set; }
        [Required]
        public PlaceType? Type { get; set; }

        public String? PhotoPath { get; set; }
        
        
        //[RegularExpression("/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$/.", ErrorMessage="Please Provide correct email format")]
        //public string email { get; set; }
    }
}
