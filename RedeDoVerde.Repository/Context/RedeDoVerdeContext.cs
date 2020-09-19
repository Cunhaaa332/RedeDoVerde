using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        public DbSet<Domain.Post.Post> Posts { get; set; }
        public DbSet<Domain.Comment.Comments> Comments { get; set; }

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
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new CommentMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<RedeDoVerdeContext>
    {
        public RedeDoVerdeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RedeDoVerdeContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RedeDoVerde;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RedeDoVerdeContext(optionsBuilder.Options);
        }
    }
}
