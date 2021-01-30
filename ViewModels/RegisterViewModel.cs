using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limak.az.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("([+994]{4})[- ]?([50,51,55,70,77]{2})[- ]?([0-9]{3})[- ]?([0-9]{2})[- ]?([0-9]{2})", ErrorMessage = "Duzgun telefon nomresi +994XXXXXXXXX")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(8)]
        public string IDSerialNumber { get; set; }
        [Required]
        public string Citizenship { get; set; }
        public DateTime Birthdate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [StringLength(7)]
        public string FINkode { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
