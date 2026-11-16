
using System;
using System.ComponentModel.DataAnnotations;

namespace VetCare.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hayvan adı zorunludur.")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hayvan türü zorunludur.")]
        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public int OwnerId { get; set; }

        public Owner? Owner { get; set; }
        public ICollection<Muayene> Muayeneler { get; set; } = new List<Muayene>();
    }
}
