using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPark.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ServiceName { get; set; }
    }
}
