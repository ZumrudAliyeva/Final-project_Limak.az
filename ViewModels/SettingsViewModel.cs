using System;
using System.ComponentModel.DataAnnotations;

namespace Limak.az.ViewModels
{
    public class SettingsViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(8)]
        public string IDSerialNumber { get; set; }
        [Required]
        public string Citizenship { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        [Required]
        [StringLength(7)]
        public string FINkode { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
