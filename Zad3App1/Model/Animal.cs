using System.ComponentModel.DataAnnotations;

namespace Zad3App1.Model
{
    
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Area { get; set; }
    }

}