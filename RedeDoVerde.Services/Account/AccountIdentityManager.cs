using Microsoft.AspNetCore.Identity;
using RedeDoVerde.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedeDoVerde.Services.Account
{
    public class AccountIdentityManager : IAccountIdentityManager
    {
        private IAccountRepository _repository { get; set; }

        private SignInManager<Domain.Account.Account> _signInManager { get; set; }

        public AccountIdentityManager(IAccountRepository accountRepository, SignInManager<Domain.Account.Account> signInManager)
        {
            _repository = accountRepository;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var account = await _repository.GetAccountByEmailPassword(email, password);

            if(account == null)
            {
                return SignInResult.Failed;
            }

            await _signInManager.SignInAsync(account, false);

            return SignInResult.Success;
        }
        
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
