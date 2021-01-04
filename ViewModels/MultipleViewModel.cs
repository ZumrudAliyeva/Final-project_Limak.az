using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.az.ViewModels
{
    public class MultipleViewModel
    {
        public LoginViewModel loginViewModel { get; set; }
        public RegisterViewModel registerViewModel { get; set; }
        public OrderViewModel orderViewModel { get; set; }
        public DeclarationViewModel declarationViewModel { get; set; }
        public SettingsViewModel settingsViewModel { get; set; }
    }
}
