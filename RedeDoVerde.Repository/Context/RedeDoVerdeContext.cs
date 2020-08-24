using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RedeDoVerde.Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Repository.Context
{
    public class RedeDoVerdeContext : DbContext
    {

        public DbSet<Domain.Account.Account> Accounts { get; set; }
        public DbSet<Domain.Account.Role> Profiles { get; set; }

        public static readonly ILoggerFactory _loggerFactory = 
            LoggerFactory.Create(builder => { builder.AddConsole(); });

        public RedeDoVerdeContext(DbContextOptions<RedeDoVerdeContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new RoleMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
