using System.ComponentModel.DataAnnotations;

namespace FizzBuzzProj.Models
{
    public class FizzBuzz
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Value must be between 1 and 1000")]

        public int Number { get; set; }
        public int CurrentPage { get; set; }

    }
}
