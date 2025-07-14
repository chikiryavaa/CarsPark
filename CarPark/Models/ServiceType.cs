using System.ComponentModel.DataAnnotations;

namespace CarPark.Models
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EngineKey { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
