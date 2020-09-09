using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeDoVerde.API.ViewModel
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório")]
        public string Password { get; set; }
    }
}
