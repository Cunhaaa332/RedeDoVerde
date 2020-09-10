using RedeDoVerde.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeDoVerde.Services.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; set; }

        public AccountService(IAccountRepository accountRepository)
        {
            AccountRepository = accountRepository;
        }

        public async Task Register(string name, DateTime dtBirthday, string email, string password)
        {
            await AccountRepository.CreateUser(name, dtBirthday, email, password);
        }
    }
}
