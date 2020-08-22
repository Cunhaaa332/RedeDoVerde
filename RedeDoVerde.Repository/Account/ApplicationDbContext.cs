using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedeDoVerde.Domain.Account.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Repository.Account
{
    public class ApplicationDbContext : IdentityDbContext, IAccountRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
