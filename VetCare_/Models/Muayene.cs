using System;
using System.ComponentModel.DataAnnotations;

namespace VetCare.Models
{
    public class Muayene
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hayvan seçimi zorunludur.")]
        public int PetId { get; set; }

        public DateTime MuayeneTarihi { get; set; }

        public TimeSpan MuayeneSaati { get; set; }

        [Required(ErrorMessage = "Yapılan işlem zorunludur.")]
        [StringLength(500)]
        public string YapilanIslem { get; set; } = string.Empty;

        public Pet? Pet { get; set; }
    }
}