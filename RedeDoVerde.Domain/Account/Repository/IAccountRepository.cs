using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeDoVerde.Domain.Account.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByEmailPassword(string email, string password);
        Task<IdentityResult> CreateUser(string name, DateTime dtBirthday, string email, string password);
    }
}
