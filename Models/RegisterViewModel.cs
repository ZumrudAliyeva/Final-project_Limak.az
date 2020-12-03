using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_project_Limak.az.Models
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
        public DateTime Birthday { get; set; }
        public DateTime Birthmonth { get; set; }
        public DateTime Birthyear { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [StringLength(7)]
        public string FINkode { get; set; }
        [Required]
        public string Address { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
    }
}
