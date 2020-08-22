using RedeDoVerde.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Services.Account
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository { get; set; }

        public AccountService(IAccountRepository accountRepository)
        {
            AccountRepository = accountRepository;
        }
    }
}
