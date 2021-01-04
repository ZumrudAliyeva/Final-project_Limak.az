using Microsoft.AspNetCore.Identity;

namespace Limak.az.ViewModels
{
    public class RoleViewModel : IdentityRole
    {
        public string RoleName { get; set; }
    }
}
