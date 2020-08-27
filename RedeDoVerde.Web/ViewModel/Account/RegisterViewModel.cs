using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeDoVerde.Web.ViewModel.Account
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public DateTime DtBirthday { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
    }
}
