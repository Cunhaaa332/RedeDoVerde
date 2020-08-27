using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeDoVerde.Services.Account
{
    public interface IAccountService
    {
        Task Register(string name, DateTime dtBirthday, string email, string password);
    }
}
