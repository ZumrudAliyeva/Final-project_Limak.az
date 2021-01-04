using System.ComponentModel.DataAnnotations;

namespace Limak.az.Areas.ViewModels
{
    public class AdminLoginViewModel
    {
        public string Id { get; set; }
        [Required]
		[EmailAddress]
		[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "duzgun mail formati example@gmail.com")]
		[Display(Name = "E-mail")]
		public string UserName { get; set; }


		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Şifrə")]
		public string Password { get; set; }
	}
}
