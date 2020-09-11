using RedeDoVerde.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Services.Post
{
    class PostService : IPostService
    {
        private readonly IAccountRepository _accountRepository;

        public PostService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

    }
}
