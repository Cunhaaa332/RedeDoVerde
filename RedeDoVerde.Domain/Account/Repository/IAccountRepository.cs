using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeDoVerde.Domain.Account.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByEmailPassword(string email, string password);
    }
}
