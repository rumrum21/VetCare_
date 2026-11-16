
using System.ComponentModel.DataAnnotations;

namespace VetCare.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad soyad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Adres en fazla 300 karakter olabilir.")]
        public string Address { get; set; } = string.Empty;

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
