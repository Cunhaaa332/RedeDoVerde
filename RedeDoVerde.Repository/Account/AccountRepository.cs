using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeDoVerde.Domain.Account.Repository;
using RedeDoVerde.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedeDoVerde.Repository.Account
{
    public class AccountRepository : IUserStore<Domain.Account.Account>, IAccountRepository
    {
        private RedeDoVerdeContext _context { get; set; }

        public AccountRepository(RedeDoVerdeContext redeDoVerdeContext)
        {
            _context = redeDoVerdeContext;
        }

        public async Task<IdentityResult> CreateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            _context.Accounts.Add(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            _context.Accounts.Remove(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<Domain.Account.Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return  _context.Accounts.FirstOrDefaultAsync(x => x.Id == new Guid(userId));
        }

        public Task<Domain.Account.Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.Name == normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetUserIdAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(Domain.Account.Account user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Domain.Account.Account user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Domain.Account.Account user, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id);

            accountToUpdate = user;
            _context.Entry(accountToUpdate).State = EntityState.Modified;

            _context.Accounts.Add(accountToUpdate);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public Task<Domain.Account.Account> GetAccountByEmailPassword(string email, string password)
        {
            return Task.FromResult(_context.Accounts
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public async Task<IdentityResult> CreateUser(string name, DateTime dtBirthday, string email, string password)
        {
            CancellationToken cancellationToken;
            var user = new Domain.Account.Account
            {
                Name = name,
                DtBirthday = dtBirthday,
                Email = email,
                Password = password
            };
            await CreateAsync(user, cancellationToken);
            return IdentityResult.Success;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AccountRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
